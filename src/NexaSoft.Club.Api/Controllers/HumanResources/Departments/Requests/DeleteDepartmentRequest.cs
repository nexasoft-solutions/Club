namespace NexaSoft.Club.Api.Controllers.HumanResources.Departments.Request;

public sealed record DeleteDepartmentRequest(
   long Id,
   string DeletedBy
);
