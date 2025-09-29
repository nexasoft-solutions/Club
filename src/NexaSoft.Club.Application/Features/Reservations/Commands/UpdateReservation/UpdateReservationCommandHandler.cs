using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Reservations;

namespace NexaSoft.Club.Application.Features.Reservations.Commands.UpdateReservation;

public class UpdateReservationCommandHandler(
    IGenericRepository<Reservation> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateReservationCommandHandler> _logger
) : ICommandHandler<UpdateReservationCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateReservationCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de Reservation con ID {ReservationId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Reservation con ID {ReservationId} no encontrado", command.Id);
                return Result.Failure<bool>(ReservationErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.MemberId,
            command.SpaceId,
            command.StartTime,
            command.EndTime,
            command.Status,
            command.TotalAmount,
            command.AccountingEntryId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Reservation con ID {ReservationId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar Reservation con ID {ReservationId}", command.Id);
            return Result.Failure<bool>(ReservationErrores.ErrorEdit);
        }
    }
}
