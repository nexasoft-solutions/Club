using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.CostCenterTypes;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.CostCenterTypes.Commands.CreateCostCenterType;

public class CreateCostCenterTypeCommandHandler(
    IGenericRepository<CostCenterType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateCostCenterTypeCommandHandler> _logger
) : ICommandHandler<CreateCostCenterTypeCommand, long>
{
    public async Task<Result<long>> Handle(CreateCostCenterTypeCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de CostCenterType");

     bool existsCode = await _repository.ExistsAsync(c => c.Code == command.Code, cancellationToken);
     if (existsCode)
     {
       return Result.Failure<long>(CostCenterTypeErrores.Duplicado);
     }

     bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
     if (existsName)
     {
       return Result.Failure<long>(CostCenterTypeErrores.Duplicado);
     }

        var entity = CostCenterType.Create(
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
            _logger.LogInformation("CostCenterType con ID {CostCenterTypeId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear CostCenterType");
            return Result.Failure<long>(CostCenterTypeErrores.ErrorSave);
        }
    }
}
