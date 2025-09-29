using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Reservations;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler(
    IGenericRepository<Reservation> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateReservationCommandHandler> _logger
) : ICommandHandler<CreateReservationCommand, long>
{
    public async Task<Result<long>> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Reservation");

        var entity = Reservation.Create(
            command.MemberId,
            command.SpaceId,
            command.StartTime,
            command.EndTime,
            command.Status,
            command.TotalAmount,
            command.AccountingEntryId,
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
            _logger.LogInformation("Reservation con ID {ReservationId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Reservation");
            return Result.Failure<long>(ReservationErrores.ErrorSave);
        }
    }
}
