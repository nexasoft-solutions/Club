using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace NexaSoft.Club.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        var openApi = new OpenApiInfo
        {
            Title = "NexaSoft Solutions API-Club",//"MVP-Colvida",//
            Version = "V1",
            Description = "Sistema de Gestion del Centro Social Ica API 2025", //"Sistema de Gestion de Seguros API 2025",//
            TermsOfService = new Uri("https://opensource.org/licenses/NIT"),
            Contact = new OpenApiContact
            {
                /*Name = "SEGUROS COLVIDA S.A.",
                Email = "colvidacontato@colvidaseguros.com.ec",
                Url = new Uri("https://colvidaseguros.com.ec")*/
                Name = "NEXASOFT SOLUTIONS S.A.C.",
                Email = "arobles@nexasoft.com.pe",
                Url = new Uri("https://nexasoft.com.pe")
            },
            License = new OpenApiLicense
            {
                Name = "Use under LICX",
                Url = new Uri("https://opensource.org/licenses/")
            }
        };

        services.AddSwaggerGen(x =>
        {
            openApi.Version = "v1";
            x.SwaggerDoc("v1", openApi);

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "JWT Bearer Token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
             { securityScheme, new string[]{ } }
            });

            x.MapType<IFormFile>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            });

            /*x.MapType<string>(() => new OpenApiSchema { Nullable = true });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            x.IncludeXmlComments(xmlPath);*/
        });

        return services;
    }
}
