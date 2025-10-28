namespace NexaSoft.Club.Api.Controllers.HumanResources.EmployeeTypes.Request;

public sealed record DeleteEmployeeTypeRequest(
   long Id,
   string DeletedBy
);
