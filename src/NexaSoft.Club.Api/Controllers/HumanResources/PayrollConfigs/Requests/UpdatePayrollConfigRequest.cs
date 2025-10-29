namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConfigs.Request;

public sealed record UpdatePayrollConfigRequest(
   long Id,
    long? CompanyId,
    long? PayPeriodTypeId,
    decimal RegularHoursPerDay,
    int WorkDaysPerWeek,
    string UpdatedBy
);
