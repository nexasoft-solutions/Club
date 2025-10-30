namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptDepartments.Request;

public sealed record CreatePayrollConceptDepartmentRequest(
    long? PayrollConceptId,
    long? DepartmentId,
    string CreatedBy
);
