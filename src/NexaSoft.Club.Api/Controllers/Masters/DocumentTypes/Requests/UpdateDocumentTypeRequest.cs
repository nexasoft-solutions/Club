namespace NexaSoft.Club.Api.Controllers.Masters.DocumentTypes.Request;

public sealed record UpdateDocumentTypeRequest(
    long Id,
    string? Name,
    string? Description,
    string? Serie,
    string UpdatedBy
);
