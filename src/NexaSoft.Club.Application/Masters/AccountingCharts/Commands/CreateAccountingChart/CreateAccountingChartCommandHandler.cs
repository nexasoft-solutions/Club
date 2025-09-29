using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.AccountingCharts;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Commands.CreateAccountingChart;

public class CreateAccountingChartCommandHandler(
    IGenericRepository<AccountingChart> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateAccountingChartCommandHandler> _logger
) : ICommandHandler<CreateAccountingChartCommand, long>
{
    public async Task<Result<long>> Handle(CreateAccountingChartCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de AccountingChart");

     bool existsAccountCode = await _repository.ExistsAsync(c => c.AccountCode == command.AccountCode, cancellationToken);
     if (existsAccountCode)
     {
       return Result.Failure<long>(AccountingChartErrores.Duplicado);
     }

     bool existsAccountName = await _repository.ExistsAsync(c => c.AccountName == command.AccountName, cancellationToken);
     if (existsAccountName)
     {
       return Result.Failure<long>(AccountingChartErrores.Duplicado);
     }

        var entity = AccountingChart.Create(
            command.AccountCode,
            command.AccountName,
            command.AccountType,
            command.ParentAccountId,
            command.Level,
            command.AllowsTransactions,
            command.Description,
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
            _logger.LogInformation("AccountingChart con ID {AccountingChartId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear AccountingChart");
            return Result.Failure<long>(AccountingChartErrores.ErrorSave);
        }
    }
}
