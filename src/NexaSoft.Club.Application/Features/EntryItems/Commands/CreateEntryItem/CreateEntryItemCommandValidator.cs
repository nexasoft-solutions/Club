using FluentValidation;

namespace NexaSoft.Club.Application.Features.EntryItems.Commands.CreateEntryItem;

public class CreateEntryItemCommandValidator : AbstractValidator<CreateEntryItemCommand>
{
    public CreateEntryItemCommandValidator()
    {
        // Validación personalizada para AccountingEntry de tipo AccountingEntry
        // Validación personalizada para AccountingChart de tipo AccountingChart
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
