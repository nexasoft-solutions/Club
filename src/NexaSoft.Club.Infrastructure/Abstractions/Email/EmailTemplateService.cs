using System.Text;
using NexaSoft.Club.Application.Abstractions.Email;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Infrastructure.Abstractions.Email;

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


    public string GenerateReceiptTemplate(ReceiptTemplateData data)
    {
        var itemsHtml = new StringBuilder();

        if (data.Items.Any())
        {
            itemsHtml.AppendLine(@"<table border='0' cellpadding='8' cellspacing='0' style='border-collapse: collapse; width: 100%; margin: 15px 0; background: white;'>");
            itemsHtml.AppendLine(@"<tr style='background: #2c3e50; color: white;'>");
            itemsHtml.AppendLine(@"<th style='padding: 10px; text-align: left;'>Descripción</th>");
            itemsHtml.AppendLine(@"<th style='padding: 10px; text-align: right; width: 120px;'>Monto</th>");
            itemsHtml.AppendLine(@"</tr>");

            foreach (var item in data.Items)
            {
                itemsHtml.AppendLine($@"<tr style='border-bottom: 1px solid #eee;'>");
                itemsHtml.AppendLine($@"<td style='padding: 10px;'>{item.Description}</td>");
                itemsHtml.AppendLine($@"<td style='padding: 10px; text-align: right;'>S/ {item.Amount:N2}</td>");
                itemsHtml.AppendLine(@"</tr>");
            }

            itemsHtml.AppendLine(@"</table>");
        }

        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: 'Segoe UI', Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0; }}
        .container {{ max-width: 600px; margin: 0 auto; background: #ffffff; }}
        .header {{ background: #2c3e50; color: white; padding: 30px; text-align: center; }}
        .content {{ padding: 30px; background: #f8f9fa; }}
        .footer {{ padding: 20px; text-align: center; font-size: 12px; color: #666; background: #2c3e50; color: white; }}
        .receipt-card {{ background: white; padding: 25px; margin: 20px 0; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .total-section {{ background: #e8f5e8; padding: 20px; border-radius: 5px; margin-top: 20px; text-align: right; }}
        .info-row {{ display: flex; justify-content: space-between; margin: 8px 0; }}
        .info-label {{ font-weight: bold; color: #555; }}
        .total-amount {{ font-size: 20px; font-weight: bold; color: #2c3e50; margin-bottom: 10px; }}
        .amount-in-words {{ font-style: italic; color: #555; margin-top: 10px; text-align: center; border-top: 1px dashed #ccc; padding-top: 10px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>{data.OrganizationName}</h1>
            <p>{data.DocumentType}</p>
        </div>
        
        <div class='content'>
            <p>Estimado(a) <strong>{data.CustomerName}</strong>,</p>
            
            <p>{data.Message}</p>
            
            <div class='receipt-card'>
                <div class='info-row'>
                    <span class='info-label'>Número de Comprobante:</span>
                    <span>{data.DocumentNumber}</span>
                </div>
                <div class='info-row'>
                    <span class='info-label'>Fecha de Emisión:</span>
                    <span>{data.IssueDate:dd/MM/yyyy}</span>
                </div>
                <div class='info-row'>
                    <span class='info-label'>Hora:</span>
                    <span>{data.IssueDate:HH:mm}</span>
                </div>
                {(string.IsNullOrEmpty(data.Reference) ? "" : $@"
                <div class='info-row'>
                    <span class='info-label'>Referencia:</span>
                    <span>{data.Reference}</span>
                </div>")}

                <div style='margin: 20px 0;'>
                    <h3 style='color: #2c3e50; border-bottom: 2px solid #2c3e50; padding-bottom: 8px;'>Detalles</h3>
                    {itemsHtml}
                </div>

                <div class='total-section'>
                    <div class='total-amount'>
                        TOTAL: S/ {data.TotalAmount:N2}
                    </div>
                    {(string.IsNullOrEmpty(data.AmountInWords) ? "" : $@"
                    <div class='amount-in-words'>
                        Son: {data.AmountInWords}
                    </div>")}
                </div>
            </div>
            
            <p>{data.AdditionalNotes}</p>
        </div>
        
        <div class='footer'>
            <p><strong>{data.OrganizationName}</strong><br>
            {data.OrganizationAddress}<br>
            {data.OrganizationContact}</p>
            <p>Este es un mensaje automático, por favor no responda a este correo.</p>
        </div>
    </div>
</body>
</html>";
    }

    public string GenerateNotificationTemplate(NotificationTemplateData data)
    {
        var detailsHtml = new StringBuilder();

        if (data.Details.Any())
        {
            detailsHtml.AppendLine(@"<div style='background: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0;'>");
            foreach (var detail in data.Details)
            {
                detailsHtml.AppendLine($@"<div style='margin: 5px 0;'><strong>{detail.Key}:</strong> {detail.Value}</div>");
            }
            detailsHtml.AppendLine(@"</div>");
        }

        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: 'Segoe UI', Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; background: #ffffff; }}
        .header {{ background: {(data.IsUrgent ? "linear-gradient(135deg, #ff6b6b 0%, #ee5a52 100%)" : "linear-gradient(135deg, #667eea 0%, #764ba2 100%)")}; 
                  color: white; padding: 30px; text-align: center; }}
        .content {{ padding: 30px; background: #f8f9fa; }}
        .footer {{ padding: 20px; text-align: center; font-size: 12px; color: #666; }}
        .button {{ background: {(data.IsUrgent ? "#dc3545" : "#667eea")}; 
                  color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block; }}
        .urgent {{ background: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>{(data.IsUrgent ? "🚨 " : "")}{data.Title}</h1>
        </div>
        <div class='content'>
            <h2>{data.Greeting}</h2>
            
            {(data.IsUrgent ? @"<div class='urgent'><h3>⚠️ ATENCIÓN REQUERIDA</h3></div>" : "")}
            
            <p>{data.Message}</p>
            
            {detailsHtml}
            
            {(string.IsNullOrEmpty(data.ActionUrl) ? "" : $@"
            <p style='text-align: center; margin: 30px 0;'>
                <a href='{data.ActionUrl}' class='button'>{data.ActionText ?? "Tomar Acción"}</a>
            </p>")}
            
            <p><strong>Atentamente,</strong><br>{data.OrganizationName}</p>
        </div>
        <div class='footer'>
            <p>© {DateTime.Now.Year} {data.OrganizationName}. {data.FooterNotes}</p>
        </div>
    </div>
</body>
</html>";
    }

}
