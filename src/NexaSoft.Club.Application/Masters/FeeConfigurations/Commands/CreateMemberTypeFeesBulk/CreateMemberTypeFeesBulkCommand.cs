using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.CreateMemberTypeFeesBulk;

public sealed record CreateMemberTypeFeesBulkCommand(
    long MemberTypeId,
    List<CreateMemberTypeFeeDto> Fees,
    string CreatedBy
) : ICommand<int>; // devolvemos cuántos se crearon

public record CreateMemberTypeFeeDto(
    long FeeConfigurationId,
    decimal? Amount // puede ser null si se usa DefaultAmount
);