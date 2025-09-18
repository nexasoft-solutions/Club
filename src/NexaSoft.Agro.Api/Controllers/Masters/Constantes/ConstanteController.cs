using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Masters.Constantes.Request;
using NexaSoft.Agro.Application.Masters.Constantes.Commands.CreateConstante;
using NexaSoft.Agro.Application.Masters.Constantes.Commands.UpdateConstante;
using NexaSoft.Agro.Application.Masters.Constantes.Commands.DeleteConstante;
using NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstante;
using NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstantes;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstantesTipo;
using NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstesMultiple;
using NexaSoft.Agro.Api.Attributes;
using NexaSoft.Agro.Application.Masters.Constantes.Queries.GetContantesReport;
using NexaSoft.Agro.Api.Controllers.Masters.Constantes.Requests;
using NexaSoft.Agro.Application.Masters.Constantes.Commands.CreateConstantes;
using NexaSoft.Agro.Application.Abstractions.Excel;

namespace NexaSoft.Agro.Api.Controllers.Masters.Constantes;

[Route("api/[controller]")]
[ApiController]
//[RequireRole("Administrador")]
public class ConstanteController(
    ISender _sender,
    IGenericExcelImporter<CreateConstantesCommand> _excelImporter
) : ControllerBase
{

    [HttpPost]
    [RequireRole("Administrador")]
    //[RequirePermission("Constante.CreateConstante")]
    public async Task<IActionResult> CreateConstante(CreateConstanteRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateConstanteCommand(
             request.TipoConstante,
             request.Valor,
             request.UsuarioCreacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [RequireRole("Administrador")] // O solo [RequirePermission("Constante.DeleteConstante")]
    [RequirePermission("Constante.UpdateConstante")]
    public async Task<IActionResult> UpdateConstante(UpdateConstanteRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateConstanteCommand(
             request.Id,
             request.TipoConstante,
             request.Valor,
             request.UsuarioModificacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteConstante(DeleteConstanteRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteConstanteCommand(
             request.Id,
             request.UsuarioEliminacion
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [RequireRole("Administrador")]
    [RequirePermission("Constante.GetConstantes")]
    public async Task<IActionResult> GetConstantes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetConstantesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetConstante(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetConstanteQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("tipo")]
    public async Task<IActionResult> GetConstantesTipo(
       CancellationToken cancellationToken
    )
    {
        var query = new GetConstantesTipoQuery();
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPost("multiple")]
    public async Task<IActionResult> GetConstantesMultiples(
        [FromBody] List<string> tiposConstante,
        CancellationToken cancellationToken)
    {
        var query = new GetConstnatesMultipleQuery(tiposConstante);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("reporte")]
    public async Task<IActionResult> GetOrganizacionesReport(
        [FromQuery] List<string> tiposConstantes,
        CancellationToken cancellationToken
    )
    {
        var query = new GetContantesReportQuery(tiposConstantes);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return result.ToActionResult(this);

        // Retornar el PDF como archivo descargable
        return File(
            fileContents: result.Value,
            contentType: "application/pdf",
            fileDownloadName: $"Reporte_Constantes_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
        );
    }

    [HttpPost("upload-excel")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadConstantesExcel(IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return BadRequest("El archivo Excel es requerido.");

        using var stream = file.OpenReadStream();

        var constantes = _excelImporter.ImportFromStream(stream);

        if (constantes.Count == 0)
            return BadRequest("El archivo Excel no contiene datos válidos.");

        var batchCommand = new CreateConstantesBatchCommand(constantes);

        var result = await _sender.Send(batchCommand, cancellationToken);

        return result.ToActionResult(this);
    }
}
