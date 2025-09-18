using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.Cumplimientos.Request;
using NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Commands.CreateCumplimiento;
using NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Commands.UpdateCumplimiento;
using NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Commands.DeleteCumplimiento;
using NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Queries.GetCumplimiento;
using NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Queries.GetCumplimientos;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.Cumplimientos.Requests;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Cumplimientos;

[Route("api/[controller]")]
[ApiController]
public class CumplimientoController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateCumplimiento(CreateCumplimientoRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCumplimientoCommand(
             request.FechaCumplimiento,
             request.RegistradoaTiempo,
             request.Observaciones,
             request.EventoRegulatorioId,
             request.UsuarioCreacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCumplimiento(UpdateCumplimientoRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCumplimientoCommand(
           request.Id,
             request.FechaCumplimiento,
             request.RegistradoaTiempo,
             request.Observaciones,
             request.EventoRegulatorioId,
             request.UsuarioModificacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCumplimiento(DeleteCumplimientoRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteCumplimientoCommand(
             request.Id,
             request.UsuarioEliminacion
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetCumplimientos(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetCumplimientosQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetCumplimiento(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetCumplimientoQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
