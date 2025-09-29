using NexaSoft.Club.Api.Middleware;
using NexaSoft.Club.Infrastructure;
using Microsoft.EntityFrameworkCore;



namespace NexaSoft.Club.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var service = scope.ServiceProvider;
            var logger = service.GetRequiredService<ILogger<Program>>();
            var context = service.GetRequiredService<ApplicationDbContext>();

            try
            {
                // Verificar si la base de datos existe
                if (!await context.Database.CanConnectAsync())
                {
                    logger.LogInformation("Creando base de datos...");
                    await context.Database.EnsureCreatedAsync();
                }

                // Obtener migraciones pendientes
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                {
                    logger.LogInformation("Aplicando {Count} migraciones pendientes...", pendingMigrations.Count());
                    await context.Database.MigrateAsync();
                    logger.LogInformation("✅ Migraciones aplicadas correctamente.");
                }
                else
                {
                    logger.LogInformation("✅ No hay migraciones pendientes.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ Error aplicando migraciones");
                throw;
            }
        }
    }
    /*public static async void ApplyMigrations(
        this IApplicationBuilder app
    )
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            try
            {
                var context = service.GetRequiredService<ApplicationDbContext>();
                //var schema = "club"; // O obtén esto de tu configuración

                // Verificar si las migraciones ya fueron aplicadas
                var migrationsApplied = await context.Database.GetAppliedMigrationsAsync();
                if (migrationsApplied.Any())
                {
                    logger.LogInformation("✅ Las migraciones ya fueron aplicadas anteriormente.");
                    return;
                }

                // Aplica las migraciones solo si no se han aplicado
                await context.Database.MigrateAsync();
                logger.LogInformation("✅ Migraciones aplicadas correctamente.");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en la migración de la capa de dominio a la base de datos");
                throw;
            }
        }
    }*/
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
