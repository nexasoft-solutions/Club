using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.WorkSchedules.Commands.CreateWorkSchedule;

public class CreateWorkScheduleCommandValidator : AbstractValidator<CreateWorkScheduleCommand>
{
    public CreateWorkScheduleCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("Este EmployeeId debe ser mayor a cero.");
        // Validación personalizada para EmployeeInfo de tipo EmployeeInfo
        // Validación personalizada para StartTime de tipo TimeOnly
        // Validación personalizada para EndTime de tipo TimeOnly
    }
}
