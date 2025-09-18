using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Queries.GetOrganizacionesReport;

public sealed record GetOrganizacionesReportQuery(BaseSpecParams<int> SpecParams):IQuery<byte[]>;

