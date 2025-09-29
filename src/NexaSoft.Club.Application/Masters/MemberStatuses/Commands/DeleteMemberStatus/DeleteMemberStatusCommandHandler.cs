using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.MemberStatuses;

namespace NexaSoft.Club.Application.Masters.MemberStatuses.Commands.DeleteMemberStatus;

public class DeleteMemberStatusCommandHandler(
    IGenericRepository<MemberStatus> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteMemberStatusCommandHandler> _logger
) : ICommandHandler<DeleteMemberStatusCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteMemberStatusCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de MemberStatus con ID {MemberStatusId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("MemberStatus con ID {MemberStatusId} no encontrado", command.Id);
                return Result.Failure<bool>(MemberStatusErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar MemberStatus con ID {MemberStatusId}", command.Id);
            return Result.Failure<bool>(MemberStatusErrores.ErrorDelete);
        }
    }
}
