namespace NexaSoft.Club.Api.Controllers.Features.EntryItems.Request;

public sealed record DeleteEntryItemRequest(
   long Id,
   string DeletedBy
);
