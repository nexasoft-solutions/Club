using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Features.Reservations.Request;
using NexaSoft.Club.Application.Features.Reservations.Commands.CreateReservation;
using NexaSoft.Club.Application.Features.Reservations.Commands.DeleteReservation;
using NexaSoft.Club.Application.Features.Reservations.Queries.GetReservation;
using NexaSoft.Club.Application.Features.Reservations.Queries.GetReservations;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Api.Controllers.Features.Reservations;

[Route("api/[controller]")]
[ApiController]
public class ReservationController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateReservation(CreateReservationRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateReservationCommand(
             request.MemberId,
             request.SpaceRateId,
             request.SpaceAvailabilityId,
             request.Date,
             request.StartTime,
             request.EndTime,
             (int)StatusEnum.Pendiente, // Default status when creating a reservation
             request.PaymentMethodId,
             request.ReferenceNumber,
             request.DocumentTypeId,
             request.ReceiptNumber,
             request.TotalAmount,
             request.AccountingEntryId,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

  

    [HttpDelete]
    public async Task<IActionResult> DeleteReservation(DeleteReservationRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteReservationCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetReservationsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetReservation(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetReservationQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
