using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.RateTypes;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.RateTypes.Commands.CreateRateType;

public class CreateRateTypeCommandHandler(
    IGenericRepository<RateType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateRateTypeCommandHandler> _logger
) : ICommandHandler<CreateRateTypeCommand, long>
{
    public async Task<Result<long>> Handle(CreateRateTypeCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de RateType");

     bool existsCode = await _repository.ExistsAsync(c => c.Code == command.Code, cancellationToken);
     if (existsCode)
     {
       return Result.Failure<long>(RateTypeErrores.Duplicado);
     }

     bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
     if (existsName)
     {
       return Result.Failure<long>(RateTypeErrores.Duplicado);
     }

        var entity = RateType.Create(
            command.Code,
            command.Name,
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
            _logger.LogInformation("RateType con ID {RateTypeId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear RateType");
            return Result.Failure<long>(RateTypeErrores.ErrorSave);
        }
    }
}
