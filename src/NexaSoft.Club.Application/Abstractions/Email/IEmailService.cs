
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(EmailMessage emailMessage);
    Task SendAsync(string to, string subject, string htmlContent); // Para backward compatibility
}