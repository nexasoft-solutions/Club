using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Infrastructure.Background;

public class QrRenewalBackgroundService : BackgroundService
{
   private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<QrRenewalBackgroundService> _logger;
    private readonly TimeSpan _checkInterval = TimeSpan.FromHours(24);

    public QrRenewalBackgroundService(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<QrRenewalBackgroundService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Servicio de renovación de QR iniciado");
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckAndRenewQrCodesAsync(stoppingToken);
                await Task.Delay(_checkInterval, stoppingToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(ex, "Error en servicio de renovación de QR");
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        _logger.LogInformation("Servicio de renovación de QR detenido");
    }

    private async Task CheckAndRenewQrCodesAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        
        var memberRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Member>>();
        var qrGenerator = scope.ServiceProvider.GetRequiredService<IMemberQrBackgroundGenerator>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<QrRenewalBackgroundService>>();

        try
        {
            logger.LogInformation("Iniciando verificación de QR codes...");

            var membersNeedingQr = await memberRepository.ListAsync(
                new MembersNeedingQrRenewalSpec(),
                cancellationToken);

            logger.LogInformation("Encontrados {Count} members que necesitan renovación de QR", membersNeedingQr.Count);

            foreach (var member in membersNeedingQr)
            {
                try
                {
                    await qrGenerator.GenerateMemberQrAsync(member.Id, member.CreatedBy!, cancellationToken);
                    logger.LogInformation("QR renovado para member {MemberId}", member.Id);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error renovando QR para member {MemberId}", member.Id);
                }

                await Task.Delay(100, cancellationToken);
            }

            logger.LogInformation("Verificación de QR codes completada");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error en verificación de QR codes");
            throw;
        }
    }
}
