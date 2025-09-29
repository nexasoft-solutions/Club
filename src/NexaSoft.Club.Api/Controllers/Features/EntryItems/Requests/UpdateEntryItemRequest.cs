namespace NexaSoft.Club.Api.Controllers.Features.EntryItems.Request;

public sealed record UpdateEntryItemRequest(
   long Id,
    long EntryId,
    long AccountId,
    decimal DebitAmount,
    decimal CreditAmount,
    string? Description,
    string UpdatedBy
);
