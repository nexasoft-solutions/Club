using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Masters.Consultoras;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Queries.GetConsultoras;

public sealed record GetConsultorasQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<ConsultoraResponse>>;
