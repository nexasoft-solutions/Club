using NexaSoft.Agro.Api.Middleware;
using NexaSoft.Agro.Infrastructure;
using Microsoft.EntityFrameworkCore;



namespace NexaSoft.Agro.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static async void ApplyMigrations(
        this IApplicationBuilder app
    )
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();  // Mantén el loggerFactory aquí
            var logger = loggerFactory.CreateLogger<Program>();  // Mover la creación del logger fuera del bloque using

            try
            {
                var context = service.GetRequiredService<ApplicationDbContext>();

                // Aplica las migraciones
                await context.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en la migración de la capa de dominio a la base de datos");
                throw;
            }
        }
    }
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
