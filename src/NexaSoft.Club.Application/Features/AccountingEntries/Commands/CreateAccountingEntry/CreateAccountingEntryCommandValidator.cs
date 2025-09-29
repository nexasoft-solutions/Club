using FluentValidation;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Commands.CreateAccountingEntry;

public class CreateAccountingEntryCommandValidator : AbstractValidator<CreateAccountingEntryCommand>
{
    public CreateAccountingEntryCommandValidator()
    {
        RuleFor(x => x.EntryNumber)
            .NotEmpty().WithMessage("El campo EntryNumber no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
        /*RuleFor(x => x.SourceModule)
            .NotEmpty().WithMessage("El campo SourceModule no puede estar vacío.");
        RuleFor(x => x.SourceId)
            .GreaterThan(0).WithMessage("Este SourceId debe ser mayor a cero.");
        RuleFor(x => x.AdjustmentReason)
            .NotEmpty().WithMessage("El campo AdjustmentReason no puede estar vacío.");*/
    }
}
