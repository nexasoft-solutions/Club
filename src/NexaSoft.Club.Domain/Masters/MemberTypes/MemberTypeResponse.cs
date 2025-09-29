namespace NexaSoft.Club.Domain.Masters.MemberTypes;

public sealed record MemberTypeResponse(
    long Id,
    string? TypeName,
    string? Description,
    //decimal MonthlyFee,
    //decimal EntryFee,
    bool? HasFamilyDiscount,
    decimal? FamilyDiscountRate,
    int? MaxFamilyMembers,
    //long? IncomeAccountId,
    //string? AccountName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
