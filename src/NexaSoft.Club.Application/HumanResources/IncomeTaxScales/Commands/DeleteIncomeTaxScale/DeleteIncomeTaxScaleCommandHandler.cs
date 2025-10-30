using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;

namespace NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Commands.DeleteIncomeTaxScale;

public class DeleteIncomeTaxScaleCommandHandler(
    IGenericRepository<IncomeTaxScale> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteIncomeTaxScaleCommandHandler> _logger
) : ICommandHandler<DeleteIncomeTaxScaleCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteIncomeTaxScaleCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de IncomeTaxScale con ID {IncomeTaxScaleId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("IncomeTaxScale con ID {IncomeTaxScaleId} no encontrado", command.Id);
                return Result.Failure<bool>(IncomeTaxScaleErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar IncomeTaxScale con ID {IncomeTaxScaleId}", command.Id);
            return Result.Failure<bool>(IncomeTaxScaleErrores.ErrorDelete);
        }
    }
}
