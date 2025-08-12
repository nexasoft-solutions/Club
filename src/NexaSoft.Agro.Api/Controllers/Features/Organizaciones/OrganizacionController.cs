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
             request.SectorId
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
             request.SectorId
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteOrganizacion(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteOrganizacionCommand(
             id
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


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetOrganizacion(
        Guid id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetOrganizacionQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
