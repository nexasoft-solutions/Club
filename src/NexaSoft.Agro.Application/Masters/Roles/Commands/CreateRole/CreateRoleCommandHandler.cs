
using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Roles;

namespace NexaSoft.Agro.Application.Masters.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler(IGenericRepository<Role> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateRoleCommandHandler> _logger) : ICommandHandler<CreateRoleCommand, long>
{
    public async Task<Result<long>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de creación de Rol");
        var validator = new CreateRoleCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }

        bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
        if (existsName)
        {
            return Result.Failure<long>(RoleErrores.Duplicado);
        }


        var entity = Role.Create(
            command.Name,
            command.Description,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioCreacion
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Rol con ID {RoleId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Rol");
            return Result.Failure<long>(RoleErrores.ErrorSave);
        }
    }
}
