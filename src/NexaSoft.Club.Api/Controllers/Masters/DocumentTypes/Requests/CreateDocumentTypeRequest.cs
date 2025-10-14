namespace NexaSoft.Club.Api.Controllers.Masters.DocumentTypes.Request;

public sealed record CreateDocumentTypeRequest(
    string? Name,
    string? Description,
    string? Serie,
    string CreatedBy
);
