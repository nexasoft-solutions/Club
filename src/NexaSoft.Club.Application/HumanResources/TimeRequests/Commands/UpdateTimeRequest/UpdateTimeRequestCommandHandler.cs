using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.TimeRequests;

namespace NexaSoft.Club.Application.HumanResources.TimeRequests.Commands.UpdateTimeRequest;

public class UpdateTimeRequestCommandHandler(
    IGenericRepository<TimeRequest> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateTimeRequestCommandHandler> _logger
) : ICommandHandler<UpdateTimeRequestCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateTimeRequestCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de TimeRequest con ID {TimeRequestId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("TimeRequest con ID {TimeRequestId} no encontrado", command.Id);
                return Result.Failure<bool>(TimeRequestErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.EmployeeId,
            command.TimeRequestTypeId,
            command.StartDate,
            command.EndDate,
            command.TotalDays,
            command.Reason,
            command.StatusId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("TimeRequest con ID {TimeRequestId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar TimeRequest con ID {TimeRequestId}", command.Id);
            return Result.Failure<bool>(TimeRequestErrores.ErrorEdit);
        }
    }
}
