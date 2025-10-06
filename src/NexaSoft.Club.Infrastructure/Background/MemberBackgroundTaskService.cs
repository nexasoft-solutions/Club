using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Features.Members.Background;
using NexaSoft.Club.Application.Features.Members.Commands.CreateMember;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Infrastructure.Background;

public class MemberBackgroundTaskService : IMemberBackgroundTaskService
{
    private readonly ILogger<MemberBackgroundTaskService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(3, 5); // Menos concurrencia para generación de cuotas

    public MemberBackgroundTaskService(
        ILogger<MemberBackgroundTaskService> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task QueueMemberFeesGenerationAsync(long memberId, CreateMemberCommand command, CancellationToken cancellationToken = default)
    {
         var backgroundData = new MemberFeesBackgroundData(
            memberId,
            command.MemberTypeId,
            command.JoinDate,
            command.ExpirationDate,
            command.CreatedBy
        );

        _ = Task.Run(async () =>
        {
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                await GenerateMemberFeesWithCompensationAsync(backgroundData, cancellationToken);
            }
            finally
            {
                _semaphore.Release();
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    private async Task GenerateMemberFeesWithCompensationAsync(MemberFeesBackgroundData data, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        try
        {
            _logger.LogInformation("Generando cuotas en background para member {MemberId}", data.MemberId);

            // 1. PRIMERO: Generar cuotas
            var feesGenerator = scope.ServiceProvider.GetRequiredService<IMemberFeesBackgroundGenerator>();
            await feesGenerator.GenerateMemberFeesAsync(data, cancellationToken);

            _logger.LogInformation("Cuotas generadas exitosamente para member {MemberId}", data.MemberId);

             // 2. LUEGO: Generar QR
            var qrGenerator = scope.ServiceProvider.GetRequiredService<IMemberQrBackgroundGenerator>();
            await qrGenerator.GenerateMemberQrAsync(data.MemberId, data.CreatedBy, cancellationToken);

            _logger.LogInformation("Procesamiento completado para member {MemberId}", data.MemberId);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generando cuotas para member {MemberId}", data.MemberId);

            // COMPENSACIÓN: Marcar member como fallado
            await CompensateMemberFeesFailureAsync(data.MemberId, ex.Message, scope.ServiceProvider, cancellationToken);
        }
    }

    private async Task CompensateMemberFeesFailureAsync(
        long memberId,
        string error,
        IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
    {
        IUnitOfWork? unitOfWork = null;

        try
        {
            var memberRepository = serviceProvider.GetRequiredService<IGenericRepository<Member>>();
            unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            // Marcar member como fallado en generación de cuotas
            var member = await memberRepository.GetByIdAsync(memberId, cancellationToken);
            if (member != null)
            {
                member.MarkAsFeesGenerationFailed();//($"Error generando cuotas: {error}");
                await memberRepository.UpdateAsync(member);
                _logger.LogInformation("Member {MemberId} marcado como fallado en generación de cuotas", memberId);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            _logger.LogWarning("Compensación completada para member {MemberId}", memberId);
        }
        catch (Exception compensationEx)
        {
            _logger.LogCritical(compensationEx,
                "ERROR CRÍTICO: No se pudo compensar member {MemberId}. Se requiere intervención manual.",
                memberId);

            if (unitOfWork != null)
            {
                try { await unitOfWork.RollbackAsync(cancellationToken); }
                catch { /* Ignored */ }
            }

            await NotifyAdminAsync(memberId, compensationEx.Message, serviceProvider, cancellationToken);
        }
    }

    private async Task NotifyAdminAsync(long memberId, string error, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        _logger.LogError(
            "🚨 ALERTA ADMIN: Member {MemberId} en estado inconsistente. " +
            "Error en generación de cuotas: {Error}. Se requiere intervención manual.",
            memberId, error);

        await Task.CompletedTask;
    }

    public void Dispose()
    {
        _semaphore?.Dispose();
    }
}
