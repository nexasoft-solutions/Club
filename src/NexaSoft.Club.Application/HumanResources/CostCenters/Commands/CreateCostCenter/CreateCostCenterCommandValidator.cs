using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.CostCenters.Commands.CreateCostCenter;

public class CreateCostCenterCommandValidator : AbstractValidator<CreateCostCenterCommand>
{
    public CreateCostCenterCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.ParentCostCenterId)
            .GreaterThan(0).WithMessage("Este ParentCostCenterId debe ser mayor a cero.");
        // Validación personalizada para ParentCostCenter de tipo CostCenter
        RuleFor(x => x.CostCenterTypeId)
            .GreaterThan(0).WithMessage("Este CostCenterTypeId debe ser mayor a cero.");
        // Validación personalizada para CostCenterType de tipo CostCenterType
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
        RuleFor(x => x.ResponsibleId)
            .GreaterThan(0).WithMessage("Este ResponsibleId debe ser mayor a cero.");
        // Validación personalizada para EmployeeInfo de tipo EmployeeInfo
    }
}
