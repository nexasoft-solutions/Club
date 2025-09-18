using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Organizaciones.Events;

public sealed record OrganizacionUpdateDomainEvent
(
    long Id
): IDomainEvent;
