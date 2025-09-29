using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.ExpensesVouchers;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.ExpensesVouchers.Commands.CreateExpenseVoucher;

public class CreateExpenseVoucherCommandHandler(
    IGenericRepository<ExpenseVoucher> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateExpenseVoucherCommandHandler> _logger
) : ICommandHandler<CreateExpenseVoucherCommand, long>
{
    public async Task<Result<long>> Handle(CreateExpenseVoucherCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de ExpenseVoucher");

        bool existsVoucherNumber = await _repository.ExistsAsync(c => c.VoucherNumber == command.VoucherNumber, cancellationToken);
        if (existsVoucherNumber)
        {
            return Result.Failure<long>(ExpenseVoucherErrores.Duplicado);
        }

        var entity = ExpenseVoucher.Create(
            command.EntryId,
            command.VoucherNumber,
            command.SupplierName,
            command.Amount,
            command.IssueDate,
            command.Description,
            command.ExpenseAccountId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );



        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("ExpenseVoucher con ID {ExpenseVoucherId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear ExpenseVoucher");
            return Result.Failure<long>(ExpenseVoucherErrores.ErrorSave);
        }
    }
}
