using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.MemberStatuses;

namespace NexaSoft.Club.Application.Masters.MemberStatuses.Commands.UpdateMemberStatus;

public class UpdateMemberStatusCommandHandler(
    IGenericRepository<MemberStatus> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateMemberStatusCommandHandler> _logger
) : ICommandHandler<UpdateMemberStatusCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateMemberStatusCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de MemberStatus con ID {MemberStatusId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("MemberStatus con ID {MemberStatusId} no encontrado", command.Id);
                return Result.Failure<bool>(MemberStatusErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.StatusName,
            command.Description,
            command.CanAccess,
            command.CanReserve,
            command.CanParticipate,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("MemberStatus con ID {MemberStatusId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar MemberStatus con ID {MemberStatusId}", command.Id);
            return Result.Failure<bool>(MemberStatusErrores.ErrorEdit);
        }
    }
}
