using NexaSoft.Agro.Application.Abstractions.Email;

namespace NexaSoft.Agro.Infrastructure.Abstractions.Email;

public class EmailService : IEmailService
{
    public Task SendAsync(string correo, string subject, string body)
    {
        return Task.CompletedTask;
    }
}
