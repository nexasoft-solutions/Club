namespace NexaSoft.Club.Domain.HumanResources.PayrollConfigs;

public sealed record PayrollConfigResponse(
    long Id,
    long? CompanyId,
    string? BusinessName,
    long? PayPeriodTypeId,
    string? PayPeriodTypeCode,
    decimal RegularHoursPerDay,
    int WorkDaysPerWeek,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
