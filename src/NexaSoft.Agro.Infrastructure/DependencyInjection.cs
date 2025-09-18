using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NexaSoft.Agro.Application.Abstractions.Auth;
using NexaSoft.Agro.Application.Abstractions.Data;
using NexaSoft.Agro.Application.Abstractions.Email;
using NexaSoft.Agro.Application.Abstractions.Excel;
using NexaSoft.Agro.Application.Abstractions.Reporting;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Features.Organizaciones;
using NexaSoft.Agro.Application.Features.Proyectos.Archivos;
using NexaSoft.Agro.Application.Features.Proyectos.Estructuras;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudiosReport;
using NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios;
using NexaSoft.Agro.Application.Features.Proyectos.Planos;
using NexaSoft.Agro.Application.Masters.Constantes;
using NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores;
using NexaSoft.Agro.Application.Masters.MenuItems;
using NexaSoft.Agro.Application.Masters.Roles;
using NexaSoft.Agro.Application.Masters.Users;
using NexaSoft.Agro.Application.Storages;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Infrastructure.Abstractions.Auth;
using NexaSoft.Agro.Infrastructure.Abstractions.Data;
using NexaSoft.Agro.Infrastructure.Abstractions.Email;
using NexaSoft.Agro.Infrastructure.Abstractions.Excel;
using NexaSoft.Agro.Infrastructure.Abstractions.Time;
using NexaSoft.Agro.Infrastructure.ConfigSettings;
using NexaSoft.Agro.Infrastructure.Repositories;
using NexaSoft.Agro.Infrastructure.Repositories.Reports;
using NexaSoft.Agro.Infrastructure.Serialization;
using NexaSoft.Agro.Infrastructure.Services;
using Npgsql;
using QuestPDF.Infrastructure;

