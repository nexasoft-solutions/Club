namespace NexaSoft.Club.Api.Controllers.Masters.MemberTypes.Request;

public sealed record CreateMemberTypeRequest(
    string? TypeName,
    string? Description,    
    bool? HasFamilyDiscount,
    decimal? FamilyDiscountRate,
    int? MaxFamilyMembers,
    string CreatedBy
);
