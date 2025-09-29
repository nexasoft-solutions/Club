using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.FeeConfigurations.Request;
using NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.CreateFeeConfiguration;
using NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.UpdateFeeConfiguration;
using NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.DeleteFeeConfiguration;
using NexaSoft.Club.Application.Masters.FeeConfigurations.Queries.GetFeeConfiguration;
using NexaSoft.Club.Application.Masters.FeeConfigurations.Queries.GetFeeConfigurations;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Api.Controllers.Masters.FeeConfigurations.Requests;
using NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.CreateMemberTypeFeesBulk;
using NexaSoft.Club.Application.Masters.FeeConfigurations.Queries.GetMemberTypeFee;

namespace NexaSoft.Club.Api.Controllers.Masters.FeeConfigurations;

[Route("api/[controller]")]
[ApiController]
public class FeeConfigurationController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateFeeConfiguration(CreateFeeConfigurationRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateFeeConfigurationCommand(
             request.FeeName,
             request.PeriodicityId,
             request.DueDay,
             request.DefaultAmount,
             request.IsVariable,
             request.Priority,
             request.AppliesToFamily,
             request.IncomeAccountId,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPost("bulk-member-type-fees")]
    public async Task<IActionResult> CreateBulk(CreateMemberTypeFeesBulkRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMemberTypeFeesBulkCommand(
            request.MemberTypeId,
            request.Fees.Select(f => new CreateMemberTypeFeeDto(
                f.FeeConfigurationId,
                f.Amount
            )).ToList(),
            request.CreatedBy
        );

        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFeeConfiguration(UpdateFeeConfigurationRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateFeeConfigurationCommand(
             request.Id,
             request.FeeName,
             request.PeriodicityId,
             request.DueDay,
             request.DefaultAmount,
             request.IsVariable,
             request.Priority,
             request.AppliesToFamily,
             request.IncomeAccountId,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFeeConfiguration(DeleteFeeConfigurationRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteFeeConfigurationCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetFeeConfigurations(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetFeeConfigurationsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("member-type-fees")]
    public async Task<IActionResult> GetMemberTypeFees(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetMemberTypeFeeQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetFeeConfiguration(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetFeeConfigurationQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
