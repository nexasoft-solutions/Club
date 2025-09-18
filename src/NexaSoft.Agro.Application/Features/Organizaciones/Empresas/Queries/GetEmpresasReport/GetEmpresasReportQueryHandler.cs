
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Reporting;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;
using NexaSoft.Agro.Domain.Specifications;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Queries.GetEmpresasReport;

public class GetEmpresasReportQueryHandler(
    IGenericRepository<Empresa> _repository,
    IPdfReportGenerator<EmpresaResponse> _pdfGenerator
) : IQueryHandler<GetEmpresasReportQuery, byte[]>
{
    public async Task<Result<byte[]>> Handle(GetEmpresasReportQuery query, CancellationToken cancellationToken)
    {
        var spec = new EmpresaSpecification(query.SpecParams);
        var empresas = await _repository.ListAsync<EmpresaResponse>(spec, cancellationToken);

        if (!empresas.Any())
            return Result.Failure<byte[]>(EmpresaErrores.NoHayConincidencias);

        var columnDefinitions = new List<ColumnDefinition>
        {
            new() { PropertyName = "RazonSocial", DisplayName = "Razón Social", Order = 1 },
            new() { PropertyName = "RucEmpresa", DisplayName = "RUC", Order = 2 },
            new() { PropertyName = "ContactoEmpresa", DisplayName = "Contacto", Order = 3 },
            new() { PropertyName = "TelefonoContactoEmpresa", DisplayName = "Teléfono", Order = 4 },
            new() { PropertyName = "Direccion", DisplayName = "Dirección", Order = 5 },
            new() { PropertyName = "DepartamentoEmpresa", DisplayName = "Departamento", Order = 6 },
            new() { PropertyName = "ProvinciaEmpresa", DisplayName = "Provincia", Order = 7 },
            new() { PropertyName = "DistritoEmpresa", DisplayName = "Distrito", Order = 8 },
            new() { PropertyName = "Organizacion", DisplayName = "Organización", Order = 9 },
            new() { PropertyName = "FechaCreacion", DisplayName = "Fecha Creación", Order = 10 }
        };

        var pdfBytes = _pdfGenerator.Generate(
            empresas,
            columnDefinitions,
            title: "Reporte de Empresas - NexaSoft Agro",
            subtitle: $"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}",
            logoPath: "",
            entityName: "Empresa",
            idPropertyName: "Id",
            highlightPropertyName: "RucEmpresa"
        );

        return Result.Success(pdfBytes);
    }
}
