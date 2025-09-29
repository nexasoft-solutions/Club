using MediatR;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberFees;
using NexaSoft.Club.Domain.Features.Payments;
using NexaSoft.Club.Domain.Features.Payments.Events;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;

public class PaymentCreatedDomainEventHandler(
    IGenericRepository<PaymentItem> _paymentItemRepository,
    IGenericRepository<MemberFee> _memberFeeRepository,
    IGenericRepository<Payment> _paymentRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<PaymentCreatedDomainEventHandler> _logger
) : INotificationHandler<PaymentCreateDomainEvent>
{
    public async Task Handle(PaymentCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Procesando items de pago para Payment ID: {PaymentId}", notification.PaymentId);

        try
        {

            var payment = await _paymentRepository.GetByIdAsync(notification.PaymentId, cancellationToken);
            if (payment == null)
            {
                _logger.LogError("Payment con ID {PaymentId} no encontrado", notification.PaymentId);
                return;
            }

            // PROCESAR ITEMS DE PAGO
            var appliedItems = await ProcessPaymentItems(notification, payment, cancellationToken);

            // ACTUALIZAR SALDO DE CRÉDITO SI ES NECESARIO
            await UpdatePaymentCreditBalance(notification, payment, appliedItems, cancellationToken);

               // 3. DISPARAR EVENTO DE ITEMS CREADOS
            payment.RaiseDomainEvent(new PaymentItemsCreatedDomainEvent(
                notification.PaymentId,
                notification.MemberId,
                notification.Amount,
                notification.PaymentMethod,
                notification.ReceiptNumber,
                appliedItems,
                notification.CreatedBy,
                _dateTimeProvider.CurrentTime.ToUniversalTime()
            ));

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            //await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Payment items procesados exitosamente para pago ID: {PaymentId}", notification.PaymentId);
        }
        catch (Exception ex)
        {
            //await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al procesar items de pago para Payment ID: {PaymentId}", notification.PaymentId);
            throw;
        }
    }

    private async Task<List<AppliedPaymentItem>> ProcessPaymentItems(
        PaymentCreateDomainEvent notification, 
        Payment payment, 
        CancellationToken cancellationToken)
    {
        var appliedItems = new List<AppliedPaymentItem>();

        if (notification.PaymentItems.Any())
        {
            // 🔹 Caso 1: Pago específico por items
            appliedItems = await ProcessSpecificPaymentItems(notification, payment, cancellationToken);
        }
        else
        {
            // 🔹 Caso 2: Pago general automático
            appliedItems = await ProcessGeneralPayment(notification, payment, cancellationToken);
        }

        return appliedItems;
    }

    private async Task<List<AppliedPaymentItem>> ProcessSpecificPaymentItems(
        PaymentCreateDomainEvent notification, 
        Payment payment, 
        CancellationToken cancellationToken)
    {
        var appliedItems = new List<AppliedPaymentItem>();
        var memberFees = new List<MemberFee>();

        // Validar y obtener las cuotas primero
        foreach (var item in notification.PaymentItems)
        {
            var memberFee = await _memberFeeRepository.GetByIdAsync(item.MemberFeeId, cancellationToken);
            if (memberFee == null || memberFee.MemberId != notification.MemberId)
            {
                throw new InvalidOperationException($"Cuota inválida: {item.MemberFeeId}");
            }

            if (memberFee.Status == "Pagado" && memberFee.RemainingAmount <= 0)
            {
                throw new InvalidOperationException($"Cuota ya pagada: {item.MemberFeeId}");
            }

            if (item.AmountToPay > memberFee.RemainingAmount)
            {
                throw new InvalidOperationException($"Monto excede deuda en cuota: {item.MemberFeeId}");
            }

            memberFees.Add(memberFee);
        }

        // Procesar pagos
        foreach (var item in notification.PaymentItems)
        {
            var memberFee = memberFees.First(f => f.Id == item.MemberFeeId);

            var paymentItem = PaymentItem.Create(
                payment.Id,
                memberFee.Id,
                item.AmountToPay,
                (int)EstadosEnum.Activo,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                notification.CreatedBy
            );

            await _paymentItemRepository.AddAsync(paymentItem, cancellationToken);

            memberFee.ApplyPayment(item.AmountToPay, _dateTimeProvider.CurrentTime.ToUniversalTime(), notification.CreatedBy);
            await _memberFeeRepository.UpdateAsync(memberFee);

            appliedItems.Add(new AppliedPaymentItem(paymentItem.Id, memberFee.Id, item.AmountToPay));
        }

        return appliedItems;
    }

    private async Task<List<AppliedPaymentItem>> ProcessGeneralPayment(
        PaymentCreateDomainEvent notification, 
        Payment payment, 
        CancellationToken cancellationToken)
    {
        var appliedItems = new List<AppliedPaymentItem>();

        var pendingFees = await _memberFeeRepository.ListAsync(
            new PendingFeesByMemberWithFeeConfigSpec(notification.MemberId),
            cancellationToken
        );

        decimal remaining = notification.Amount;

        foreach (var fee in pendingFees
            .OrderBy(f => f.MemberTypeFee!.FeeConfiguration.Priority)
            .ThenBy(f => f.DueDate))
        {
            if (remaining <= 0) break;

            var toPay = Math.Min(remaining, fee.RemainingAmount);

            var paymentItem = PaymentItem.Create(
                payment.Id,
                fee.Id,
                toPay,
                (int)EstadosEnum.Activo,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                notification.CreatedBy
            );

            await _paymentItemRepository.AddAsync(paymentItem, cancellationToken);

            fee.ApplyPayment(toPay, _dateTimeProvider.CurrentTime.ToUniversalTime(), notification.CreatedBy);
            await _memberFeeRepository.UpdateAsync(fee);

            appliedItems.Add(new AppliedPaymentItem(paymentItem.Id, fee.Id, toPay));
            remaining -= toPay;
        }

        return appliedItems;
    }

    private async Task UpdatePaymentCreditBalance(
        PaymentCreateDomainEvent notification, 
        Payment payment, 
        List<AppliedPaymentItem> appliedItems,
        CancellationToken cancellationToken)
    {
        var totalApplied = appliedItems.Sum(x => x.AmountApplied);
        var remainingCredit = notification.Amount - totalApplied;

        if (remainingCredit > 0)
        {
            payment.AddCreditBalance(remainingCredit);
            await _paymentRepository.UpdateAsync(payment);
            
            _logger.LogInformation("Saldo de crédito de {CreditBalance} agregado al pago {PaymentId}", 
                remainingCredit, payment.Id);
        }
    }

}