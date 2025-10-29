using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.SpecialRates;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.SpecialRates.Commands.CreateSpecialRate;

public class CreateSpecialRateCommandHandler(
    IGenericRepository<SpecialRate> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateSpecialRateCommandHandler> _logger
) : ICommandHandler<CreateSpecialRateCommand, long>
{
    public async Task<Result<long>> Handle(CreateSpecialRateCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de SpecialRate");

     bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
     if (existsName)
     {
       return Result.Failure<long>(SpecialRateErrores.Duplicado);
     }

        var entity = SpecialRate.Create(
            command.RateTypeId,
            command.Name,
            command.Multiplier,
            command.StartTime,
            command.EndTime,
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
            _logger.LogInformation("SpecialRate con ID {SpecialRateId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear SpecialRate");
            return Result.Failure<long>(SpecialRateErrores.ErrorSave);
        }
    }
}
