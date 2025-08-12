using System.Text.Json;
using System.Text.Json.Serialization;
using static NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos.EstudioAmbientalDto;

namespace NexaSoft.Agro.Infrastructure.Serialization;

[JsonSerializable(typeof(EstudioAmbientalDtoResponse))]
[JsonSerializable(typeof(CapituloDtoResponse))]
[JsonSerializable(typeof(SubCapituloDtoResponse))]
[JsonSerializable(typeof(EstructuraDtoResponse))]
[JsonSerializable(typeof(ArchivoDtoResponse))]
[JsonSerializable(typeof(PlanoDtoResponse))]
[JsonSerializable(typeof(PlanoDetalleDtoResponse))]
// Agrega aquí todos los tipos que necesites serializar
public partial class AppJsonContext : JsonSerializerContext 
{
}

// 2. Configuración global de JSON (opcional)
public static class JsonConfig
{
    public static readonly JsonSerializerOptions Options = new()
    {
        TypeInfoResolver = AppJsonContext.Default,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true, // Solo para desarrollo
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}