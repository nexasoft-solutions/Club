using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.SpacePhotos.Request;
using NexaSoft.Club.Application.Masters.SpacePhotos.Commands.CreateSpacePhoto;
using NexaSoft.Club.Application.Masters.SpacePhotos.Commands.UpdateSpacePhoto;
using NexaSoft.Club.Application.Masters.SpacePhotos.Commands.DeleteSpacePhoto;
using NexaSoft.Club.Application.Masters.SpacePhotos.Queries.GetSpacePhoto;
using NexaSoft.Club.Application.Masters.SpacePhotos.Queries.GetSpacePhotos;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.Masters.SpacePhotos;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SpacePhotoController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("SpacePhoto.CreateSpacePhoto")]
    public async Task<IActionResult> CreateSpacePhoto([FromForm] CreateSpacePhotoRequest request, CancellationToken cancellationToken)
    {
        using var stream = request.PhotoFile.OpenReadStream();

        var command = new CreateSpacePhotoCommand(
            request.SpaceId,
            stream,                          // ✅ Stream (correcto)
            request.PhotoFile.FileName,      // OriginalFileName
            request.PhotoFile.ContentType,   // ContentType
            request.Order,
            request.Description,
            request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("SpacePhoto.UpdateSpacePhoto")]
    public async Task<IActionResult> UpdateSpacePhoto(UpdateSpacePhotoRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSpacePhotoCommand(
             request.Id,
             request.SpaceId,
             request.PhotoUrl,
             request.Order,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("SpacePhoto.DeleteSpacePhoto")]
    public async Task<IActionResult> DeleteSpacePhoto(DeleteSpacePhotoRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteSpacePhotoCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("SpacePhoto.GetSpacePhoto")]
    public async Task<IActionResult> GetSpacePhotos(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSpacePhotosQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("SpacePhoto.GetSpacePhoto")]
    public async Task<IActionResult> GetSpacePhoto(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetSpacePhotoQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
