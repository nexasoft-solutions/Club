using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Background;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriod;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Infrastructure.Background;

public class PayrollBackgroundTaskService: IPayrollBackgroundTaskService
{
    private readonly ILogger<PayrollBackgroundTaskService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5, 10); // Control de concurrencia

    public PayrollBackgroundTaskService(
        ILogger<PayrollBackgroundTaskService> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task QueuePayrollProcessingAsync(long payrollPeriodId, CreatePayrollPeriodCommand command, string periodName, CancellationToken cancellationToken = default)
    {
        // Ejecutar inmediatamente en background
        _ = Task.Run(async () =>
        {
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                await ProcessPayrollInBackgroundAsync(payrollPeriodId, command, periodName, cancellationToken);
            }
            finally
            {
                _semaphore.Release();
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    private async Task ProcessPayrollInBackgroundAsync(long payrollPeriodId, CreatePayrollPeriodCommand command, string periodName, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        try
        {
            _logger.LogInformation("🔄 Iniciando procesamiento background para planilla {PayrollPeriodId}", payrollPeriodId);

            var payrollProcessor = scope.ServiceProvider.GetRequiredService<IPayrollBackgroundProcessor>();
            await payrollProcessor.ProcessPayrollAsync(payrollPeriodId, command, periodName, cancellationToken);

            _logger.LogInformation("✅ Procesamiento background completado para planilla {PayrollPeriodId}", payrollPeriodId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error en procesamiento background para planilla {PayrollPeriodId}", payrollPeriodId);

            // COMPENSACIÓN: Revertir la planilla
            await CompensatePayrollFailureAsync(payrollPeriodId, command, ex.Message, scope.ServiceProvider, cancellationToken);
        }
    }
    
    private async Task CompensatePayrollFailureAsync(
        long payrollPeriodId, 
        CreatePayrollPeriodCommand command, 
        string error, 
        IServiceProvider serviceProvider, 
        CancellationToken cancellationToken)
    {
        IUnitOfWork? unitOfWork = null;
        
        try
        {
            var payrollPeriodRepository = serviceProvider.GetRequiredService<IGenericRepository<PayrollPeriod>>();
            unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. Marcar planilla como cancelada/fallida
            var payrollPeriod = await payrollPeriodRepository.GetByIdAsync(payrollPeriodId, cancellationToken);
            if (payrollPeriod != null)
            {
                payrollPeriod.MarkAsFailed();
                await payrollPeriodRepository.UpdateAsync(payrollPeriod);
                _logger.LogInformation("Planilla {PayrollPeriodId} marcada como fallida", payrollPeriodId);
            }

            // 2. COMMIT de la compensación
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            _logger.LogWarning("⚠️ Compensación completada para planilla fallida {PayrollPeriodId}", payrollPeriodId);
        }
        catch (Exception compensationEx)
        {
            _logger.LogCritical(compensationEx, 
                "🚨 ERROR CRÍTICO: No se pudo compensar planilla {PayrollPeriodId}. Se requiere intervención manual.", 
                payrollPeriodId);
            
            // Rollback de la transacción de compensación si existe
            if (unitOfWork != null)
            {
                try
                {
                    await unitOfWork.RollbackAsync(cancellationToken);
                }
                catch (Exception rollbackEx)
                {
                    _logger.LogError(rollbackEx, "Error haciendo rollback de la compensación de planilla");
                }
            }
            
            await NotifyAdminAsync(payrollPeriodId, compensationEx.Message, serviceProvider, cancellationToken);
        }
    }

    private async Task NotifyAdminAsync(long payrollPeriodId, string error, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogError(
                "🚨 ALERTA ADMINISTRADOR: Planilla {PayrollPeriodId} en estado inconsistente. " +
                "Error en compensación: {Error}. Se requiere intervención manual.", 
                payrollPeriodId, error);
            
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error notificando al administrador sobre planilla {PayrollPeriodId}", payrollPeriodId);
        }
    }
}