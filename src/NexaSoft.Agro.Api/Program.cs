using NexaSoft.Agro.Infrastructure;
using NexaSoft.Agro.Application;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Api.Middleware;
using NexaSoft.Agro.Infrastructure.ConfigSettings;
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
app.ApplyMigrations();

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

/*builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);


//builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
//builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngularDev", builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

//app.SeedData();

app.UseCustomExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<PermissionMiddleware>();

app.UseCors("AllowAngularDev");

app.MapControllers();
*/
// Genera 64 bytes (512 bits) - tamaño ideal para HMAC-SHA512
/*var key = new byte[64];
RandomNumberGenerator.Fill(key);

// Convierte a Base64 válido
string validKey = Convert.ToBase64String(key); 

Console.WriteLine("Clave generada (copia exactamente):");
Console.WriteLine(validKey);*/

//app.Run();

