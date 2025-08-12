using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Queries.GetEmpresas;

public sealed record GetEmpresasQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<EmpresaResponse>>;
