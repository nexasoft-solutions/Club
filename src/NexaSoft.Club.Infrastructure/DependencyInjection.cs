using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NexaSoft.Club.Application.Abstractions.Auth;
using NexaSoft.Club.Application.Abstractions.Data;
using NexaSoft.Club.Application.Abstractions.Email;
using NexaSoft.Club.Application.Abstractions.Excel;
using NexaSoft.Club.Application.Abstractions.Reporting;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Members.Background;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Application.Features.Payments.Background;
using NexaSoft.Club.Application.Features.Payments.Services;
using NexaSoft.Club.Application.Features.Reservations.Background;
using NexaSoft.Club.Application.Features.Reservations.Services;
using NexaSoft.Club.Application.HumanResources.LegalParameters;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Background;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Services;
using NexaSoft.Club.Application.Masters.MenuItems;
using NexaSoft.Club.Application.Masters.Roles;
using NexaSoft.Club.Application.Masters.Users;
using NexaSoft.Club.Application.Storages;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Infrastructure.Abstractions.Auth;
using NexaSoft.Club.Infrastructure.Abstractions.Data;
using NexaSoft.Club.Infrastructure.Abstractions.Email;
using NexaSoft.Club.Infrastructure.Abstractions.Excel;
using NexaSoft.Club.Infrastructure.Abstractions.Time;
using NexaSoft.Club.Infrastructure.Auth;
using NexaSoft.Club.Infrastructure.Background;
using NexaSoft.Club.Infrastructure.ConfigSettings;
using NexaSoft.Club.Infrastructure.Repositories;
using NexaSoft.Club.Infrastructure.Repositories.Reports;
using NexaSoft.Club.Infrastructure.services;
using NexaSoft.Club.Infrastructure.Services;
using Npgsql;
using QuestPDF.Infrastructure;

namespace NexaSoft.Club.Infrastructure;

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

        var environment = "dev"; // puede ser "dev", "qa", "prod"
        var system = "club";
        var schema = "club";


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

        var dbSecrets = vaultService.GetSecretsAsync($"{environment}/{system}/db").Result;
        var schemaSecrets = vaultService.GetSecretsAsync($"{environment}/{system}/schemas/{schema}").Result;
        var jwtSettings = vaultService.GetSecretAsObjectAsync<JwtOptions>($"{environment}/{system}/jwt").Result!;
        var storageSettings = vaultService.GetSecretAsObjectAsync<StorageOptions>($"{environment}/{system}/storage").Result!;

        var brevoEmailOptions = vaultService.GetSecretAsObjectAsync<BrevoOptions>($"{environment}/mail").Result!;

        var reniecOptions = vaultService.GetSecretAsObjectAsync<ReniecOptions>($"{environment}/reniec").Result!;

        services.Configure<ReniecOptions>(o =>
        {
            o.ApiKey = reniecOptions.ApiKey;
            o.Url = reniecOptions.Url;
        });

        services.Configure<BrevoOptions>(o =>
        {
            o.ApiKey = brevoEmailOptions.ApiKey;
            o.FromEmail = brevoEmailOptions.FromEmail;
            o.FromName = brevoEmailOptions.FromName;
        });



        /*var connectionString = $"Host={dbSecrets["Host"]};" +
                               $"Username={dbSecrets["Username"]};" +
                               $"Password={dbSecrets["Password"]};" +
                               $"Database={dbSecrets["Database"]};" +
                               $"SearchPath={schemaSecrets["Schema"]}"; */

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = dbSecrets["Host"],
            Username = dbSecrets["Username"],
            Password = dbSecrets["Password"],
            Database = dbSecrets["Database"]
        };

        if (schemaSecrets.TryGetValue("Schema", out var schemafin))
        {
            builder.SearchPath = schemafin;
        }

        var connectionString = builder.ConnectionString;
        //Console.WriteLine("Connection string final: " + connectionString);

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
        /*services.AddDbContext<ApplicationDbContext>(options =>
        {
            var conn = builtConfiguration["ConnectionStrings:Database"];
            options.UseNpgsql(conn, npgsqlOptions => npgsqlOptions.UseNetTopologySuite())
                   .UseSnakeCaseNamingConvention();
        });*/

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var conn = builtConfiguration["ConnectionStrings:Database"];
            options.UseNpgsql(conn, npgsqlOptions =>
            {
                npgsqlOptions.UseNetTopologySuite();
                npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", schemaSecrets["Schema"]); // Especifica el esquema
            })
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


        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddScoped<IAuthService, AuthService>();
       
        //services.AddScoped<IStudyTreePdfReportGenerator, StudyTreePdfReportGenerator>();
        services.AddScoped<IReceiptThermalService, ReceiptThermalService>();
        services.AddScoped<IReceiptGenericService, ReceiptGenericService>();
        services.AddScoped<IPayrollReceiptService, PayrollReceiptService>();

        services.AddSingleton<IFileStorageService, MinioStorageService>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericExcelImporter<>), typeof(GenericExcelImporter<>));

        // Generadores background
        services.AddScoped<IMemberQrBackgroundGenerator, MemberQrBackgroundGenerator>();

        services.AddSingleton<IPaymentBackgroundTaskService, PaymentBackgroundTaskService>();
        services.AddScoped<IPaymentBackgroundProcessor, PaymentBackgroundProcessor>();
        services.AddSingleton<IMemberBackgroundTaskService, MemberBackgroundTaskService>();
        services.AddScoped<IMemberFeesBackgroundGenerator, MemberFeesBackgroundGenerator>();
        services.AddScoped<IMemberUserBackgroundGenerator, MemberUserBackgroundGenerator>();
        services.AddScoped<IPayrollBackgroundProcessor, PayrollBackgroundProcessor>();
        services.AddScoped<IPayrollBackgroundTaskService, PayrollBackgroundTaskService>();

        services.AddScoped<IReservationBackgroundTaskService, ReservationBackgroundTaskService>();
        services.AddScoped<IReservationBackgroundProcessor, ReservationBackgroundProcessor>();

        services.AddScoped<IMemberTokenService, MemberTokenService>();
        services.AddScoped<IMemberQrService, UserQrService>();

        services.AddScoped<ILegalParametersRepository, LegalParametersRepository>();

        services.AddScoped<IPayrollCalculationService, PayrollCalculationService>();

        services.AddHttpClient<IReniecService, ReniecService>((serviceProvider, client) =>
        {
            // Configuración adicional de HttpClient si es necesario
        });

        services.AddScoped<IReniecService>(serviceProvider =>
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient(nameof(ReniecService));
            var options = serviceProvider.GetRequiredService<IOptions<ReniecOptions>>().Value;
            return new ReniecService(httpClient, options);
        });

        // Para QR (Domain Events - No crítico)
        //services.AddScoped<INotificationHandler<MemberQrGenerationRequiredDomainEvent>, MemberQrGenerationEventHandler>();
        services.AddScoped<IQrGeneratorService, QrGeneratorService>();
        services.AddScoped<ISpacePhotoStorageService, SpacePhotoStorageService>();

        // Background service para renovación QR
        services.AddHostedService<QrRenewalBackgroundService>();
        services.AddHostedService<MemberUserBackgroundService>();




        // -----------------------------------
        // 6. JSON config
        // -----------------------------------
        /*services.Configure<JsonSerializerOptions>(options =>
        {
            options.TypeInfoResolver = AppJsonContext.Default;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });*/

        QuestPDF.Settings.License = LicenseType.Community;
        QuestPDF.Settings.EnableDebugging = true;

        return services;
    }
}