using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.Positions.Commands.CreatePosition;

public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
{
    public CreatePositionCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("Este DepartmentId debe ser mayor a cero.");
        // Validación personalizada para Department de tipo Department
        RuleFor(x => x.EmployeeTypeId)
            .GreaterThan(0).WithMessage("Este EmployeeTypeId debe ser mayor a cero.");
        // Validación personalizada para EmployeeType de tipo EmployeeType
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
