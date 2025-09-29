namespace NexaSoft.Club.Api.Controllers.Masters.FeeConfigurations.Requests;

public sealed record CreateMemberTypeFeesBulkRequest(
    long MemberTypeId,
    List<MemberTypeFeeDto> Fees,
    string CreatedBy
);

public sealed record MemberTypeFeeDto(
    long FeeConfigurationId,
    decimal? Amount
);