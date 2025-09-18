using System.Text;
using NexaSoft.Agro.Application.Abstractions.Email;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Infrastructure.Abstractions.Email;

public class EmailTemplateService : IEmailTemplateService
{
    public string GenerateWelcomeEmail(string name, string organization)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px; text-align: center; }}
        .content {{ background: #ffffff; padding: 30px; border-radius: 0 0 5px 5px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
        .button {{ background: #667eea; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Bienvenido a NexaSoft Agro</h1>
        </div>
        <div class='content'>
            <h2>Estimado/a {name},</h2>
            <p>Le damos la más cordial bienvenida a la plataforma <strong>NexaSoft Agro</strong> de <strong>{organization}</strong>.</p>
            <p>Su cuenta ha sido configurada exitosamente y ya puede acceder a todas las funcionalidades del sistema.</p>
            <p style='text-align: center; margin: 30px 0;'>
                <a href='https://tu-app.nexasoft.com' class='button'>Acceder a la Plataforma</a>
            </p>
            <p>Para cualquier consulta, no dude en contactar a nuestro equipo de soporte.</p>
            <p><strong>Atentamente,</strong><br>Equipo NexaSoft Agro</p>
        </div>
        <div class='footer'>
            <p>© 2024 NexaSoft Agro. Todos los derechos reservados.</p>
            <p>Este es un mensaje automático, por favor no responder este correo.</p>
        </div>
    </div>
</body>
</html>";
    }

    public string GenerateImportantAnnouncement(string title, string content, string actionUrl = "")
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #ff6b6b 0%, #ee5a52 100%); color: white; padding: 30px; text-align: center; }}
        .urgent {{ background: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0; }}
        .content {{ background: #ffffff; padding: 30px; border-radius: 0 0 5px 5px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .button {{ background: #dc3545; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🚨 Comunicado Importante</h1>
        </div>
        <div class='content'>
            <div class='urgent'>
                <h3>URGENTE: {title}</h3>
            </div>
            <p>{content}</p>
            {(string.IsNullOrEmpty(actionUrl) ? "" : $@"
            <p style='text-align: center; margin: 30px 0;'>
                <a href='{actionUrl}' class='button'>Tomar Acción</a>
            </p>")}
            <p><strong>Este mensaje requiere su atención inmediata.</strong></p>
        </div>
    </div>
</body>
</html>";
    }

    public string GenerateReportNotification(string reportName, string downloadUrl)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%); color: white; padding: 30px; text-align: center; }}
        .content {{ background: #ffffff; padding: 30px; border-radius: 0 0 5px 5px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
        .button {{ background: #4facfe; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block; }}
        .info-box {{ background: #e8f4fd; border-left: 4px solid #4facfe; padding: 15px; margin: 20px 0; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>📊 Reporte Generado</h1>
        </div>
        <div class='content'>
            <h2>Estimado usuario,</h2>
            
            <div class='info-box'>
                <h3>Nuevo reporte disponible: {reportName}</h3>
                <p>El sistema ha generado exitosamente el reporte solicitado.</p>
            </div>

            <p>Puede descargar el documento desde el siguiente enlace:</p>
            
            <p style='text-align: center; margin: 30px 0;'>
                <a href='{downloadUrl}' class='button'>📥 Descargar Reporte</a>
            </p>

            <p><strong>Detalles del reporte:</strong></p>
            <ul>
                <li>📅 Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}</li>
                <li>📋 Tipo: Reporte ejecutivo</li>
                <li>🔐 Disponible por: 7 días</li>
            </ul>

            <p>Si tiene problemas con el enlace, copie y pegue esta URL en su navegador:<br>
            <code>{downloadUrl}</code></p>

            <p><strong>Atentamente,</strong><br>Sistema de Reportes NexaSoft Agro</p>
        </div>
        <div class='footer'>
            <p>© 2024 NexaSoft Agro. Este es un mensaje automático.</p>
        </div>
    </div>
</body>
</html>";
    }

    public string GenerateMeetingInvitation(string meetingTitle, DateTime date, string location)
    {
        var formattedDate = date.ToString("dddd, dd 'de' MMMM 'de' yyyy");
        var formattedTime = date.ToString("hh:mm tt");

        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #5ee7df 0%, #b490ca 100%); color: white; padding: 30px; text-align: center; }}
        .content {{ background: #ffffff; padding: 30px; border-radius: 0 0 5px 5px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
        .button {{ background: #5ee7df; color: #333; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block; font-weight: bold; }}
        .calendar-event {{ background: #f8f9fa; border: 2px dashed #dee2e6; padding: 20px; margin: 20px 0; text-align: center; }}
        .event-detail {{ margin: 10px 0; padding: 10px; background: #e9ecef; border-radius: 5px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>📅 Invitación a Reunión</h1>
        </div>
        <div class='content'>
            <h2>Estimado colaborador,</h2>
            <p>Se le invita cordialmente a participar de la siguiente reunión:</p>

            <div class='calendar-event'>
                <h3>{meetingTitle}</h3>
                <div class='event-detail'>
                    <strong>📅 Fecha:</strong> {formattedDate}
                </div>
                <div class='event-detail'>
                    <strong>⏰ Hora:</strong> {formattedTime}
                </div>
                <div class='event-detail'>
                    <strong>📍 Lugar:</strong> {location}
                </div>
            </div>

            <p>Por favor confirme su asistencia y agregue este evento a su calendario.</p>

            <p style='text-align: center; margin: 30px 0;'>
                <a href='#' class='button'>✅ Confirmar Asistencia</a>
            </p>

            <p><strong>Agenda:</strong></p>
            <ul>
                <li>Revisión de puntos principales</li>
                <li>Discusión de temas estratégicos</li>
                <li>Acuerdos y próximos pasos</li>
            </ul>

            <p><strong>Recomendaciones:</strong></p>
            <ul>
                <li>Llegar 5 minutos antes</li>
                <li>Revisar documentación previa</li>
                <li>Preparar puntos a discutir</li>
            </ul>

            <p><strong>Atentamente,</strong><br>Comité Organizador</p>
        </div>
        <div class='footer'>
            <p>© 2024 NexaSoft Agro. Invitación generada automáticamente.</p>
        </div>
    </div>
</body>
</html>";
    }

    public string GeneratePasswordResetEmail(string name, string resetUrl)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #ff758c 0%, #ff7eb3 100%); color: white; padding: 30px; text-align: center; }}
        .content {{ background: #ffffff; padding: 30px; border-radius: 0 0 5px 5px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
        .button {{ background: #ff758c; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block; }}
        .warning {{ background: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🔒 Restablecer Contraseña</h1>
        </div>
        <div class='content'>
            <h2>Hola {name},</h2>
            <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta.</p>

            <div class='warning'>
                <p><strong>⚠️ Importante:</strong> Este enlace expirará en 1 hora por seguridad.</p>
            </div>

            <p style='text-align: center; margin: 30px 0;'>
                <a href='{resetUrl}' class='button'>🔄 Restablecer Contraseña</a>
            </p>

            <p>Si no solicitaste este cambio, por favor ignora este mensaje y verifica la seguridad de tu cuenta.</p>

            <p><strong>Enlace alternativo:</strong><br>
            <code>{resetUrl}</code></p>

            <p><strong>Atentamente,</strong><br>Equipo de Seguridad NexaSoft Agro</p>
        </div>
        <div class='footer'>
            <p>© 2024 NexaSoft Agro. Mensaje de seguridad automático.</p>
        </div>
    </div>
</body>
</html>";
    }

    public string GenerateProjectUpdate(string projectName, string updateMessage, string projectUrl)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #a8edea 0%, #fed6e3 100%); color: #333; padding: 30px; text-align: center; }}
        .content {{ background: #ffffff; padding: 30px; border-radius: 0 0 5px 5px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
        .button {{ background: #a8edea; color: #333; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block; font-weight: bold; }}
        .update-box {{ background: #f8f9fa; border-left: 4px solid #a8edea; padding: 20px; margin: 20px 0; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🚀 Actualización de Proyecto</h1>
        </div>
        <div class='content'>
            <h2>Estimado equipo,</h2>
            <p>Tenemos una actualización importante para el proyecto:</p>

            <div class='update-box'>
                <h3>📋 Proyecto: {projectName}</h3>
                <p>{updateMessage}</p>
            </div>

            <p>Puede acceder al proyecto para ver los detalles completos:</p>

            <p style='text-align: center; margin: 30px 0;'>
                <a href='{projectUrl}' class='button'>👀 Ver Proyecto</a>
            </p>

            <p><strong>Próximos pasos:</strong></p>
            <ul>
                <li>Revisar los cambios realizados</li>
                <li>Proporcionar feedback si es necesario</li>
                <li>Actualizar tareas asignadas</li>
            </ul>

            <p><strong>Atentamente,</strong><br>Equipo de Gestión de Proyectos</p>
        </div>
        <div class='footer'>
            <p>© 2024 NexaSoft Agro. Actualización automática del sistema.</p>
        </div>
    </div>
</body>
</html>";
    }

    public string GenerateCustomTemplate(TemplateData templateData)
    {
        var detailsHtml = new StringBuilder();

        // Generar tabla de detalles si hay datos
        if (templateData.Details.Any())
        {
            detailsHtml.AppendLine(@"<table border='0' cellpadding='5' cellspacing='0' style='border-collapse: collapse; width: 100%; margin: 15px 0;'>");

            for (int i = 0; i < templateData.Details.Count; i++)
            {
                var bgColor = i % 2 == 0 ? "#f8f9fa" : "#ffffff";
                detailsHtml.AppendLine($@"
            <tr style='background-color: {bgColor};'>
                <td style='border: 1px solid #dee2e6; padding: 8px; width: 30%;'><strong>{templateData.Details[i].Key}:</strong></td>
                <td style='border: 1px solid #dee2e6; padding: 8px;'>{templateData.Details[i].Value}</td>
            </tr>");
            }

            detailsHtml.AppendLine("</table>");
        }

        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, {(templateData.IsUrgent ? "#ff6b6b 0%, #ee5a52 100%" : "#667eea 0%, #764ba2 100%")}); 
                  color: white; padding: 30px; text-align: center; }}
        .content {{ background: #ffffff; padding: 30px; border-radius: 0 0 5px 5px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
        .button {{ background: {(templateData.IsUrgent ? "#dc3545" : "#667eea")}; 
                  color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block; }}
        .urgent {{ background: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>{(templateData.IsUrgent ? "🚨 " : "")}{templateData.Title}</h1>
        </div>
        <div class='content'>
            <h2>{templateData.Greeting}</h2>
            
            {(templateData.IsUrgent ? @"<div class='urgent'><h3>⚠️ ATENCIÓN REQUERIDA</h3></div>" : "")}
            
            <p>{templateData.MainContent}</p>
            
            {detailsHtml}
            
            {(string.IsNullOrEmpty(templateData.ActionUrl) ? "" : $@"
            <p style='text-align: center; margin: 30px 0;'>
                <a href='{templateData.ActionUrl}' class='button'>{templateData.ActionText}</a>
            </p>")}
            
            <p><strong>Atentamente,</strong><br>Equipo Consulting</p>
        </div>
        <div class='footer'>
            <p>© 2025 NexaSoft Agro. {templateData.Footer}</p>
        </div>
    </div>
</body>
</html>";
    }

    
}
