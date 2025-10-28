namespace NexaSoft.Club.Api.Controllers.HumanResources.CostCenters.Request;

public sealed record DeleteCostCenterRequest(
   long Id,
   string DeletedBy
);
