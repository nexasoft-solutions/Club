using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.CostCenters;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.CostCenters.Commands.CreateCostCenter;

public class CreateCostCenterCommandHandler(
    IGenericRepository<CostCenter> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateCostCenterCommandHandler> _logger
) : ICommandHandler<CreateCostCenterCommand, long>
{
    public async Task<Result<long>> Handle(CreateCostCenterCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de CostCenter");

        bool existsCode = await _repository.ExistsAsync(c => c.Code == command.Code, cancellationToken);
        if (existsCode)
        {
            return Result.Failure<long>(CostCenterErrores.Duplicado);
        }

        bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
        if (existsName)
        {
            return Result.Failure<long>(CostCenterErrores.Duplicado);
        }

        var entity = CostCenter.Create(
            command.Code,
            command.Name,
            command.ParentCostCenterId,
            command.CostCenterTypeId,
            command.Description,
            command.ResponsibleId,
            command.Budget,
            command.StartDate,
            command.EndDate,
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
            _logger.LogInformation("CostCenter con ID {CostCenterId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear CostCenter");
            return Result.Failure<long>(CostCenterErrores.ErrorSave);
        }
    }
}
