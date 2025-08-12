using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Consultoras;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Queries.GetConsultora;

public sealed record GetConsultoraQuery(
    Guid Id
) : IQuery<ConsultoraResponse>;
