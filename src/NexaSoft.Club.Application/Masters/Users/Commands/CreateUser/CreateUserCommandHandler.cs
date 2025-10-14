using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Users;
using static NexaSoft.Club.Domain.Shareds.Enums;
using BC = BCrypt.Net.BCrypt;

namespace NexaSoft.Club.Application.Masters.Users.Commands.CreateUser;

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


    var fullName = UserService.CreateFullName(command.LastName ?? "", command.FirstName ?? "");
    var userName = UserService.CreateUserName(command.LastName ?? "", command.FirstName ?? "");

    bool existsFullName = await _repository.ExistsAsync(c => c.FullName == fullName, cancellationToken);
    if (existsFullName)
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

    bool existsDni = await _repository.ExistsAsync(c => c.Dni == command.Dni, cancellationToken);
    if (existsDni)
    {
      return Result.Failure<long>(UserErrores.Duplicado);
    }

    bool existsPhone = await _repository.ExistsAsync(c => c.Phone == command.Phone, cancellationToken);
    if (existsPhone)
    {
      return Result.Failure<long>(UserErrores.Duplicado);
    }

    var entity = User.Create(
        command.LastName,
        command.FirstName,
        command.Email,
        command.Dni,
        command.Phone,
        command.UserTypeId,
        command.BirthDate,
        command.MemberId,
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
