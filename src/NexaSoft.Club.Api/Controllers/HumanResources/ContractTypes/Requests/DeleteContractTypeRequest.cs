namespace NexaSoft.Club.Api.Controllers.HumanResources.ContractTypes.Request;

public sealed record DeleteContractTypeRequest(
   long Id,
   string DeletedBy
);