namespace NexaSoft.Agro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(
           this IServiceCollection services,
           IConfiguration configuration
       )
    {

        // -----------------------------------
        // 1. Cargar secretos desde Vault
        // -----------------------------------

        var environment = "qa"; // puede ser "dev", "qa", "prod"


        var vaultAddress = configuration["Vault:Address"] ?? "http://127.0.0.1:18200";
        var vaultToken = Environment.GetEnvironmentVariable("VAULT_TOKEN");

        if (string.IsNullOrEmpty(vaultToken))
        {
            var tokenFilePath = Environment.GetEnvironmentVariable("VAULT_TOKEN_FILE") ?? "/run/secrets/vault_token";
            if (File.Exists(tokenFilePath))
            {
                vaultToken = File.ReadAllText(tokenFilePath).Trim();
            }
        }

        if (string.IsNullOrEmpty(vaultToken))
        {
            throw new InvalidOperationException("VAULT_TOKEN no definido ni encontrado en archivo.");
        }


        var vaultService = new VaultService(vaultAddress, vaultToken);

        var dbSecrets = vaultService.GetSecretsAsync($"{environment}/db").Result;
        var jwtSettings = vaultService.GetSecretAsObjectAsync<JwtOptions>($"{environment}/jwt").Result!;
        var storageSettings = vaultService.GetSecretAsObjectAsync<StorageOptions>($"{environment}/storage").Result!;

        var brevoEmailOptions = vaultService.GetSecretAsObjectAsync<BrevoOptions>($"{environment}/mail").Result!;

        services.Configure<BrevoOptions>(o =>
        {
            o.ApiKey = brevoEmailOptions.ApiKey;
            o.FromEmail = brevoEmailOptions.FromEmail;
            o.FromName = brevoEmailOptions.FromName;
        });



        /*Console.WriteLine("🔐 Vault DB Secrets:");
        Console.WriteLine($"Host: {dbSecrets["Host"]}");
        Console.WriteLine($"Username: {dbSecrets["Username"]}");
        // Console.WriteLine($"Password: {dbSecrets["Password"]}"); // ⚠️ Evitar en producción
        Console.WriteLine($"Database: {dbSecrets["Database"]}");*/

        /*var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        var dbHost = isDocker ? "host.docker.internal" : dbSecrets["Host"] ;//"localhost";
                                 
        var connectionString = $"Host={dbHost};" +//$"Host={dbSecrets["Host"]};" +*/
        var connectionString = $"Host={dbSecrets["Host"]};" +
                               $"Username={dbSecrets["Username"]};" +
                               $"Password={dbSecrets["Password"]};" +
                               $"Database={dbSecrets["Database"]}";

        // -----------------------------------
        // 2. Configurar IConfiguration extendido con secrets
        // -----------------------------------
        var extraSettings = new Dictionary<string, string>
        {
            { "ConnectionStrings:Database", connectionString },
            { "Jwt:Issuer", jwtSettings.Issuer },
            { "Jwt:Secret", jwtSettings.Secret },
            { "Jwt:Expires", jwtSettings.Expires },
            { "Storage:AccessKey", storageSettings.AccessKey },
            { "Storage:SecretKey", storageSettings.SecretKey },
            { "Storage:ServiceUrl", storageSettings.ServiceUrl },
            { "Storage:Bucket", storageSettings.Bucket }
        };

        var configurationBuilder = new ConfigurationBuilder()
            .AddConfiguration(configuration)
            .AddInMemoryCollection(extraSettings!);

        var builtConfiguration = configurationBuilder.Build();

        services.AddSingleton<IConfiguration>(builtConfiguration);

        // -----------------------------------
        // 3. Registrar servicios
        // -----------------------------------
        services.Configure<JwtOptions>(builtConfiguration.GetSection("Jwt"));
        services.Configure<StorageOptions>(builtConfiguration.GetSection("Storage"));

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        //services.AddTransient<IEmailService, EmailService>();
        //services.AddHttpClient<IEmailService, EmailService>();
        // ✅ Configuración del HttpClient
        services.AddHttpClient("BrevoClient", (serviceProvider, client) =>
        {
            var brevoOptions = serviceProvider.GetRequiredService<IOptions<BrevoOptions>>().Value;

            client.BaseAddress = new Uri("https://api.brevo.com/v3/");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", brevoOptions.ApiKey);
            client.DefaultRequestHeaders.Add("accept", "application/json");
        });

        // ✅ Registro del servicio CON IOptions
        services.AddScoped<IEmailService>(serviceProvider =>
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("BrevoClient");
            var brevoOptions = serviceProvider.GetRequiredService<IOptions<BrevoOptions>>();

            return new EmailService(httpClient, brevoOptions);
        });
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();

        // -----------------------------------
        // 4. PostgreSQL DbContext
        // -----------------------------------
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var conn = builtConfiguration.GetConnectionString("Database");
            options.UseNpgsql(conn, npgsqlOptions => npgsqlOptions.UseNetTopologySuite())
                   .UseSnakeCaseNamingConvention();
        });

        // -----------------------------------
        // 5. Servicios y Repos
        // -----------------------------------
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddSingleton<ISqlConnectionFactory>(_ =>
        {
            var conn = builtConfiguration.GetConnectionString("Database")
                      ?? throw new InvalidOperationException("No se encontró la cadena de conexión.");
            return new SqlConnectionFactory(conn);
        });

        services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
        services.AddScoped<IEstudioAmbientalRepository, EstudioAmbientalRepository>();
        services.AddScoped<IOrganizacionRepository, OrganizacionRepository>();
        services.AddScoped<IArchivoRepository, ArchivoRepository>();
        services.AddScoped<IEstructuraRepository, EstructuraRepository>();
        services.AddScoped<IPlanoRepository, PlanoRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddScoped<IEventoRegulatorioRepository, EventoRegulatorioRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped(typeof(IPdfReportGenerator<>), typeof(PdfReportGenerator<>));
        services.AddScoped<IConstantsPdfReportGenerator, ConstantsPdfReportGenerator>();
        services.AddScoped<IStudyTreePdfReportGenerator, StudyTreePdfReportGenerator>();

        services.AddSingleton<IFileStorageService, MinioStorageService>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericExcelImporter<>), typeof(GenericExcelImporter<>));


        // -----------------------------------
        // 6. JSON config
        // -----------------------------------
        services.Configure<JsonSerializerOptions>(options =>
        {
            options.TypeInfoResolver = AppJsonContext.Default;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        QuestPDF.Settings.License = LicenseType.Community;
        QuestPDF.Settings.EnableDebugging = true;

        return services;
    }
}