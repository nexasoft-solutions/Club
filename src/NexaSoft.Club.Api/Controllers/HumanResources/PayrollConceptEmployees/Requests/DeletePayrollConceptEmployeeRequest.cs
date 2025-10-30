namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptEmployees.Request;

public sealed record DeletePayrollConceptEmployeeRequest(
   long Id,
   string DeletedBy
);
