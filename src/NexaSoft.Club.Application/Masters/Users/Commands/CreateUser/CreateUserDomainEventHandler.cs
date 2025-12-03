using MediatR;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Email;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Users;
using NexaSoft.Club.Domain.Masters.Users.Events;
using NexaSoft.Club.Domain.Masters.UserTypes;
using NexaSoft.Club.Domain.Shareds;

namespace NexaSoft.Club.Application.Masters.Users.Commands.CreateUser;

public class CreateUserDomainEventHandler(
    IGenericRepository<User> _repository,
    IEmailService _emailService,
    IEmailTemplateService _templateService,
    ILogger<CreateUserDomainEventHandler> _logger
) : INotificationHandler<UserCreateDomainEvent>
{
    public async Task Handle(UserCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(notification.Id, cancellationToken);
        if (user is null)
        {
            _logger.LogError("No se encontró el usuario con ID {UserId} para enviar el correo electrónico", notification.Id);
            return;
        }

        if (user.UserTypeId != (int)Enums.UserTypeEnum.Usuario)
        {
            _logger.LogWarning("El usuario con ID {UserId} es un usuario asociado a un miembro. No se enviará el correo.", notification.Id);
            return;
        }

        if (string.IsNullOrEmpty(user.Email))
        {
            _logger.LogWarning("El usuario con ID {UserId} no tiene un correo electrónico registrado. No se enviará el correo.", notification.Id);
            return;
        }

        if (string.IsNullOrEmpty(notification.TemporaryPassword))
        {
            _logger.LogWarning("No se proporcionó una clave temporal para el usuario con ID {UserId}. No se enviará el correo.", notification.Id);
            return;
        }


        // Suponiendo que el evento tiene la clave temporal y el username generado
        var tempPassword = notification.TemporaryPassword;
        var userName = user.UserName;

        var htmlContent = _templateService.GenerateNotificationTemplate(new NotificationTemplateData
        {
            Title = "Bienvenido a NexaSoft Club - Usuario Administrativo",
            Greeting = $"Hola <strong>{user.FullName}</strong>,",
            Message = "Tu usuario administrativo ha sido creado exitosamente. Utiliza las credenciales temporales para ingresar al sistema y cambia tu clave en el primer acceso.",
            Details = new List<KeyValuePair<string, string>>
            {
                new("Usuario", userName!),
                new("Clave temporal", tempPassword!)
            },
            ActionText = "Acceder al sistema",
            ActionUrl = "https://app.nexasoft.com/login",
            OrganizationName = "NexaSoft Club",
            FooterNotes = "Por seguridad, cambia tu clave al ingresar.",
            IsUrgent = false
        });

        var emailMessage = new EmailMessage
        {
            To = user.Email!,
            ToName = user.FullName!,
            Subject = "Bienvenido a NexaSoft Club - Usuario Administrativo",
            HtmlContent = htmlContent,
            IsImportant = true
        };

        await _emailService.SendAsync(emailMessage);
        _logger.LogInformation("Correo electrónico de usuario administrativo enviado a {Email}", user.Email);
    }
}
