using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Request;
using NexaSoft.Agro.Application.Features.Organizaciones.Commands.CreateOrganizacion;
using NexaSoft.Agro.Application.Features.Organizaciones.Commands.UpdateOrganizacion;
using NexaSoft.Agro.Application.Features.Organizaciones.Commands.DeleteOrganizacion;
using NexaSoft.Agro.Application.Features.Organizaciones.Queries.GetOrganizacion;
using NexaSoft.Agro.Application.Features.Organizaciones.Queries.GetOrganizaciones;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Application.Features.Organizaciones.Queries.GetOrganizacionesReport;
using NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Requests;

namespace NexaSoft.Agro.Api.Controllers.Features.Organizaciones;

[Route("api/[controller]")]
[ApiController]
public class OrganizacionController(ISender _sender) : ControllerBase
{

    /// <summary>
    /// Endpoint para crear una nueva organización
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// 
    [HttpPost]
    public async Task<IActionResult> CreateOrganizacion(CreateOrganizacionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateOrganizacionCommand(
             request.NombreOrganizacion,
             request.ContactoOrganizacion,
             request.TelefonoContacto,
             request.RucOrganizacion,
             request.Observaciones,
             request.SectorId,
             request.UsuarioCreacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrganizacion(UpdateOrganizacionRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateOrganizacionCommand(
           request.Id,
             request.NombreOrganizacion,
             request.ContactoOrganizacion,
             request.TelefonoContacto,
             request.RucOrganizacion,
             request.Observaciones,
             request.SectorId,
             request.UsuarioModificacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOrganizacion(DeleteOrganizacionRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteOrganizacionCommand(
             request.Id,
             request.UsuarioEliminacion
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrganizaciones(
       [FromQuery] BaseSpecParams<int> specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetOrganizacionesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetOrganizacion(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetOrganizacionQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }
    
    [HttpGet("reporte")]
    public async Task<IActionResult> GetOrganizacionesReport(
        [FromQuery] BaseSpecParams<int> specParams,
        CancellationToken cancellationToken
    )
    {
        var query = new GetOrganizacionesReportQuery(specParams);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return result.ToActionResult(this);

        // Retornar el PDF como archivo descargable
        return File(
            fileContents: result.Value,
            contentType: "application/pdf",
            fileDownloadName: "Reporte_Organizaciones.pdf"
        );
    }

}
