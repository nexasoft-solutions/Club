using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using NexaSoft.Agro.Application.Abstractions.Email;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Infrastructure.ConfigSettings;

namespace NexaSoft.Agro.Infrastructure.Abstractions.Email;

public class EmailService : IEmailService
{
    private readonly HttpClient _httpClient;
    private readonly BrevoOptions _brevoOptions;

    public EmailService(HttpClient httpClient, IOptions<BrevoOptions> brevoOptions)
    {
        _httpClient = httpClient;
        _brevoOptions = brevoOptions.Value;
        _brevoOptions.ApiKey = _brevoOptions.ApiKey.Trim();
    }

    public async Task SendAsync(string to, string subject, string htmlContent)
    {
        var payload = new
        {
            sender = new { name = _brevoOptions.FromName, email = _brevoOptions.FromEmail },
            to = new[] { new { email = to } },
            subject = subject,
            htmlContent = htmlContent
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // ✅ CREA UNA NUEVA INSTANCIA DE HTTPCLIENT PARA EVITAR PROBLEMAS
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://api.brevo.com/v3/");

        // ✅ FORMA ALTERNATIVA DE AUTENTICACIÓN
        httpClient.DefaultRequestHeaders.Add("api-key", _brevoOptions.ApiKey);
        httpClient.DefaultRequestHeaders.Add("accept", "application/json");

        // ✅ LOG DE LOS HEADERS
        /*foreach (var header in httpClient.DefaultRequestHeaders)
        {
            Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }*/

        try
        {
            var response = await httpClient.PostAsync("smtp/email", content);
            var responseBody = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"❌ Error al enviar correo: {response.StatusCode} - {responseBody}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw;
        }
    }

    public async Task SendAsync(EmailMessage emailMessage)
    {
        // ✅ USA CreateBrevoPayload PARA MANTENER LA LÓGICA CENTRALIZADA
        var payload = CreateBrevoPayload(emailMessage);
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

       
        // ✅ USA EXACTAMENTE EL MISMO ENFOQUE QUE EL PRIMER MÉTODO
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://api.brevo.com/v3/");
        httpClient.DefaultRequestHeaders.Add("api-key", _brevoOptions.ApiKey);
        httpClient.DefaultRequestHeaders.Add("accept", "application/json");

        // ✅ LOG DE LOS HEADERS
        /*Console.WriteLine("Headers siendo enviados:");
        foreach (var header in httpClient.DefaultRequestHeaders)
        {
            Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }*/

        try
        {
            var response = await httpClient.PostAsync("smtp/email", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Response Status: {(int)response.StatusCode} {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"❌ Error Brevo: {responseBody}");
                throw new InvalidOperationException($"Error al enviar correo: {response.StatusCode} - {responseBody}");
            }

            Console.WriteLine("✅ Correo enviado exitosamente");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Exception enviando correo: {ex.Message}");
            throw;
        }
    }

    private object CreateBrevoPayload(EmailMessage emailMessage)
    {
        return new
        {
            sender = new
            {
                name = _brevoOptions.FromName,
                email = _brevoOptions.FromEmail
            },
            to = new[]
            {
                new
                {
                    email = emailMessage.To,
                    name = emailMessage.ToName ?? emailMessage.To
                }
            },
            cc = emailMessage.CC?.Select(email => new { email }).ToArray(),
            bcc = emailMessage.BCC?.Select(email => new { email }).ToArray(),
            subject = emailMessage.Subject,
            htmlContent = emailMessage.HtmlContent,
            textContent = emailMessage.TextContent,
            attachment = emailMessage.Attachments?.Select(att => new
            {
                name = att.Name,
                content = Convert.ToBase64String(att.Content)
            }).ToArray(),
            headers = new
            {
                X_Priority = emailMessage.IsImportant ? "1" : "3",
                Importance = emailMessage.IsImportant ? "high" : "normal"
            }
        };
    }
}
