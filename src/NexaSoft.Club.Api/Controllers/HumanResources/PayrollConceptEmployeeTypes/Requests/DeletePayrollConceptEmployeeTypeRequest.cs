namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptEmployeeTypes.Request;

public sealed record DeletePayrollConceptEmployeeTypeRequest(
   long Id,
   string DeletedBy
);
