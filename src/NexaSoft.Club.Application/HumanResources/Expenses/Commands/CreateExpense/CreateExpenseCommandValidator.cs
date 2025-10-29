using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.Expenses.Commands.CreateExpense;

public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
    {
        RuleFor(x => x.CostCenterId)
            .GreaterThan(0).WithMessage("Este CostCenterId debe ser mayor a cero.");
        // Validación personalizada para CostCenter de tipo CostCenter
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
        // Validación personalizada para ExpenseDate de tipo DateOnly
        RuleFor(x => x.DocumentNumber)
            .NotEmpty().WithMessage("El campo DocumentNumber no puede estar vacío.");
        RuleFor(x => x.DocumentPath)
            .NotEmpty().WithMessage("El campo DocumentPath no puede estar vacío.");
    }
}
