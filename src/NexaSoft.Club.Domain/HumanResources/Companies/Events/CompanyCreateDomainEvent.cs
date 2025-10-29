using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Companies.Events;

public sealed record CompanyCreateDomainEvent
(
    long Id
): IDomainEvent;
