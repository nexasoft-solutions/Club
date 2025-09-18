
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(EmailMessage emailMessage);
    Task SendAsync(string to, string subject, string htmlContent); // Para backward compatibility
}