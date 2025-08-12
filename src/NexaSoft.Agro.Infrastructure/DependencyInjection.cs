using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexaSoft.Agro.Application.Abstractions.Auth;
using NexaSoft.Agro.Application.Abstractions.Data;
using NexaSoft.Agro.Application.Abstractions.Email;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Features.Organizaciones;
using NexaSoft.Agro.Application.Features.Proyectos.Archivos;
using NexaSoft.Agro.Application.Features.Proyectos.Estructuras;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales;
using NexaSoft.Agro.Application.Features.Proyectos.Planos;
using NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores;
using NexaSoft.Agro.Application.Masters.MenuItems;
using NexaSoft.Agro.Application.Masters.Roles;
using NexaSoft.Agro.Application.Masters.Users;
using NexaSoft.Agro.Application.Storages;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Infrastructure.Abstractions.Auth;
using NexaSoft.Agro.Infrastructure.Abstractions.Data;
using NexaSoft.Agro.Infrastructure.Abstractions.Email;
using NexaSoft.Agro.Infrastructure.Abstractions.Time;
using NexaSoft.Agro.Infrastructure.ConfigSettings;
using NexaSoft.Agro.Infrastructure.Repositories;
using NexaSoft.Agro.Infrastructure.Serialization;
using Npgsql;

namespace NexaSoft.Agro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(
           this IServiceCollection services,
           IConfiguration configuration
       )
    {

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.Configure<StorageOptions>(configuration.GetSection("Storage"));


        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("Database")
         ?? throw new ArgumentNullException(nameof(configuration));


        services.AddDbContext<ApplicationDbContext>(
         options =>
         {
             /*options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)).UseSnakeCaseNamingConvention();*/
             options.UseNpgsql(connectionString, x => x.UseNetTopologySuite())
                .UseSnakeCaseNamingConvention();
                

         });

        services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
        services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
        services.AddScoped<IEstudioAmbientalRepository, EstudioAmbientalRepository>();
        services.AddScoped<IOrganizacionRepository, OrganizacionRepository>();
        services.AddScoped<IArchivoRepository, ArchivoRepository>();
        services.AddScoped<IEstructuraRepository, EstructuraRepository>();
        services.AddScoped<IPlanoRepository, PlanoRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddSingleton<IFileStorageService, MinioStorageService>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>
        (
            _ => new SqlConnectionFactory(connectionString)
        );

        // Repositorios
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.Configure<JsonSerializerOptions>(options =>
        {
            options.TypeInfoResolver = AppJsonContext.Default;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        return services;

    }
}