using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Application.Masters.Users;


namespace NexaSoft.Club.Infrastructure.Background;

public class MemberUserBackgroundService: BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<MemberUserBackgroundService> _logger;
    private readonly TimeSpan _checkInterval = TimeSpan.FromHours(1);

    public MemberUserBackgroundService(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<MemberUserBackgroundService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Servicio de verificación de usuarios de miembros iniciado");
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await GeneratePendingMemberUsersAsync(stoppingToken);
                await Task.Delay(_checkInterval, stoppingToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(ex, "Error en servicio de generación de usuarios de miembros");
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        _logger.LogInformation("Servicio de verificación de usuarios de miembros detenido");
    }

    private async Task GeneratePendingMemberUsersAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRoleRepository>();
        var userGenerator = scope.ServiceProvider.GetRequiredService<IMemberUserBackgroundGenerator>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MemberUserBackgroundService>>();

        try
        {
            logger.LogInformation("Buscando miembros pendientes de usuario...");

            var users = await userRepository.MembersPendingUserSpec();

            logger.LogInformation("{Count} miembros encontrados para generación de usuario", users.Count);

            foreach (var user in users)
            {
                try
                {
                    await userGenerator.GenerateMemberUserAsync(user.MemberId, user.UserId, user.CreatedBy!, cancellationToken);
                    logger.LogInformation("Usuario generado para miembro {MemberId}", user.MemberId);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error generando usuario para miembro {MemberId}", user.MemberId);
                }

                await Task.Delay(100, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error en la ejecución del generador de usuarios de miembros");
            throw;
        }
    }
}
