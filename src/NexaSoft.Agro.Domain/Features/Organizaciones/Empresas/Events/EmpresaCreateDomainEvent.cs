using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Organizaciones.Empresas.Events;

public sealed record EmpresaCreateDomainEvent
(
    long Id
): IDomainEvent;
