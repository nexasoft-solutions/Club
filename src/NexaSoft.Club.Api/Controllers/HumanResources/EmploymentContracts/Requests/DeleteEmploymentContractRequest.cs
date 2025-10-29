namespace NexaSoft.Club.Api.Controllers.HumanResources.EmploymentContracts.Request;

public sealed record DeleteEmploymentContractRequest(
   long Id,
   string DeletedBy
);
