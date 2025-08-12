using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Queries.GetColaboradores;

public sealed record GetColaboradoresQuery(
    BaseSpecParams<int> SpecParams
) : IQuery<Pagination<ColaboradorResponse>>;
