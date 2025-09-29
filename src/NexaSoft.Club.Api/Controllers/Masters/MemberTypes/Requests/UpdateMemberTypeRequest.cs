namespace NexaSoft.Club.Api.Controllers.Masters.MemberTypes.Request;

public sealed record UpdateMemberTypeRequest(
    long Id,
    string? TypeName,
    string? Description,    
    bool? HasFamilyDiscount,
    decimal? FamilyDiscountRate,
    int? MaxFamilyMembers,
    string UpdatedBy
);
