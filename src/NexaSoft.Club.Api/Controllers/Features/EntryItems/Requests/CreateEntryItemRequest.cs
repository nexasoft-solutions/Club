namespace NexaSoft.Club.Api.Controllers.Features.EntryItems.Request;

public sealed record CreateEntryItemRequest(
    long EntryId,
    long AccountId,
    decimal DebitAmount,
    decimal CreditAmount,
    string? Description,
    string CreatedBy
);
