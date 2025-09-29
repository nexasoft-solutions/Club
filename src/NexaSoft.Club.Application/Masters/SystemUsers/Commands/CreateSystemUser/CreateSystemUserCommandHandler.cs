using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SystemUsers;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Masters.SystemUsers.Commands.CreateSystemUser;

public class CreateSystemUserCommandHandler(
    IGenericRepository<SystemUser> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateSystemUserCommandHandler> _logger
) : ICommandHandler<CreateSystemUserCommand, long>
{
    public async Task<Result<long>> Handle(CreateSystemUserCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de SystemUser");

     bool existsUsername = await _repository.ExistsAsync(c => c.Username == command.Username, cancellationToken);
     if (existsUsername)
     {
       return Result.Failure<long>(SystemUserErrores.Duplicado);
     }

     bool existsEmail = await _repository.ExistsAsync(c => c.Email == command.Email, cancellationToken);
     if (existsEmail)
     {
       return Result.Failure<long>(SystemUserErrores.Duplicado);
     }

        var entity = SystemUser.Create(
            command.Username,
            command.Email,
            command.FirstName,
            command.LastName,
            command.Role,
            command.IsActive,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("SystemUser con ID {SystemUserId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear SystemUser");
            return Result.Failure<long>(SystemUserErrores.ErrorSave);
        }
    }
}
