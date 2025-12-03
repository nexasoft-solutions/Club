using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.DocumentTypes.Request;
using NexaSoft.Club.Application.Masters.DocumentTypes.Commands.CreateDocumentType;
using NexaSoft.Club.Application.Masters.DocumentTypes.Commands.UpdateDocumentType;
using NexaSoft.Club.Application.Masters.DocumentTypes.Commands.DeleteDocumentType;
using NexaSoft.Club.Application.Masters.DocumentTypes.Queries.GetDocumentType;
using NexaSoft.Club.Application.Masters.DocumentTypes.Queries.GetDocumentTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.Masters.DocumentTypes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DocumentTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("DocumentType.CreateDocumentType")]
    public async Task<IActionResult> CreateDocumentType(CreateDocumentTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateDocumentTypeCommand(
             request.Name,
             request.Description,
             request.Serie,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("DocumentType.UpdateDocumentType")]
    public async Task<IActionResult> UpdateDocumentType(UpdateDocumentTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateDocumentTypeCommand(
             request.Id,
             request.Name,
             request.Description,
             request.Serie,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("DocumentType.DeleteDocumentType")]
    public async Task<IActionResult> DeleteDocumentType(DeleteDocumentTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteDocumentTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("DocumentType.GetDocumentType")]
    public async Task<IActionResult> GetDocumentTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetDocumentTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("DocumentType.GetDocumentType")]
    public async Task<IActionResult> GetDocumentType(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetDocumentTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
