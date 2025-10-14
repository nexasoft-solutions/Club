using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.UserTypes;

namespace NexaSoft.Club.Application.Masters.UserTypes.Commands.DeleteUserType;

public class DeleteUserTypeCommandHandler(
    IGenericRepository<UserType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteUserTypeCommandHandler> _logger
) : ICommandHandler<DeleteUserTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteUserTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de UserType con ID {UserTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("UserType con ID {UserTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(UserTypeErrores.NoEncontrado);
            }

         entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(),command.DeletedBy);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al eliminar UserType con ID {UserTypeId}", command.Id);
            return Result.Failure<bool>(UserTypeErrores.ErrorDelete);
        }
    }
}
