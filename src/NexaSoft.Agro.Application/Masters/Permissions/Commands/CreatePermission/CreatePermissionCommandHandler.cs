
using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Permissions;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.CreatePermision;

public class CreatePermissionCommandHandler(IGenericRepository<Permission> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePermissionCommandHandler> _logger) : ICommandHandler<CreatePermissionCommand, long>
{
    public async Task<Result<long>> Handle(CreatePermissionCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de creación de Permiso");
        var validator = new CreatePermissionCommandValidator();
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
            return Result.Failure<long>(PermissionErrores.Duplicado);
        }


        var entity = Permission.Create(
            command.Name,
            command.Description,
            command.ReferenciaControl,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioCreacion
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Permiso con ID {PermisoId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Permiso");
            return Result.Failure<long>(PermissionErrores.ErrorSave);
        }
    }
}
