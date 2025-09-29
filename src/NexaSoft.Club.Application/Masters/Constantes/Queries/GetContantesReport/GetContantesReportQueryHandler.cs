using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Masters.Constantes.Queries.GetConstesMultiple;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Constantes;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.Constantes.Queries.GetContantesReport;

public class GetContantesReportQueryHandler(
    IGenericRepository<Constante> _repository,
    IConstantsPdfReportGenerator _constantsPdfGenerator
) : IQueryHandler<GetContantesReportQuery, byte[]>
{
    public async Task<Result<byte[]>> Handle(GetContantesReportQuery query, CancellationToken cancellationToken)
    {
        var spec = new ConstanteMultipleSpecification(query.TiposConstante);
        var list = await _repository.ListAsync(spec, cancellationToken);

        var constantes = list
            .GroupBy(c => c.TipoConstante)
            .Select(g => new ConstantesAgrupadasResponse(
                g.Key!,
                g.Select(c => new ConstantesClaveValorResponse(
                    c.Clave,
                    c.Valor!
                )).ToList()
            ))
            .ToList();

        if (!constantes.Any())
            return Result.Failure<byte[]>(ConstanteErrores.NoHayConincidencias);

        var pdfBytes = _constantsPdfGenerator.GenerateConstantsReport(
            constantes,
            title: "Catálogo de Constantes - NexaSoft Agro",
            subtitle: $"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}"
        );

        return Result.Success(pdfBytes);
    }
}
