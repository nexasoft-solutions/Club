using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Email;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Templates;

namespace NexaSoft.Club.Application.Features.Members.Commands.InitMemberRegistration;

public class InitMemberRegistrationCommandHandler(
    IGenericRepository<Member> _memberRepository,
    IGenericRepository<MemberPin> _pinRepository,
    IEmailService _emailService,
    IEmailTemplateService _templateService,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<InitMemberRegistrationCommandHandler> _logger
) : ICommandHandler<InitMemberRegistrationCommand, MemberRegistrationResponse>
{
    public async Task<Result<MemberRegistrationResponse>> Handle(InitMemberRegistrationCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando registro para DNI: {Dni}", command.Dni);

        try
        {
            // 1. Buscar member por DNI y fecha de nacimiento
            var member = await _memberRepository.GetEntityWithSpec(
                new MemberByDniAndBirthDateSpec(command.Dni, command.BirthDate),
                cancellationToken);

            if (member == null)
                return Result.Failure<MemberRegistrationResponse>(MemberErrores.NoEncontrado);

            if (member.Status != "Active")
                return Result.Failure<MemberRegistrationResponse>(MemberErrores.NotActive);

            // 2. Generar PIN aleatorio
            var pin = GenerateRandomPin();
            var expiresAt = _dateTimeProvider.CurrentTime.AddMinutes(10);

            // 3. Guardar PIN (invalidar anteriores)
            await InvalidatePreviousPins(member.Id, cancellationToken);

            var memberPin = MemberPin.Create(
                member.Id,
                pin,
                expiresAt,
                command.DeviceId,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                command.CreatedBy
            );

            await _pinRepository.AddAsync(memberPin, cancellationToken);

            // 4. Enviar PIN por email/SMS
            /*await _notificationService.SendPinAsync(
                member.Email,
                member.Phone,
                pin,
                expiresAt,
                cancellationToken);*/


            var emailMessage = new EmailMessage
            {
                To = member.Email!,
                ToName = member.FirstName + " " + member.LastName,
                Subject = $"📋 PIN de validación",
                HtmlContent = _templateService.GenerateCustomTemplate(
                new TemplateData
                {
                    Title = "PIN de validación para registro en el App Club Social",
                    Greeting = $"Estimado <strong>{member.FirstName} {member.LastName}</strong>,",
                    MainContent = "Se ha generado el siguiente PIN para activar su acceso a la App del Club Social. Por favor, introdúzcalo en la aplicación para completar su registro.",
                    IsUrgent = true,
                    Footer = "Si usted no solicitó este código, por favor ignore este mensaje o comuníquese con soporte."
                }.ForActivacionPin(
                    member.FirstName!,
                    member.LastName!,
                    member.Email!,
                    pin,
                    expiresAt
                )
             ),
                //reportUrl
                IsImportant = true,
                //CC = ccList//new List<string> { "aldoroblesarana@gmail.com" }
            };

            await _emailService.SendAsync(emailMessage);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("PIN enviado para member {MemberId}", member.Id);

            return Result.Success(new MemberRegistrationResponse(
                Success: true,
                Message: "PIN enviado exitosamente",
                MemberName: $"{member.FirstName} {member.LastName}",
                Email: member.Email, // Para mostrar máscara de email
                PinExpirationMinutes: 10
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error iniciando registro para DNI: {Dni}", command.Dni);
            return Result.Failure<MemberRegistrationResponse>(MemberErrores.ErrorSave);
        }
    }

    private string GenerateRandomPin()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString(); // 6 dígitos
    }

    private async Task InvalidatePreviousPins(long memberId, CancellationToken cancellationToken)
    {
        var existingPins = await _pinRepository.ListAsync(
            new ActivePinsByMemberSpec(memberId),
            cancellationToken);

        foreach (var pin in existingPins)
        {
            pin.MarkAsUsed();
        }

        if (existingPins.Any())
            await _pinRepository.UpdateRangeAsync(existingPins, cancellationToken);
    }

}

