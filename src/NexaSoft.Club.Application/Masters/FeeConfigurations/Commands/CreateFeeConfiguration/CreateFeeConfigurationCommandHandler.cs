using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.CreateFeeConfiguration;

public class CreateFeeConfigurationCommandHandler(
    IGenericRepository<FeeConfiguration> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateFeeConfigurationCommandHandler> _logger
) : ICommandHandler<CreateFeeConfigurationCommand, long>
{
    public async Task<Result<long>> Handle(CreateFeeConfigurationCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de FeeConfiguration");

        bool existsFeeName = await _repository.ExistsAsync(c => c.FeeName == command.FeeName, cancellationToken);
        if (existsFeeName)
        {
            return Result.Failure<long>(FeeConfigurationErrores.Duplicado);
        }

        var entity = FeeConfiguration.Create(
            command.FeeName,
            command.PeriodicityId,
            command.DueDay,
            command.DefaultAmount,
            command.IsVariable,
            command.Priority,
            command.AppliesToFamily,
            command.IncomeAccountId,
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
            _logger.LogInformation("FeeConfiguration con ID {FeeConfigurationId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear FeeConfiguration");
            return Result.Failure<long>(FeeConfigurationErrores.ErrorSave);
        }
    }
}
