using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.CompanyBankAccounts.Request;
using NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Commands.CreateCompanyBankAccount;
using NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Commands.UpdateCompanyBankAccount;
using NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Commands.DeleteCompanyBankAccount;
using NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Queries.GetCompanyBankAccount;
using NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Queries.GetCompanyBankAccounts;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.CompanyBankAccounts;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CompanyBankAccountController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("CompanyBankAccount.CreateCompanyBankAccount")]
    public async Task<IActionResult> CreateCompanyBankAccount(CreateCompanyBankAccountRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCompanyBankAccountCommand(
             request.CompanyId,
             request.BankId,
             request.BankAccountTypeId,
             request.BankAccountNumber,
             request.CciNumber,
             request.CurrencyId,
             request.IsPrimary,
             request.IsActive,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("CompanyBankAccount.UpdateCompanyBankAccount")]
    public async Task<IActionResult> UpdateCompanyBankAccount(UpdateCompanyBankAccountRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCompanyBankAccountCommand(
           request.Id,
             request.CompanyId,
             request.BankId,
             request.BankAccountTypeId,
             request.BankAccountNumber,
             request.CciNumber,
             request.CurrencyId,
             request.IsPrimary,
             request.IsActive,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("CompanyBankAccount.DeleteCompanyBankAccount")]
    public async Task<IActionResult> DeleteCompanyBankAccount(DeleteCompanyBankAccountRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteCompanyBankAccountCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("CompanyBankAccount.GetCompanyBankAccount")]
    public async Task<IActionResult> GetCompanyBankAccounts(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetCompanyBankAccountsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("CompanyBankAccount.GetCompanyBankAccount")]
    public async Task<IActionResult> GetCompanyBankAccount(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetCompanyBankAccountQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
