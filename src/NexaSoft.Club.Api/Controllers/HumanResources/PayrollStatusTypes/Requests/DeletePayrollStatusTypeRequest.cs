namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollStatusTypes.Request;

public sealed record DeletePayrollStatusTypeRequest(
   long Id,
   string DeletedBy
);
