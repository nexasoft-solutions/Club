using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Empresas.Request;
using NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.CreateEmpresa;
using NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.UpdateEmpresa;
using NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.DeleteEmpresa;
using NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Queries.GetEmpresa;
using NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Queries.GetEmpresas;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Queries.GetEmpresasReport;
using NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Empresas.Requests;

namespace NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Empresas;

[Route("api/[controller]")]
[ApiController]
public class EmpresaController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateEmpresa(CreateEmpresaRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateEmpresaCommand(
             request.RazonSocial,
             request.RucEmpresa,
             request.ContactoEmpresa,
             request.TelefonoContactoEmpresa,
             request.DepartamentoEmpresaId,
             request.ProvinciaEmpresaId,
             request.DistritoEmpresaId,
             request.Direccion,
             request.LatitudEmpresa,
             request.LongitudEmpresa,
             request.OrganizacionId,
             request.UsuarioCreacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmpresa(UpdateEmpresaRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateEmpresaCommand(
           request.Id,
             request.RazonSocial,
             request.RucEmpresa,
             request.ContactoEmpresa,
             request.TelefonoContactoEmpresa,
             request.DepartamentoEmpresaId,
             request.ProvinciaEmpresaId,
             request.DistritoEmpresaId,
             request.Direccion,
             request.LatitudEmpresa,
             request.LongitudEmpresa,
             request.OrganizacionId,
             request.UsuarioModificacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmpresa(DeleteEmpresaRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteEmpresaCommand(
             request.Id,
             request.UsuarioEliminacion!
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetEmpresas(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEmpresasQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetEmpresa(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetEmpresaQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("reporte")]
    public async Task<IActionResult> GetEmpresasReport(
        [FromQuery] BaseSpecParams specParams,
        CancellationToken cancellationToken
    )
    {
        var query = new GetEmpresasReportQuery(specParams);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return result.ToActionResult(this);

        // Retornar el PDF como archivo descargable
        return File(
            fileContents: result.Value,
            contentType: "application/pdf",
            fileDownloadName: "Reporte_Empresas.pdf"
        );
    }

}
