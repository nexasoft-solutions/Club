namespace NexaSoft.Club.Api.Controllers.HumanResources.ContractTypes.Request;

public sealed record UpdateContractTypeRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
