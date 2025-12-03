namespace NexaSoft.Club.Application.Masters.Users.Commands.ChangePassword;

using FluentValidation;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
	public ChangePasswordCommandValidator()
	{
		RuleFor(x => x.NewPassword)
			.NotEmpty().WithMessage("La contraseña no puede estar vacía.")
			.MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
			.Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
			.Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula.")
			.Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número.")
			.Matches("[!@#$%^&*(),.?\":{}|<>]|").WithMessage("La contraseña debe contener al menos un carácter especial.");
	}
}
