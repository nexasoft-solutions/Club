namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptDepartments.Request;

public sealed record DeletePayrollConceptDepartmentRequest(
   long Id,
   string DeletedBy
);
