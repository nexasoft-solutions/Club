using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Templates;

public static class TemplateDataExtensions
{
    public static TemplateData ForEventoRegulatorio(this TemplateData data, dynamic entity)
    {
        try
        {
            string nombreEvento = GetPropertySafe(entity, "NombreEvento");
            string proyecto = GetPropertySafe(entity, "Proyecto");
            string responsable = GetPropertySafe(entity, "Responsable");
            string descripcion = GetPropertySafe(entity, "Descripcion");

            // Manejo seguro de DateOnly
            string fechaVencimiento = FormatDateSafe(entity, "FechaVencimiento");

            // Manejo seguro de int?
            string notificarDias = entity.NotificarDíasAntes != null ?
                $"{entity.NotificarDíasAntes} días antes" : "No configurado";

            // Icono según estado
            string estado = GetPropertySafe(entity, "EstadoEvento")?.ToLower() ?? "pendiente";
            string iconoEstado = GetIconoPorEstado(estado);
            string estadoFormateado = CapitalizeFirstLetter(estado);

            data.Details.AddRange(new[]
            {
                new KeyValuePair<string, string>("📋 Evento", nombreEvento),
                new KeyValuePair<string, string>("🏢 Proyecto", proyecto),
                new KeyValuePair<string, string>("👤 Responsable", responsable),
                new KeyValuePair<string, string>("📅 Vencimiento", fechaVencimiento),
                new KeyValuePair<string, string>("🔔 Recordatorios", notificarDias),
                new KeyValuePair<string, string>($"{iconoEstado} Estado", estadoFormateado),
                new KeyValuePair<string, string>("📝 Descripción",
                    string.IsNullOrEmpty(descripcion) ? "Sin descripción" : descripcion)
            });
        }
        catch (Exception ex)
        {
            data.Details.Add(new KeyValuePair<string, string>("⚠️ Error",
                "No se pudieron cargar todos los detalles del evento " + ex));
        }

        return data;
    }

    private static string FormatDateSafe(dynamic obj, string propertyName)
    {
        try
        {
            var value = obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
            if (value == null) return "N/A";

            // Funciona para DateTime, DateOnly, DateTime?
            return value is DateOnly dateOnly ?
                dateOnly.ToString("dd/MM/yyyy") :
                Convert.ToDateTime(value).ToString("dd/MM/yyyy");
        }
        catch
        {
            return "N/A";
        }
    }

    private static string GetIconoPorEstado(string estado)
    {
        return estado.ToLower() switch
        {
            "pendiente" => "⏳",
            "programado" => "📌",
            "presentado" => "✅",
            "aprobado" => "🎯",
            "reprogramado" => "🔄",
            "rechazado" => "❌",
            //"en revision" or "en revisión" => "🔍",
            //"completado" => "🏁",
            "vencido" => "⚠️",
            "cancelado" => "🚫",
            _ => "📌"
        };
    }

    private static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToUpper(input[0]) + input[1..].ToLower();
    }

    private static string GetPropertySafe(dynamic obj, string propertyName)
    {
        try
        {
            var value = obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
            return value?.ToString() ?? "N/A";
        }
        catch
        {
            return "N/A";
        }
    }

    // ✅ Puedes agregar más métodos para diferentes entidades
    public static TemplateData ForProyecto(this TemplateData data, dynamic proyecto)
    {
        data.Details.AddRange(new[]
        {
            new KeyValuePair<string, string>("🏢 Proyecto", proyecto.Nombre),
            new KeyValuePair<string, string>("👤 Manager", proyecto.Manager),
            new KeyValuePair<string, string>("📅 Inicio", proyecto.FechaInicio.ToString("dd/MM/yyyy")),
            new KeyValuePair<string, string>("🏁 Fin", proyecto.FechaFin.ToString("dd/MM/yyyy"))
        });

        return data;
    }

    public static TemplateData ForActivacionPin(this TemplateData data, string nombre, string apellido, string email, string pin, DateTime? fechaExpiracion = null)
    {
        data.Details.AddRange(new[]
        {
        new KeyValuePair<string, string>("👤 Nombre", $"{nombre} {apellido}"),
        new KeyValuePair<string, string>("📧 Correo", email),
        new KeyValuePair<string, string>("🔐 PIN de Activación", $"<strong style='font-size:20px'>{pin}</strong>"),
        new KeyValuePair<string, string>("🕒 Válido hasta", fechaExpiracion?.ToString("dd/MM/yyyy") ?? "No definido")
    });

        return data;
    }

}
