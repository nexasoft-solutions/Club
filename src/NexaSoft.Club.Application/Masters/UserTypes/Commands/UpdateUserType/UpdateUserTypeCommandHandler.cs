using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.UserTypes;

namespace NexaSoft.Club.Application.Masters.UserTypes.Commands.UpdateUserType;

public class UpdateUserTypeCommandHandler(
    IGenericRepository<UserType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateUserTypeCommandHandler> _logger
) : ICommandHandler<UpdateUserTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateUserTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de UserType con ID {UserTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("UserType con ID {UserTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(UserTypeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Name,
            command.Description,
            command.IsAdministrative,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("UserType con ID {UserTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar UserType con ID {UserTypeId}", command.Id);
            return Result.Failure<bool>(UserTypeErrores.ErrorEdit);
        }
    }
}
