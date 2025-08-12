
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
    ILogger<CreateRoleCommandHandler> _logger) : ICommandHandler<CreateRoleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
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
            return Result.Failure<Guid>(RoleErrores.Duplicado);
        }


        var entity = Role.Create(
            command.Name,
            command.Description,
            _dateTimeProvider.CurrentTime.ToUniversalTime()
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
            return Result.Failure<Guid>(RoleErrores.ErrorSave);
        }
    }
}
