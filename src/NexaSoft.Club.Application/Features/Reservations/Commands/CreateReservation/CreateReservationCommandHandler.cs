using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Reservations.Background;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Reservations;
using NexaSoft.Club.Domain.Masters.Contadores;
using NexaSoft.Club.Domain.Masters.DocumentTypes;
using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;
using NexaSoft.Club.Domain.Masters.SpaceRates;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler(
    IGenericRepository<Reservation> _reservationRepository,
    IGenericRepository<SpaceRate> _spaceRateRepository,
    IGenericRepository<SpaceAvailability> _spaceAvailabilityRepository,
    IGenericRepository<DocumentType> _documentTypeRepository,
    IGenericRepository<Contador> _contadorRepository,
    IReservationBackgroundTaskService _backgroundTaskService,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateReservationCommandHandler> _logger
) : ICommandHandler<CreateReservationCommand, ReservationResponse>
{
    public async Task<Result<ReservationResponse>> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de creación de Reservation para SpaceRateId: {SpaceRateId}", command.SpaceRateId);

        try
        {
            // 1. VALIDACIONES (SIN TRANSACCIÓN)
            var validationResult = await ValidateReservation(command, cancellationToken);
            if (validationResult.IsFailure)
                return Result.Failure<ReservationResponse>(validationResult.Error);

            var (spaceRate, spaceAvailability) = validationResult.Value;

            // 2. VERIFICAR DISPONIBILIDAD
            bool isAvailable = await CheckAvailability(command, spaceRate.SpaceId, cancellationToken);
            if (!isAvailable)
                return Result.Failure<ReservationResponse>(ReservationErrores.NoDisponible);

            // 3. TRANSACCIÓN CORTA: SOLO OPERACIONES CRÍTICAS
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 3.1 CREAR REGISTRO DE RESERVA
            var reservation = await CreateReservationRecord(command, spaceRate.Rate, cancellationToken);

            // 3.2 COMMIT RÁPIDO (sin marcar disponibilidad como ocupada)
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Reservation {ReservationId} creada, iniciando procesamiento background", reservation.Id);

            // 4. ENCOLAR PROCESAMIENTO BACKGROUND (PAGO Y ASIENTO CONTABLE)

            await _backgroundTaskService.QueueReservationProcessingAsync(reservation.Id, command, cancellationToken);

            // 5. RESPUESTA INMEDIATA
            var response = CreateReservationResponseImmediate(reservation);

            _logger.LogInformation("Reservation con ID {ReservationId} creada satisfactoriamente", reservation.Id);
            return Result.Success(response);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Reservation para SpaceRateId: {SpaceRateId}", command.SpaceRateId);
            return Result.Failure<ReservationResponse>(ReservationErrores.ErrorSave);
        }
    }


    private ReservationResponse CreateReservationResponseImmediate(Reservation reservation)
    {
        return new ReservationResponse(
            reservation.Id,
            reservation.ReceiptNumber ?? string.Empty,
            reservation.TotalAmount,
            reservation.Date,
            reservation.StartTime,
            reservation.EndTime,
            StatusEnum.Iniciado.ToString(), // Estado inicial
            "Pendiente", // Estado de pago
            "Pendiente"  // Estado de asiento contable
        );
    }

    private async Task<Result<(SpaceRate SpaceRate, SpaceAvailability SpaceAvailability)>> ValidateReservation(
        CreateReservationCommand command, CancellationToken cancellationToken)
    {
        // Validar SpaceRate
        var spaceRate = await _spaceRateRepository.GetByIdAsync(command.SpaceRateId, cancellationToken);
        if (spaceRate is null || !spaceRate.IsActive || spaceRate.StateSpaceRate == (int)EstadosEnum.Eliminado)
            return Result.Failure<(SpaceRate, SpaceAvailability)>(ReservationErrores.TarifaNoValida);

        // Validar SpaceAvailability
        var spec = new SpaceAvailabilitiesBySpaceSpec(command.SpaceAvailabilityId);
        var spaceAvailability = await _spaceAvailabilityRepository.GetEntityWithSpec(spec, cancellationToken);

        if (spaceAvailability is null || spaceAvailability.SpaceId != spaceRate.SpaceId)
            return Result.Failure<(SpaceRate, SpaceAvailability)>(ReservationErrores.DisponibilidadNoValida);

        // Validar que la fecha y horario coincidan con la disponibilidad
        if (!AreDatesCompatible(command, spaceAvailability))
            return Result.Failure<(SpaceRate, SpaceAvailability)>(ReservationErrores.HorarioNoCompatible);

        // Validar que el horario no exceda la máxima reservación permitida
        var duration = CalculateDuration(command.StartTime, command.EndTime);
        if (spaceAvailability.Space != null && duration.TotalHours > spaceAvailability.Space.MaxReservationHours)
            return Result.Failure<(SpaceRate, SpaceAvailability)>(ReservationErrores.TiempoExcedido);

        return Result.Success((spaceRate, spaceAvailability));
    }

    private bool AreDatesCompatible(CreateReservationCommand command, SpaceAvailability availability)
    {
        // Ambos usan el mismo formato: Domingo=0, Lunes=1, ..., Sábado=6
        var reservationDayOfWeek = (int)command.Date.DayOfWeek;

        Console.WriteLine($"Reservation DayOfWeek: {reservationDayOfWeek}, Availability DayOfWeek: {availability.DayOfWeek}");

        if (reservationDayOfWeek != availability.DayOfWeek)
            return false;

        // Convertir TimeSpan a TimeOnly para comparar
        var availabilityStartTime = TimeOnly.FromTimeSpan(availability.StartTime);
        var availabilityEndTime = TimeOnly.FromTimeSpan(availability.EndTime);

        // Validar que el horario esté dentro del rango disponible
        if (command.StartTime < availabilityStartTime || command.EndTime > availabilityEndTime)
            return false;

        return true;
    }

    private async Task<bool> CheckAvailability(CreateReservationCommand command, long spaceId, CancellationToken cancellationToken)
    {
        try
        {
            // Verificar si ya existe una reserva en el mismo espacio, fecha y horario
            var existingReservation = await _reservationRepository.ExistsAsync(
                r => r.SpaceRate!.SpaceId == spaceId &&
                     r.Date == command.Date &&
                     r.StatusId != (int)StatusEnum.Cancelado &&
                     ((r.StartTime <= command.StartTime && r.EndTime > command.StartTime) ||
                      (r.StartTime < command.EndTime && r.EndTime >= command.EndTime) ||
                      (r.StartTime >= command.StartTime && r.EndTime <= command.EndTime)),
                cancellationToken);

            return !existingReservation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verificando disponibilidad para SpaceId: {SpaceId}", spaceId);
            return false;
        }
    }

    private async Task<Reservation> CreateReservationRecord(CreateReservationCommand command, decimal rate, CancellationToken cancellationToken)
    {
        // Calcular el monto total basado en la tarifa y duración
        var duration = CalculateDuration(command.StartTime, command.EndTime);
        var totalAmount = rate * (decimal)duration.TotalHours;

        if (string.IsNullOrWhiteSpace(command.ReceiptNumber))
        {
            command = command with
            {
                ReceiptNumber = await GenerateUniqueReceiptNumber(command.DocumentTypeId, command.CreatedBy, cancellationToken)
            };
            _logger.LogInformation("Número de recibo generado: {ReceiptNumber}", command.ReceiptNumber);
        }


        var reservation = Reservation.Create(
            command.MemberId,
            command.SpaceRateId,
            command.SpaceAvailabilityId,
            command.Date,
            command.StartTime,
            command.EndTime,
            (int)StatusEnum.Iniciado, // Status inicial
            command.PaymentMethodId,
            command.ReferenceNumber,
            command.DocumentTypeId,
            command.ReceiptNumber,
            totalAmount, // Usar el monto calculado
            accountingEntryId: null, // Se generará en background
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        // Marcar como processing inmediatamente
        //reservation.MarkAsProcessing();

        await _reservationRepository.AddAsync(reservation, cancellationToken);

        return reservation;
    }

    // Método auxiliar para calcular duración entre TimeOnly
    private TimeSpan CalculateDuration(TimeOnly startTime, TimeOnly endTime)
    {
        if (endTime < startTime)
        {
            // Si termina al día siguiente (ej: 22:00 a 02:00)
            return endTime.ToTimeSpan() + TimeSpan.FromDays(1) - startTime.ToTimeSpan();
        }

        return endTime - startTime;
    }

    /*private async Task<string> GenerateUniqueReservationNumber(string createdBy, CancellationToken cancellationToken)
    {
        try
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(1000, 9999);
            return $"RES-{timestamp}-{random}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar número de reserva único");
            return $"RES-{DateTime.Now:yyyyMMddHHmmss}";
        }
    }*/

    private async Task<string> GenerateUniqueReceiptNumber(long DocumentTypeId, string createdBy, CancellationToken cancellationToken)
    {
        try
        {

            var documentType = await _documentTypeRepository.GetByIdAsync(DocumentTypeId, cancellationToken);
            if (documentType == null)
            {
                _logger.LogWarning("DocumentType con ID {DocumentTypeId} no encontrado. Usando valor por defecto para serie.", DocumentTypeId);
            }

            var contador = await _contadorRepository.GetEntityWithSpec(new ContadorRawSpec("DocumentType", documentType!.Serie), cancellationToken);

            if (contador == null)
            {
                var contadorNew = Contador.Create(
                    "DocumentType",
                    documentType!.Serie ?? "R-001",
                    1,
                    string.Empty,
                    "string",
                    8,
                    _dateTimeProvider.CurrentTime.ToUniversalTime(),
                    createdBy
                );

                await _contadorRepository.AddAsync(contadorNew, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                contador = contadorNew;
            }

            var nuevoCodigo = contador.Incrementar(_dateTimeProvider.CurrentTime.ToUniversalTime(), createdBy, null);
            return nuevoCodigo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar número de comprobando único");
            return $"R-001-{DateTime.Now:yyyyMMddHHmmss}";
        }
    }

}
