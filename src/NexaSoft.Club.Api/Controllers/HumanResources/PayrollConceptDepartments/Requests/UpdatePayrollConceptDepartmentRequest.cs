namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptDepartments.Request;

public sealed record UpdatePayrollConceptDepartmentRequest(
   long Id,
    long? PayrollConceptId,
    long? DepartmentId,
    string UpdatedBy
);
