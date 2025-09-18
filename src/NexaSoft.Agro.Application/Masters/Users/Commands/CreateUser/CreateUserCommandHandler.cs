using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Users;
using static NexaSoft.Agro.Domain.Shareds.Enums;
using BC = BCrypt.Net.BCrypt;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.CreateUser;

public class CreateUserCommandHandler(
    IGenericRepository<User> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateUserCommandHandler> _logger
) : ICommandHandler<CreateUserCommand, long>
{
  public async Task<Result<long>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
  {

    _logger.LogInformation("Iniciando proceso de creación de User");
    var validator = new CreateUserCommandValidator();
    var validationResult = validator.Validate(command);
    if (!validationResult.IsValid)
    {
      var errors = validationResult.Errors
         .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

      throw new ValidationExceptions(errors);
    }

    var nombreCompleto = UserService.CreateNombreCompleto(command.UserApellidos ?? "", command.UserNombres ?? "");
    var userName = UserService.CreateUserName(command.UserApellidos ?? "", command.UserNombres ?? "");

    bool existsNombreCompleto = await _repository.ExistsAsync(c => c.NombreCompleto == nombreCompleto, cancellationToken);
    if (existsNombreCompleto)
    {
      return Result.Failure<long>(UserErrores.Duplicado);
    }

    bool existsUserName = await _repository.ExistsAsync(c => c.UserName == userName, cancellationToken);
    if (existsUserName)
    {
      return Result.Failure<long>(UserErrores.Duplicado);
    }

  
    bool existsEmail = await _repository.ExistsAsync(c => c.Email == command.Email, cancellationToken);
    if (existsEmail)
    {
      return Result.Failure<long>(UserErrores.Duplicado);
    }

    bool existsUserDni = await _repository.ExistsAsync(c => c.UserDni == command.UserDni, cancellationToken);
    if (existsUserDni)
    {
      return Result.Failure<long>(UserErrores.Duplicado);
    }

    bool existsUserTelefono = await _repository.ExistsAsync(c => c.UserTelefono == command.UserTelefono, cancellationToken);
    if (existsUserTelefono)
    {
      return Result.Failure<long>(UserErrores.Duplicado);
    }

    var entity = User.Create(
        command.UserApellidos,
        command.UserNombres,
        BC.HashPassword(command.Password),
            command.Email,
            command.UserDni,
            command.UserTelefono,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            userName
        );

    try
    {
      await _unitOfWork.BeginTransactionAsync(cancellationToken);
      await _repository.AddAsync(entity, cancellationToken);
      await _unitOfWork.SaveChangesAsync(cancellationToken);
      await _unitOfWork.CommitAsync(cancellationToken);
      _logger.LogInformation("User con ID {UserId} creado satisfactoriamente", entity.Id);

      return Result.Success(entity.Id);
    }
    catch (Exception ex)
    {
      await _unitOfWork.RollbackAsync(cancellationToken);
      _logger.LogError(ex, "Error al crear User");
      return Result.Failure<long>(UserErrores.ErrorSave);
    }
  }
}
