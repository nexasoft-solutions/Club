using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberFees;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Features.Payments;
using NexaSoft.Club.Domain.Features.Payments.Events;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler(
    IGenericRepository<Payment> _paymentRepository,
    IGenericRepository<PaymentItem> _paymentItemRepository,
    IGenericRepository<Member> _memberRepository,
    IGenericRepository<MemberFee> _memberFeeRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePaymentCommandHandler> _logger
) : ICommandHandler<CreatePaymentCommand, PaymentResponse>
{
    public async Task<Result<PaymentResponse>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de creación de Payment");

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. VALIDACIONES
            var validationResult = await ValidatePayment(command, cancellationToken);
            if (validationResult.IsFailure)
                return Result.Failure<PaymentResponse>(validationResult.Error);

            var (member, memberFees) = validationResult.Value;

            // 2. VERIFICAR NÚMERO DE RECIBO DUPLICADO
            bool existsReceiptNumber = await _paymentRepository.ExistsAsync(
                c => c.ReceiptNumber == command.ReceiptNumber, cancellationToken);

            if (existsReceiptNumber)
                return Result.Failure<PaymentResponse>(PaymentErrores.Duplicado);

            // 3. CREAR REGISTRO DE PAGO
            var payment = await CreatePaymentRecord(command, cancellationToken);

            // 4. DISPARAR EVENTO DE CREACIÓN DE PAGO
            payment.RaiseDomainEvent(new PaymentCreateDomainEvent(
                payment.Id,
                command.MemberId,
                command.Amount,
                command.PaymentMethod!,
                command.ReceiptNumber!,
                command.PaymentDate,
                command.PaymentItems?.Select(
                        x => new PaymentItemDetail(x.MemberFeeId, x.AmountToPay)).ToList() ?? new(),
                command.CreatedBy,
                _dateTimeProvider.CurrentTime.ToUniversalTime()
            ));

            // 5. ACTUALIZAR BALANCE DEL MIEMBRO
            await UpdateMemberBalance(member, command.Amount, command.CreatedBy, cancellationToken);

            // 6. GUARDAR CAMBIOS Y CONFIRMAR TRANSACCIÓN
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            // 7. GENERAR RESPUESTA (los items se cargarán desde la BD)
            var paymentItems = await LoadPaymentItems(payment.Id, cancellationToken);
            var response = CreatePaymentResponse(payment, paymentItems);

            _logger.LogInformation("Payment con ID {PaymentId} creado satisfactoriamente", payment.Id);
            return Result.Success(response);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Payment para miembro {MemberId}", command.MemberId);
            return Result.Failure<PaymentResponse>(PaymentErrores.ErrorSave);
        }
    }

    private async Task<List<PaymentItemResponse>> LoadPaymentItems(long paymentId, CancellationToken cancellationToken)
    {
        var specParams = new BaseSpecParams
        {
            NoPaging = true,
            SearchFields = "payment",
            Search = paymentId.ToString()
        };
        var spec = new PaymentItemsByPaymentSpec(specParams);

        var paymentItems = await _paymentItemRepository.ListAsync(spec, cancellationToken);

        return paymentItems.Select(item => new PaymentItemResponse(
            item.Id,
            item.MemberFeeId,
            item.MemberFee?.Period ?? "N/A",
            item.Amount,
            item.MemberFee?.Status ?? "Pendiente"
        )).ToList();
    }

    private async Task<Result<(Member Member, List<MemberFee> MemberFees)>> ValidatePayment(
     CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(command.MemberId, cancellationToken);
        if (member is null)
            return Result.Failure<(Member, List<MemberFee>)>(PaymentErrores.MiembroNoExiste);

        var memberFees = new List<MemberFee>();
        if (command.PaymentItems != null && command.PaymentItems.Any())
        {
            if (command.PaymentItems.Sum(x => x.AmountToPay) != command.Amount)
                return Result.Failure<(Member, List<MemberFee>)>(PaymentErrores.MontoNoCoincide);

            foreach (var item in command.PaymentItems)
            {
                var memberFee = await _memberFeeRepository.GetByIdAsync(item.MemberFeeId, cancellationToken);
                if (memberFee == null || memberFee.MemberId != command.MemberId)
                    return Result.Failure<(Member, List<MemberFee>)>(PaymentErrores.CuotaInvalida);

                if (memberFee.Status == "Pagado" && memberFee.RemainingAmount <= 0)
                    return Result.Failure<(Member, List<MemberFee>)>(PaymentErrores.CuotaYaPagada);

                if (item.AmountToPay > memberFee.RemainingAmount)
                    return Result.Failure<(Member, List<MemberFee>)>(PaymentErrores.MontoExcedeDeuda);

                memberFees.Add(memberFee);
            }
        }

        return Result.Success((member, memberFees));
    }

    private async Task<Payment> CreatePaymentRecord(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        // Determinar si es pago parcial
        //bool isPartial = false;
        bool isPartial = await DetermineIfPartialPayment(command, cancellationToken);

        var payment = Payment.Create(
            command.MemberId,
            command.Amount,
            command.PaymentDate,
            command.PaymentMethod,
            command.ReferenceNumber,
            command.ReceiptNumber,
            isPartial: isPartial,
            accountingEntryId: null,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _paymentRepository.AddAsync(payment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken); // Generar ID del payment

        return payment;
    }

    private async Task<bool> DetermineIfPartialPayment(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        // Si no hay items específicos, es un pago general
        if (command.PaymentItems == null || !command.PaymentItems.Any())
        {
            return await IsGeneralPaymentPartial(command.MemberId, command.Amount, cancellationToken);
        }

        // Si hay items, verificar si alguno es parcial
        return await IsSpecificPaymentPartial(command.PaymentItems, cancellationToken);
    }

    private async Task<bool> IsGeneralPaymentPartial(long memberId, decimal amount, CancellationToken cancellationToken)
    {
        try
        {
            // Obtener la deuda total del miembro
            var totalDebt = await GetTotalMemberDebt(memberId, cancellationToken);

            // Es parcial si el monto pagado es menor que la deuda total
            return amount < totalDebt && totalDebt > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al determinar si el pago general es parcial para miembro {MemberId}", memberId);
            return false; // Por defecto, considerar como pago completo en caso de error
        }
    }

    private async Task<bool> IsSpecificPaymentPartial(List<PaymentItemDto> paymentItems, CancellationToken cancellationToken)
    {
        foreach (var item in paymentItems)
        {
            try
            {
                // Obtener la cuota específica
                var memberFee = await _memberFeeRepository.GetByIdAsync(item.MemberFeeId, cancellationToken);

                if (memberFee == null)
                {
                    _logger.LogWarning("No se encontró la cuota {MemberFeeId} para verificar pago parcial", item.MemberFeeId);
                    continue;
                }

                // Verificar si el pago es menor al monto pendiente de la cuota
                if (item.AmountToPay < memberFee.RemainingAmount && memberFee.RemainingAmount > 0)
                {
                    _logger.LogInformation("Pago parcial detectado para cuota {MemberFeeId}: Pagado {Paid} de {Remaining}",
                        item.MemberFeeId, item.AmountToPay, memberFee.RemainingAmount);
                    return true;
                }

                // Verificar si el pago es menor al monto original de la cuota (para cuotas no pagadas)
                if (item.AmountToPay < memberFee.Amount && memberFee.RemainingAmount == memberFee.Amount)
                {
                    _logger.LogInformation("Pago parcial detectado para cuota {MemberFeeId}: Pagado {Paid} de {Total}",
                        item.MemberFeeId, item.AmountToPay, memberFee.Amount);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar pago parcial para cuota {MemberFeeId}", item.MemberFeeId);
            }
        }

        // Si ninguna cuota es parcial, el pago es completo
        return false;
    }

    private async Task UpdateMemberBalance(Member member, decimal amount, string updatedBy, CancellationToken cancellationToken)
    {
        // Usar el nuevo método que actualiza balance y verifica cambio de estado
        member.UpdateBalance(amount, updatedBy);
        await _memberRepository.UpdateAsync(member);

        _logger.LogInformation("Balance del miembro {MemberId} actualizado. Nuevo balance: {Balance}",
            member.Id, member.Balance);
    }



    private PaymentResponse CreatePaymentResponse(Payment payment, List<PaymentItemResponse> appliedItems)
    {
        return new PaymentResponse(
            payment.Id,
            payment.ReceiptNumber ?? string.Empty,
            payment.TotalAmount,
            payment.PaymentDate,
            appliedItems.All(item => item.FeeStatus == "Paid") ? "Completed" : "Partial",
            appliedItems
        );
    }

    private async Task<decimal> GetTotalMemberDebt(long memberId, CancellationToken cancellationToken)
    {
        try
        {
            // Obtener todas las cuotas pendientes del miembro
            var pendingFees = await _memberFeeRepository.ListAsync(
                new PendingFeesByMemberSpec(memberId), cancellationToken);

            return pendingFees.Sum(f => f.RemainingAmount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al calcular la deuda total del miembro {MemberId}", memberId);
            return 0M;
        }
    }
}