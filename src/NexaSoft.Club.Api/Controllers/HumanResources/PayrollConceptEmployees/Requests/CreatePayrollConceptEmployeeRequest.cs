namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptEmployees.Request;

public sealed record CreatePayrollConceptEmployeeRequest(
    long? PayrollConceptId,
    long? EmployeeId,
    string CreatedBy
);
