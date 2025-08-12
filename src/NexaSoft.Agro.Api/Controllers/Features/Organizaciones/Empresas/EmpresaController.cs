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
             request.OrganizacionId
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
             request.OrganizacionId
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
   public async Task<IActionResult> DeleteEmpresa(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteEmpresaCommand(
             id
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


   [HttpGet("{id:Guid}")]
   public async Task<IActionResult> GetEmpresa(
       Guid id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEmpresaQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
