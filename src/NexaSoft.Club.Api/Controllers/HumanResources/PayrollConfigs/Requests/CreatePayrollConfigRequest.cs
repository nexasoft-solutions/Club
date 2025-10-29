namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConfigs.Request;

public sealed record CreatePayrollConfigRequest(
    long? CompanyId,
    long? PayPeriodTypeId,
    decimal RegularHoursPerDay,
    int WorkDaysPerWeek,
    string CreatedBy
);
