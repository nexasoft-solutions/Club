using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.MemberTypes.Commands.CreateMemberType;

public sealed record CreateMemberTypeCommand(
    string? TypeName,
    string? Description,
    //decimal MonthlyFee,
    //decimal EntryFee,
    bool? HasFamilyDiscount,
    decimal? FamilyDiscountRate,
    int? MaxFamilyMembers,
    //long? IncomeAccountId,
    string CreatedBy
) : ICommand<long>;
