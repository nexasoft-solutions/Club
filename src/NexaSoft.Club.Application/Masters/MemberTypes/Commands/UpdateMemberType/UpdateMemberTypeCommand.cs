using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.MemberTypes.Commands.UpdateMemberType;

public sealed record UpdateMemberTypeCommand(
    long Id,
    string? TypeName,
    string? Description,  
    bool? HasFamilyDiscount,
    decimal? FamilyDiscountRate,
    int? MaxFamilyMembers,  
    string UpdatedBy
) : ICommand<bool>;
