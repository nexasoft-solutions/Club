using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.AccountingCharts.Request;
using NexaSoft.Club.Application.Masters.AccountingCharts.Commands.CreateAccountingChart;
using NexaSoft.Club.Application.Masters.AccountingCharts.Commands.UpdateAccountingChart;
using NexaSoft.Club.Application.Masters.AccountingCharts.Commands.DeleteAccountingChart;
using NexaSoft.Club.Application.Masters.AccountingCharts.Queries.GetAccountingChart;
using NexaSoft.Club.Application.Masters.AccountingCharts.Queries.GetAccountingCharts;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Application.Masters.AccountingCharts.Queries.GetTreeAccountingChart;

namespace NexaSoft.Club.Api.Controllers.Masters.AccountingCharts;

[Route("api/[controller]")]
[ApiController]
public class AccountingChartController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateAccountingChart(CreateAccountingChartRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAccountingChartCommand(
             request.AccountCode,
             request.AccountName,
             request.AccountTypeId,
             request.ParentAccountId,
             request.Level,
             request.AllowsTransactions,
             request.Description,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAccountingChart(UpdateAccountingChartRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateAccountingChartCommand(
             request.Id,
             request.AccountCode,
             request.AccountName,
             request.AccountTypeId,
             request.ParentAccountId,
             request.Level,
             request.AllowsTransactions,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccountingChart(DeleteAccountingChartRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteAccountingChartCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetAccountingCharts(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetAccountingChartsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAccountingChart(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetAccountingChartQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("tree")]
    public async Task<IActionResult> GetTreeAccountingChart(
        CancellationToken cancellationToken
     )
    {
        var query = new GetTreeAccountingChartQuery();
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
