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
) : ICommandHandler<CreateUserCommand, Guid>
{
  public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
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
      return Result.Failure<Guid>(UserErrores.Duplicado);
    }

    bool existsUserName = await _repository.ExistsAsync(c => c.UserName == userName, cancellationToken);
    if (existsUserName)
    {
      return Result.Failure<Guid>(UserErrores.Duplicado);
    }

    bool existsPassword = await _repository.ExistsAsync(c => c.Password == command.Password, cancellationToken);
    if (existsPassword)
    {
      return Result.Failure<Guid>(UserErrores.Duplicado);
    }

    bool existsEmail = await _repository.ExistsAsync(c => c.Email == command.Email, cancellationToken);
    if (existsEmail)
    {
      return Result.Failure<Guid>(UserErrores.Duplicado);
    }

    bool existsUserDni = await _repository.ExistsAsync(c => c.UserDni == command.UserDni, cancellationToken);
    if (existsUserDni)
    {
      return Result.Failure<Guid>(UserErrores.Duplicado);
    }

    bool existsUserTelefono = await _repository.ExistsAsync(c => c.UserTelefono == command.UserTelefono, cancellationToken);
    if (existsUserTelefono)
    {
      return Result.Failure<Guid>(UserErrores.Duplicado);
    }

    var entity = User.Create(
        command.UserApellidos,
        command.UserNombres,
        //command.NombreCompleto,
        //command.UserName,
        BC.HashPassword(command.Password),
            command.Email,
            command.UserDni,
            command.UserTelefono,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime()
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
      return Result.Failure<Guid>(UserErrores.ErrorSave);
    }
  }
}
