using FluentValidation;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.CreateFeeConfiguration;

public class CreateFeeConfigurationCommandValidator : AbstractValidator<CreateFeeConfigurationCommand>
{
    public CreateFeeConfigurationCommandValidator()
    {
        // Validación personalizada para MemberType de tipo MemberType
        RuleFor(x => x.FeeName)
            .NotEmpty().WithMessage("El campo FeeName no puede estar vacío.");       
        RuleFor(x => x.DueDay)
            .GreaterThan(0).WithMessage("Este DueDay debe ser mayor a cero.");
        RuleFor(x => x.IncomeAccountId)
            .GreaterThan(0).WithMessage("Este IncomeAccountId debe ser mayor a cero.");
        // Validación personalizada para IncomeAccount de tipo AccountingChart
    }
}
