namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptEmployeeTypes.Request;

public sealed record UpdatePayrollConceptEmployeeTypeRequest(
   long Id,
    long? PayrollConceptId,
    long? EmployeeTypeId,
    string UpdatedBy
);
