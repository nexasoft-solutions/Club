using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.AttendanceRecords.Commands.CreateAttendanceRecord;

public class CreateAttendanceRecordCommandValidator : AbstractValidator<CreateAttendanceRecordCommand>
{
    public CreateAttendanceRecordCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("Este EmployeeId debe ser mayor a cero.");
        // Validación personalizada para EmployeeInfo de tipo EmployeeInfo
        RuleFor(x => x.CostCenterId)
            .GreaterThan(0).WithMessage("Este CostCenterId debe ser mayor a cero.");
        // Validación personalizada para CostCenter de tipo CostCenter
        // Validación personalizada para CheckInTime de tipo TimeOnly
        // Validación personalizada para CheckOutTime de tipo TimeOnly
        // Validación personalizada para TotalHours de tipo decimal
        // Validación personalizada para RegularHours de tipo decimal
        // Validación personalizada para OvertimeHours de tipo decimal
        // Validación personalizada para SundayHours de tipo decimal
        // Validación personalizada para HolidayHours de tipo decimal
        // Validación personalizada para NightHours de tipo decimal
        RuleFor(x => x.LateMinutes)
            .GreaterThan(0).WithMessage("Este LateMinutes debe ser mayor a cero.");
        RuleFor(x => x.EarlyDepartureMinutes)
            .GreaterThan(0).WithMessage("Este EarlyDepartureMinutes debe ser mayor a cero.");
        RuleFor(x => x.AttendanceStatusTypeId)
            .GreaterThan(0).WithMessage("Este AttendanceStatusTypeId debe ser mayor a cero.");
        // Validación personalizada para AttendanceStatusType de tipo AttendanceStatusType
    }
}
