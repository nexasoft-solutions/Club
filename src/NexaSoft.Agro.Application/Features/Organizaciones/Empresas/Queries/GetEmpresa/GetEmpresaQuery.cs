using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Queries.GetEmpresa;

public sealed record GetEmpresaQuery(
    Guid Id
) : IQuery<EmpresaResponse>;
