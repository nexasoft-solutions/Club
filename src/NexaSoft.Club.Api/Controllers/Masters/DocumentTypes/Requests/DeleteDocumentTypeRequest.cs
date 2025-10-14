namespace NexaSoft.Club.Api.Controllers.Masters.DocumentTypes.Request;

public sealed record DeleteDocumentTypeRequest(
   long Id,
   string DeletedBy
);
