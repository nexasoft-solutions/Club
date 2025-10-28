namespace NexaSoft.Club.Api.Controllers.HumanResources.CostCenterTypes.Request;

public sealed record DeleteCostCenterTypeRequest(
   long Id,
   string DeletedBy
);
