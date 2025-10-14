using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Features.Reservations.Background;
using NexaSoft.Club.Application.Features.Reservations.Commands.CreateReservation;
using NexaSoft.Club.Application.Features.Reservations.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Reservations;

namespace NexaSoft.Club.Infrastructure.Background;

public class ReservationBackgroundTaskService: IReservationBackgroundTaskService
{
    private readonly ILogger<ReservationBackgroundTaskService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5, 10); // Control de concurrencia

    public ReservationBackgroundTaskService(
        ILogger<ReservationBackgroundTaskService> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task QueueReservationProcessingAsync(long reservationId, CreateReservationCommand command, CancellationToken cancellationToken = default)
    {
        // Ejecutar inmediatamente en background
        _ = Task.Run(async () =>
        {
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                await ProcessReservationInBackgroundAsync(reservationId, command, cancellationToken);
            }
            finally
            {
                _semaphore.Release();
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    private async Task ProcessReservationInBackgroundAsync(long reservationId, CreateReservationCommand command, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        try
        {
            _logger.LogInformation("Iniciando procesamiento background para reserva {ReservationId}", reservationId);

            var reservationProcessor = scope.ServiceProvider.GetRequiredService<IReservationBackgroundProcessor>();
            await reservationProcessor.ProcessReservationAsync(reservationId, command, cancellationToken);

            _logger.LogInformation("Procesamiento background completado para reserva {ReservationId}", reservationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en procesamiento background para reserva {ReservationId}", reservationId);

            // COMPENSACIÓN: Revertir la reserva
            await CompensateReservationFailureAsync(reservationId, command, ex.Message, scope.ServiceProvider, cancellationToken);
        }
    }
    
    private async Task CompensateReservationFailureAsync(
        long reservationId, 
        CreateReservationCommand command, 
        string error, 
        IServiceProvider serviceProvider, 
        CancellationToken cancellationToken)
    {
        IUnitOfWork? unitOfWork = null;
        
        try
        {
            var reservationRepository = serviceProvider.GetRequiredService<IGenericRepository<Reservation>>();
            unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. Marcar reserva como cancelada/fallida
            var reservation = await reservationRepository.GetByIdAsync(reservationId, cancellationToken);
            if (reservation != null)
            {
                reservation.MarkAsFailed(); // O Cancelada
                await reservationRepository.UpdateAsync(reservation);
                _logger.LogInformation("Reserva {ReservationId} marcada como fallida", reservationId);
            }

            // 2. COMMIT de la compensación
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            _logger.LogWarning("Compensación completada para reserva fallida {ReservationId}", reservationId);
        }
        catch (Exception compensationEx)
        {
            _logger.LogCritical(compensationEx, 
                "ERROR CRÍTICO: No se pudo compensar reserva {ReservationId}. Se requiere intervención manual.", 
                reservationId);
            
            // Rollback de la transacción de compensación si existe
            if (unitOfWork != null)
            {
                try
                {
                    await unitOfWork.RollbackAsync(cancellationToken);
                }
                catch (Exception rollbackEx)
                {
                    _logger.LogError(rollbackEx, "Error haciendo rollback de la compensación de reserva");
                }
            }
            
            await NotifyAdminAsync(reservationId, compensationEx.Message, serviceProvider, cancellationToken);
        }
    }

    private async Task NotifyAdminAsync(long reservationId, string error, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogError(
                "🚨 ALERTA ADMINISTRADOR: Reserva {ReservationId} en estado inconsistente. " +
                "Error en compensación: {Error}. Se requiere intervención manual.", 
                reservationId, error);
            
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error notificando al administrador sobre reserva {ReservationId}", reservationId);
        }
    }
}
