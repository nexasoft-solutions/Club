namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptEmployeeTypes.Request;

public sealed record CreatePayrollConceptEmployeeTypeRequest(
    long? PayrollConceptId,
    long? EmployeeTypeId,
    string CreatedBy
);
