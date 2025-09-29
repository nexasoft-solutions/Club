using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Application.Masters.MenuItems.Commands.CreateMenu;

public class CreateMenuCommandHandler(
    IGenericRepository<MenuItemOption> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateMenuCommandHandler> _logger
) : ICommandHandler<CreateMenuCommand, long>
{
    public async Task<Result<long>> Handle(CreateMenuCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de creación de Menu");
        var validator = new CreateMenuCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }


        bool existsValor = await _repository.ExistsAsync(c => c.Label == command.Label, cancellationToken);
        if (existsValor)
        {
            return Result.Failure<long>(MenuItemErrores.Duplicado);
        }
     
        var entity = MenuItemOption.Create(
            command.Label,
            command.Icon,
            command.Route,
            command.ParentId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioCreacion
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Menu con ID {MenuId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Menu");
            return Result.Failure<long>(MenuItemErrores.ErrorSave);
        }
    }
}
