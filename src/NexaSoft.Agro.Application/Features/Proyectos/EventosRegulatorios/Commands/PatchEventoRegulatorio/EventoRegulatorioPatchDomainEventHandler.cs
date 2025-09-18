using MediatR;
using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Email;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios.Events;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Templates;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.PatchEventoRegulatorio;

public class EventoRegulatorioPatchDomainEventHandler(
    IEventoRegulatorioRepository _repository,
    IGenericRepository<Responsable> _responsableRepository,
    IEmailService _emailService,
    IEmailTemplateService _templateService,
    ILogger<EventoRegulatorioCreadoDomainEvent> _logger
) : INotificationHandler<EventoRegulatorioPatchDomainEvent>

{
    public async Task Handle(EventoRegulatorioPatchDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de envio de correo del Evento con ID {EventoRegulatorioId}", notification.Id);
        var specParams = new BaseSpecParams { Id = notification.Id };
        var spec = new EventoRegulatorioSpecification(specParams);

        var (pagination, _) = await _repository.GetEventosRegulatoriosAsync(spec, cancellationToken);

        var entity = pagination.Data.FirstOrDefault();

        if (entity is not null)
        {
            var responsableIds = entity.Responsables?
                .Select(r => r.ResponsableId)
                .ToList();

            var SpecParams = new BaseSpecParams<long>
            {
                Ids = responsableIds ?? new List<long>()
            };
            specParams.NoPaging = true;

            var responsableSpec = new ResponsableSpecification(SpecParams);
            var responsables = await _responsableRepository
                .ListAsync<ResponsableResponse>(responsableSpec, cancellationToken);


            var ccList = new List<string>();
            if (responsables != null)
            {
                foreach (var responsable in responsables)
                {
                    if (!string.IsNullOrWhiteSpace(responsable.CorreoResponsable))
                    {
                        ccList.Add(responsable.CorreoResponsable);
                    }
                }
            }

            var emailMessage = new EmailMessage
            {
                To = entity.CorreoResponsable!,
                ToName = entity.Responsable!,
                Subject = $"📋 Evento Regulatorio Actualizado {entity.TipoEvento} - Atención Requerida",
                HtmlContent = _templateService.GenerateCustomTemplate(
                    new TemplateData
                    {
                        Title = $"Evento Regulatorio {entity.TipoEvento} - Atención Inmediata",
                        Greeting = $"Estimado <strong>{entity.Responsable}</strong>,",
                        MainContent = $@"Se ha actualizado el evento regulatorio a <strong>{entity.EstadoEvento}</strong> que requiere su revisión inmediata. 
                                        Por favor verifique los detalles y tome las acciones correspondientes.",
                        IsUrgent = true,
                        ActionText = "📋 Ver Detalles Completos",
                        //ActionUrl = $"https://app.nexasoft.com/eventos/{entity.Id}",
                        Footer = "Requiere confirmación de recepción"
                    }.ForEventoRegulatorio(entity)
                ),
                //reportUrl
                IsImportant = true,
                CC = ccList//new List<string> { "aldoroblesarana@gmail.com" }
            };

            await _emailService.SendAsync(emailMessage);
            _logger.LogInformation("Envio de notificacón con ID {EventoRegulatorioId}, sobre {TipoEventoId}, enviado satisfactoriamente a {ResponsableId}", entity.Id, entity.TipoEventoId, entity.ResponsableId);
        }
    }
}
