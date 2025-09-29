using FluentValidation;

namespace NexaSoft.Club.Application.Features.ExpensesVouchers.Commands.CreateExpenseVoucher;

public class CreateExpenseVoucherCommandValidator : AbstractValidator<CreateExpenseVoucherCommand>
{
    public CreateExpenseVoucherCommandValidator()
    {
        RuleFor(x => x.EntryId)
            .GreaterThan(0).WithMessage("Este EntryId debe ser mayor a cero.");
        // Validación personalizada para AccountingEntry de tipo AccountingEntry
        RuleFor(x => x.VoucherNumber)
            .NotEmpty().WithMessage("El campo VoucherNumber no puede estar vacío.");
        RuleFor(x => x.SupplierName)
            .NotEmpty().WithMessage("El campo SupplierName no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
        // Validación personalizada para ExpenseAccount de tipo AccountingChart
    }
}
