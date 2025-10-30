namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptEmployees.Request;

public sealed record UpdatePayrollConceptEmployeeRequest(
   long Id,
    long? PayrollConceptId,
    long? EmployeeId,
    string UpdatedBy
);
