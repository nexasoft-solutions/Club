namespace NexaSoft.Agro.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(string correo, string subject, string body);
}