using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.ContractTypes;

namespace NexaSoft.Club.Application.HumanResources.ContractTypes.Commands.DeleteContractType;

public class DeleteContractTypeCommandHandler(
    IGenericRepository<ContractType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteContractTypeCommandHandler> _logger
) : ICommandHandler<DeleteContractTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteContractTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de ContractType con ID {ContractTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("ContractType con ID {ContractTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(ContractTypeErrores.NoEncontrado);
            }

         entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(),command.DeletedBy);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al eliminar ContractType con ID {ContractTypeId}", command.Id);
            return Result.Failure<bool>(ContractTypeErrores.ErrorDelete);
        }
    }
}
