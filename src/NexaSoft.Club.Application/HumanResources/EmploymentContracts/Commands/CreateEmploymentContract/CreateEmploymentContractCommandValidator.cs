using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.EmploymentContracts.Commands.CreateEmploymentContract;

public class CreateEmploymentContractCommandValidator : AbstractValidator<CreateEmploymentContractCommand>
{
    public CreateEmploymentContractCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("Este EmployeeId debe ser mayor a cero.");
        // Validación personalizada para EmployeeInfo de tipo EmployeeInfo
        RuleFor(x => x.ContractTypeId)
            .GreaterThan(0).WithMessage("Este ContractTypeId debe ser mayor a cero.");
        // Validación personalizada para ContractType de tipo ContractType
        RuleFor(x => x.DocumentPath)
            .NotEmpty().WithMessage("El campo DocumentPath no puede estar vacío.");
    }
}
