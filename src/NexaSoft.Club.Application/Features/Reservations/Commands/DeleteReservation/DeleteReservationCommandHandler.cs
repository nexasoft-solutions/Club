using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Reservations;

namespace NexaSoft.Club.Application.Features.Reservations.Commands.DeleteReservation;

public class DeleteReservationCommandHandler(
    IGenericRepository<Reservation> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteReservationCommandHandler> _logger
) : ICommandHandler<DeleteReservationCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteReservationCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Reservation con ID {ReservationId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Reservation con ID {ReservationId} no encontrado", command.Id);
            return Result.Failure<bool>(ReservationErrores.NoEncontrado);
        }

        entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(), command.DeletedBy);

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
            _logger.LogError(ex, "Error al eliminar Reservation con ID {ReservationId}", command.Id);
            return Result.Failure<bool>(ReservationErrores.ErrorDelete);
        }
    }
}
