using NexaSoft.Club.Infrastructure;
using NexaSoft.Club.Application;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Api.Middleware;
using NexaSoft.Club.Infrastructure.ConfigSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

// 👇 Asegúrate de registrar la configuración personalizada
builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(); // ya se configura por ConfigureJwtBearerOptions

builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//builder.Services.AddControllers();

builder.Services.AddControllers()
     .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new NetTopologySuite.IO.Converters.GeometryConverter());
    });

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Migraciones
//app.ApplyMigrations();
await app.ApplyMigrationsAsync();

// Errores globales
app.UseCustomExceptionHandler();

app.UseCors("AllowAngularDev"); // ✅ Debe ir antes de endpoints si frontend llama API

// 🔐 Seguridad
app.UseAuthentication();
app.UseAuthorization();

// Middleware de permisos/roles
app.UseMiddleware<PermissionMiddleware>();

app.MapControllers();

app.Run();

