namespace NexaSoft.Club.Domain.Features.EntryItems;

public sealed record EntryItemResponse(
    long Id,
    long EntryId,
    string? EntryNumber,
    long AccountId,
    string? AccountName,
    decimal DebitAmount,
    decimal CreditAmount,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
