using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Masters.Consultoras.Colaboradores.Request;
using NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Commands.CreateColaborador;
using NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Commands.UpdateColaborador;
using NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Commands.DeleteColaborador;
using NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Queries.GetColaborador;
using NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Queries.GetColaboradores;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;

namespace NexaSoft.Agro.Api.Controllers.Masters.Consultoras.Colaboradores;

[Route("api/[controller]")]
[ApiController]
public class ColaboradorController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateColaborador(CreateColaboradorRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateColaboradorCommand(
             request.NombresColaborador,
             request.ApellidosColaborador,
             request.TipoDocumentoId,
             request.NumeroDocumentoIdentidad,
             request.FechaNacimiento,
             request.GeneroColaboradorId,
             request.EstadoCivilColaboradorId,
             request.Direccion,
             request.CorreoElectronico,
             request.TelefonoMovil,
             request.CargoId,
             request.DepartamentoId,
             request.FechaIngreso,
             request.Salario,
             request.FechaCese,
             request.Comentarios,
             request.ConsultoraId
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateColaborador(UpdateColaboradorRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateColaboradorCommand(
           request.Id,
             request.NombresColaborador,
             request.ApellidosColaborador,
             request.TipoDocumentoId,
             request.NumeroDocumentoIdentidad,
             request.FechaNacimiento,
             request.GeneroColaboradorId,
             request.EstadoCivilColaboradorId,
             request.Direccion,
             request.CorreoElectronico,
             request.TelefonoMovil,
             request.CargoId,
             request.DepartamentoId,
             request.FechaIngreso,
             request.Salario,
             request.FechaCese,
             request.Comentarios,
             request.ConsultoraId
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
   public async Task<IActionResult> DeleteColaborador(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteColaboradorCommand(
             id
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetColaboradores(
       [FromQuery] BaseSpecParams<int> specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetColaboradoresQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:Guid}")]
   public async Task<IActionResult> GetColaborador(
       Guid id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetColaboradorQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
