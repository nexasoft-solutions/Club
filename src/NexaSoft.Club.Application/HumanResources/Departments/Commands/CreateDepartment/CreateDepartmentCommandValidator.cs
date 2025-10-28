using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.ParentDepartmentId)
            .GreaterThan(0).WithMessage("Este ParentDepartmentId debe ser mayor a cero.");
        // Validación personalizada para ParentDepartment de tipo Department
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
        RuleFor(x => x.ManagerId)
            .GreaterThan(0).WithMessage("Este ManagerId debe ser mayor a cero.");
        // Validación personalizada para EmployeeInfo de tipo EmployeeInfo
        RuleFor(x => x.CostCenterId)
            .GreaterThan(0).WithMessage("Este CostCenterId debe ser mayor a cero.");
        // Validación personalizada para CostCenter de tipo CostCenter
        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("El campo Location no puede estar vacío.");
        RuleFor(x => x.PhoneExtension)
            .NotEmpty().WithMessage("El campo PhoneExtension no puede estar vacío.");
    }
}
