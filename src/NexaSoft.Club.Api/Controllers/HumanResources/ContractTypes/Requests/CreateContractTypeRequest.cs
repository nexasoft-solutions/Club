namespace NexaSoft.Club.Api.Controllers.HumanResources.ContractTypes.Request;

public sealed record CreateContractTypeRequest(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
);
