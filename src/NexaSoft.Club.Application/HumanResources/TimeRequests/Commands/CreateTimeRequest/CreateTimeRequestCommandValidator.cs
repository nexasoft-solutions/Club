using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.TimeRequests.Commands.CreateTimeRequest;

public class CreateTimeRequestCommandValidator : AbstractValidator<CreateTimeRequestCommand>
{
    public CreateTimeRequestCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("Este EmployeeId debe ser mayor a cero.");
        // Validación personalizada para EmployeeInfo de tipo EmployeeInfo
        RuleFor(x => x.TimeRequestTypeId)
            .GreaterThan(0).WithMessage("Este TimeRequestTypeId debe ser mayor a cero.");
        // Validación personalizada para TimeRequestType de tipo TimeRequestType
        // Validación personalizada para StartDate de tipo DateOnly
        // Validación personalizada para EndDate de tipo DateOnly
        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Este StatusId debe ser mayor a cero.");
        // Validación personalizada para Status de tipo Status
    }
}
