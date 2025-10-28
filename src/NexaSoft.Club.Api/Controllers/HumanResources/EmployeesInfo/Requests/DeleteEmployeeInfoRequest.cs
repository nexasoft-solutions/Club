namespace NexaSoft.Club.Api.Controllers.HumanResources.EmployeesInfo.Request;

public sealed record DeleteEmployeeInfoRequest(
   long Id,
   string DeletedBy
);
