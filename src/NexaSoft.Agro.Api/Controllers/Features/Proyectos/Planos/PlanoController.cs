using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.CreatePlano;
using NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.UpdatePlano;
using NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.DeletePlano;
using NexaSoft.Agro.Application.Features.Proyectos.Planos.Queries.GetPlano;
using NexaSoft.Agro.Application.Features.Proyectos.Planos.Queries.GetPlanos;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Application.Features.Proyectos.Planos.Queries.GetPlanoByArchivo;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Planos;

[Route("api/[controller]")]
[ApiController]
public class PlanoController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreatePlano(CreatePlanoCommand request, CancellationToken cancellationToken)
    {
        //var detalles = _mapper.Map<List<CreatePlanoDetalleCommand>>(request.Detalles);
        var command = new CreatePlanoCommand(
             request.EscalaId,
             request.SistemaProyeccion,
             request.NombrePlano,
             request.ArchivoId,
             request.ColaboradorId,
             request.Detalles,
             request.UsuarioCreacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePlano(UpdatePlanoCommand request, CancellationToken cancellationToken)
    {
        //var detalles = _mapper.Map<List<UpdatePlanoDetalleCommand>>(request.Detalles);
        var command = new UpdatePlanoCommand(
           request.Id,
             request.EscalaId,
             request.SistemaProyeccion,
             request.NombrePlano,
             request.CodigoPlano,
             request.ArchivoId,
             request.ColaboradorId,
             request.Detalles,
             request.UsuarioModificacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeletePlano(long id, CancellationToken cancellationToken)
    {
        var command = new DeletePlanoCommand(
             id
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPlanos(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPlanosQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetPlano(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetPlanoQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("{id:long}/archivoId")]
    public async Task<IActionResult> GetPlanoByArchivo(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPlanoByArchivoQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
