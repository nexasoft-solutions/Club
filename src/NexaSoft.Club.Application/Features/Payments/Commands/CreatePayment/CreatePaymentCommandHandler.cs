using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Payments.Background;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberFees;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Features.Payments;
using NexaSoft.Club.Domain.Masters.Contadores;
using NexaSoft.Club.Domain.Masters.DocumentTypes;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler(
    IGenericRepository<Payment> _paymentRepository,
    IPaymentBackgroundTaskService _backgroundTaskService,
    IGenericRepository<Member> _memberRepository,
    IGenericRepository<MemberFee> _memberFeeRepository,
    IGenericRepository<Contador> _contadorRepository,
    IGenericRepository<DocumentType> _documentTypeRepository,
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
            // 1. VALIDACIONES (SIN TRANSACCIÓN)
            var validationResult = await ValidatePayment(command, cancellationToken);
            if (validationResult.IsFailure)
                return Result.Failure<PaymentResponse>(validationResult.Error);

            var (member, memberFees) = validationResult.Value;

            // 2. VERIFICAR NÚMERO DE RECIBO DUPLICADO
            bool existsReceiptNumber = await _paymentRepository.ExistsAsync(
                c => c.MemberId == command.MemberId && c.ReceiptNumber == command.ReceiptNumber, cancellationToken);

            if (existsReceiptNumber)
                return Result.Failure<PaymentResponse>(PaymentErrores.Duplicado);

            // 3. TRANSACCIÓN CORTA: SOLO OPERACIONES CRÍTICAS
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 3.1 CREAR REGISTRO DE PAGO
            var payment = await CreatePaymentRecord(command, cancellationToken);

            // 3.2 ACTUALIZAR BALANCE DEL MIEMBRO (ATÓMICO)
            await UpdateMemberBalanceAtomic(member, command.Amount, command.CreatedBy);

            // 3.3 COMMIT RÁPIDO
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Payment {PaymentId} creado, iniciando procesamiento background", payment.Id);

            // 4. ENCOLAR PROCESAMIENTO BACKGROUND
            await _backgroundTaskService.QueuePaymentProcessingAsync(payment.Id, command, cancellationToken);

            // 5. RESPUESTA INMEDIATA
            var response = CreatePaymentResponseImmediate(payment);

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

    private async Task UpdateMemberBalanceAtomic(Member member, decimal amount, string updatedBy)
    {
        member.UpdateBalance(amount, updatedBy);
        // No necesitamos UpdateAsync porque ya está siendo trackeado
        _logger.LogInformation("Balance del miembro {MemberId} actualizado. Nuevo balance: {Balance}",
            member.Id, member.Balance);
        // Retornar Task completada
        await Task.CompletedTask;
    }

    private PaymentResponse CreatePaymentResponseImmediate(Payment payment)
    {
        return new PaymentResponse(
            payment.Id,
            payment.ReceiptNumber ?? string.Empty,
            payment.TotalAmount,
            payment.PaymentDate,
            StatusEnum.Iniciado.ToString(), // Estado inicial
            new List<PaymentItemResponse>() // Items vacíos, se procesan en background
        );
    }

    // LOS DEMÁS MÉTODOS SE MANTIENEN IGUAL
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

                if (memberFee.StatusId == (int)StatusEnum.Pagado && memberFee.RemainingAmount <= 0)
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
        bool isPartial = await DetermineIfPartialPayment(command, cancellationToken);

        if (string.IsNullOrWhiteSpace(command.ReceiptNumber))
        {
            command = command with
            {
                ReceiptNumber = await GenerateUniqueReceiptNumber(command.DocumentTypeId, command.CreatedBy, cancellationToken)
            };
            _logger.LogInformation("Número de recibo generado: {ReceiptNumber}", command.ReceiptNumber);
        }

        var payment = Payment.Create(
            command.MemberId,
            command.Amount,
            command.PaymentDate,
            command.PaymentMethodId,
            command.ReferenceNumber,
            command.DocumentTypeId,
            command.ReceiptNumber,
            isPartial: isPartial,
            accountingEntryId: null,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy,
            statusId: (long)StatusEnum.Iniciado
        );

        // Marcar como processing inmediatamente
        payment.MarkAsProcessing();

        await _paymentRepository.AddAsync(payment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return payment;
    }

    private async Task<string> GenerateUniqueReceiptNumber(long DocumentTypeId, string createdBy, CancellationToken cancellationToken)
    {
        try
        {

            var documentType = await _documentTypeRepository.GetByIdAsync(DocumentTypeId, cancellationToken);
            if (documentType == null)
            {
                _logger.LogWarning("DocumentType con ID {DocumentTypeId} no encontrado. Usando valor por defecto para serie.", DocumentTypeId);
            }

            var contador = await _contadorRepository.GetEntityWithSpec(new ContadorRawSpec("DocumentType", documentType!.Serie), cancellationToken);

            if (contador == null)
            {
                var contadorNew = Contador.Create(
                    "DocumentType",
                    documentType!.Serie ?? "R-001",
                    1,
                    string.Empty,
                    "string",
                    8,
                    _dateTimeProvider.CurrentTime.ToUniversalTime(),
                    createdBy
                );

                await _contadorRepository.AddAsync(contadorNew, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                contador = contadorNew;
            }

            var nuevoCodigo = contador.Incrementar(_dateTimeProvider.CurrentTime.ToUniversalTime(), createdBy, null);
            return nuevoCodigo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar número de comprobando único");
            return $"R-001-{DateTime.Now:yyyyMMddHHmmss}";
        }
    }


    private async Task<bool> DetermineIfPartialPayment(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        if (command.PaymentItems == null || !command.PaymentItems.Any())
        {
            return await IsGeneralPaymentPartial(command.MemberId, command.Amount, cancellationToken);
        }
        return await IsSpecificPaymentPartial(command.PaymentItems, cancellationToken);
    }

    private async Task<bool> IsGeneralPaymentPartial(long memberId, decimal amount, CancellationToken cancellationToken)
    {
        try
        {
            var totalDebt = await GetTotalMemberDebt(memberId, cancellationToken);
            return amount < totalDebt && totalDebt > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al determinar si el pago general es parcial para miembro {MemberId}", memberId);
            return false;
        }
    }

    private async Task<bool> IsSpecificPaymentPartial(List<PaymentItemDto> paymentItems, CancellationToken cancellationToken)
    {
        foreach (var item in paymentItems)
        {
            try
            {
                var memberFee = await _memberFeeRepository.GetByIdAsync(item.MemberFeeId, cancellationToken);
                if (memberFee == null) continue;

                if (item.AmountToPay < memberFee.RemainingAmount && memberFee.RemainingAmount > 0)
                {
                    _logger.LogInformation("Pago parcial detectado para cuota {MemberFeeId}", item.MemberFeeId);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar pago parcial para cuota {MemberFeeId}", item.MemberFeeId);
            }
        }
        return false;
    }

    private async Task<decimal> GetTotalMemberDebt(long memberId, CancellationToken cancellationToken)
    {
        try
        {
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
