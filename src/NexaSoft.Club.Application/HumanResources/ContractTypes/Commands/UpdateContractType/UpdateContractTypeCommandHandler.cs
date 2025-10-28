using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.ContractTypes;

namespace NexaSoft.Club.Application.HumanResources.ContractTypes.Commands.UpdateContractType;

public class UpdateContractTypeCommandHandler(
    IGenericRepository<ContractType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateContractTypeCommandHandler> _logger
) : ICommandHandler<UpdateContractTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateContractTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de ContractType con ID {ContractTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("ContractType con ID {ContractTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(ContractTypeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.Description,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("ContractType con ID {ContractTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar ContractType con ID {ContractTypeId}", command.Id);
            return Result.Failure<bool>(ContractTypeErrores.ErrorEdit);
        }
    }
}
