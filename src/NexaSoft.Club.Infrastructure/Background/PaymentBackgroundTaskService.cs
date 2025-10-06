using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Features.Payments.Background;
using NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;
using NexaSoft.Club.Application.Features.Payments.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Payments;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Infrastructure.Background;

public class PaymentBackgroundTaskService: IPaymentBackgroundTaskService
{
    private readonly ILogger<PaymentBackgroundTaskService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5, 10); // Control de concurrencia

    public PaymentBackgroundTaskService(
        ILogger<PaymentBackgroundTaskService> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task QueuePaymentProcessingAsync(long paymentId, CreatePaymentCommand command, CancellationToken cancellationToken = default)
    {
        // Ejecutar inmediatamente en background
        _ = Task.Run(async () =>
        {
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                await ProcessPaymentInBackgroundAsync(paymentId, command, cancellationToken);
            }
            finally
            {
                _semaphore.Release();
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    private async Task ProcessPaymentInBackgroundAsync(long paymentId, CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        try
        {
            _logger.LogInformation("Iniciando procesamiento background para pago {PaymentId}", paymentId);

            var paymentProcessor = scope.ServiceProvider.GetRequiredService<IPaymentBackgroundProcessor>();
            await paymentProcessor.ProcessPaymentAsync(paymentId, command, cancellationToken);

            _logger.LogInformation("Procesamiento background completado para pago {PaymentId}", paymentId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en procesamiento background para pago {PaymentId}", paymentId);

            // Marcar el pago como fallado en background
            //await MarkPaymentAsFailedAsync(paymentId, ex.Message, scope.ServiceProvider, cancellationToken);
            // COMPENSACIÓN: Revertir los cambios del Command Handler
            await CompensatePaymentFailureAsync(paymentId, command, ex.Message, scope.ServiceProvider, cancellationToken);
        }
    }
    
    private async Task CompensatePaymentFailureAsync(
        long paymentId, 
        CreatePaymentCommand command, 
        string error, 
        IServiceProvider serviceProvider, 
        CancellationToken cancellationToken)
    {
        IUnitOfWork? unitOfWork = null;
        
        try
        {
            var paymentRepository = serviceProvider.GetRequiredService<IGenericRepository<Payment>>();
            var memberRepository = serviceProvider.GetRequiredService<IGenericRepository<Member>>();
            unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. Revertir balance del miembro
            var member = await memberRepository.GetByIdAsync(command.MemberId, cancellationToken);
            if (member != null)
            {
                member.RevertBalance(command.Amount);//, "System - Rollback por falla en procesamiento");
                await memberRepository.UpdateAsync(member);
                _logger.LogInformation("Balance revertido para miembro {MemberId}", member.Id);
            }

            // 2. Marcar payment como fallado
            var payment = await paymentRepository.GetByIdAsync(paymentId, cancellationToken);
            if (payment != null)
            {
                payment.MarkAsFailed(); //($"Error en procesamiento: {error}");
                await paymentRepository.UpdateAsync(payment);
                _logger.LogInformation("Payment {PaymentId} marcado como fallado", paymentId);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            _logger.LogWarning("Compensación completada para payment fallido {PaymentId}", paymentId);
        }
        catch (Exception compensationEx)
        {
            _logger.LogCritical(compensationEx, 
                "ERROR CRÍTICO: No se pudo compensar payment {PaymentId}. Se requiere intervención manual.", 
                paymentId);
            
            // Rollback de la transacción de compensación si existe
            if (unitOfWork != null)
            {
                try
                {
                    await unitOfWork.RollbackAsync(cancellationToken);
                }
                catch (Exception rollbackEx)
                {
                    _logger.LogError(rollbackEx, "Error haciendo rollback de la compensación");
                }
            }
            
            await NotifyAdminAsync(paymentId, compensationEx.Message, serviceProvider, cancellationToken);
        }
    }

    private async Task NotifyAdminAsync(long paymentId, string error, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        try
        {
            // Aquí implementarías tu sistema de notificación a administradores
            // Por ejemplo: email, Slack, SMS, etc.
            _logger.LogError(
                "🚨 ALERTA ADMINISTRADOR: Payment {PaymentId} en estado inconsistente. " +
                "Error en compensación: {Error}. Se requiere intervención manual.", 
                paymentId, error);
            
            // Ejemplo con un servicio de notificación (deberías crear este servicio)
            // var notificationService = serviceProvider.GetService<IAdminNotificationService>();
            // if (notificationService != null)
            // {
            //     await notificationService.NotifyPaymentInconsistencyAsync(paymentId, error, cancellationToken);
            // }
            
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error notificando al administrador sobre payment {PaymentId}", paymentId);
        }
    }



    /*private async Task MarkPaymentAsFailedAsync(long paymentId, string error, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        try
        {
            var paymentRepository = serviceProvider.GetRequiredService<IGenericRepository<Payment>>();
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            var payment = await paymentRepository.GetByIdAsync(paymentId, cancellationToken);
            if (payment != null)
            {
                payment.MarkAsFailed();
                await paymentRepository.UpdateAsync(payment);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marcando pago {PaymentId} como fallado", paymentId);
        }
    }*/
}