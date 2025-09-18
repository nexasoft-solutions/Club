using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Reporting;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Organizaciones;
using NexaSoft.Agro.Domain.Specifications;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Queries.GetOrganizacionesReport;

public class GetOrganizacionesReportQueryHandler(
    IOrganizacionRepository _organizacionRepository,
    IPdfReportGenerator<OrganizacionResponse> _pdfGenerator)
 : IQueryHandler<GetOrganizacionesReportQuery, byte[]>
{
    public async Task<Result<byte[]>> Handle(GetOrganizacionesReportQuery query, CancellationToken cancellationToken)
    {
        var spec = new OrganizacionSpecification(query.SpecParams);
        var (pagination, _) = await _organizacionRepository.GetOrganizacionesAsync(spec, cancellationToken);
        var organizaciones = pagination.Data;
        
        if (!organizaciones.Any())
            return Result.Failure<byte[]>(OrganizacionErrores.NoHayConincidencias);

        var columnDefinitions = new List<ColumnDefinition>
        {
            new() { PropertyName = "NombreOrganizacion", DisplayName = "Nombre Organización", Order = 1 },
            new() { PropertyName = "RucOrganizacion", DisplayName = "RUC", Order = 2 },
            new() { PropertyName = "ContactoOrganizacion", DisplayName = "Contacto", Order = 3 },
            new() { PropertyName = "TelefonoContacto", DisplayName = "Teléfono", Order = 4 },
            new() { PropertyName = "Sector", DisplayName = "Sector", Order = 5 },
            new() { PropertyName = "Observaciones", DisplayName = "Observaciones", Order = 6 },

        };

        var pdfBytes = _pdfGenerator.Generate(
            organizaciones,
            columnDefinitions,
            title: "Reporte de Organizaciones - NexaSoft Agro",
            subtitle: $"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}",
            logoPath: "",
            entityName: "Organizacion",
            idPropertyName: "Id",
            highlightPropertyName: "RucOrganizacion"
        );

        return Result.Success(pdfBytes);
    }
}
