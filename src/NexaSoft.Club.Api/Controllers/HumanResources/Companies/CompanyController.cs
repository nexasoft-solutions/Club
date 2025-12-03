using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.Companies.Request;
using NexaSoft.Club.Application.HumanResources.Companies.Commands.CreateCompany;
using NexaSoft.Club.Application.HumanResources.Companies.Commands.UpdateCompany;
using NexaSoft.Club.Application.HumanResources.Companies.Commands.DeleteCompany;
using NexaSoft.Club.Application.HumanResources.Companies.Queries.GetCompany;
using NexaSoft.Club.Application.HumanResources.Companies.Queries.GetCompanies;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.Companies;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CompanyController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("Company.CreateCompany")]
    public async Task<IActionResult> CreateCompany(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCompanyCommand(
             request.Ruc,
             request.BusinessName,
             request.TradeName,
             request.Address,
             request.Phone,
             request.Website,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("Company.UpdateCompany")]
    public async Task<IActionResult> UpdateCompany(UpdateCompanyRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCompanyCommand(
           request.Id,
             request.Ruc,
             request.BusinessName,
             request.TradeName,
             request.Address,
             request.Phone,
             request.Website,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("Company.DeleteCompany")]
    public async Task<IActionResult> DeleteCompany(DeleteCompanyRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteCompanyCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("Company.GetCompany")]
    public async Task<IActionResult> GetCompanies(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetCompaniesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("Company.GetCompany")]
    public async Task<IActionResult> GetCompany(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetCompanyQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
