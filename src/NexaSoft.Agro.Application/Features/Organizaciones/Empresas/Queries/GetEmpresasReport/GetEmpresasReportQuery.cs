using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Queries.GetEmpresasReport;

public sealed record GetEmpresasReportQuery(BaseSpecParams SpecParams): IQuery<byte[]>;

