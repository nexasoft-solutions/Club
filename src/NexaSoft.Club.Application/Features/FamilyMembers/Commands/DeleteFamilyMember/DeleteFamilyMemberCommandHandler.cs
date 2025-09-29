using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.FamilyMembers;

namespace NexaSoft.Club.Application.Features.FamilyMembers.Commands.DeleteFamilyMember;

public class DeleteFamilyMemberCommandHandler(
    IGenericRepository<FamilyMember> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteFamilyMemberCommandHandler> _logger
) : ICommandHandler<DeleteFamilyMemberCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteFamilyMemberCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de FamilyMember con ID {FamilyMemberId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("FamilyMember con ID {FamilyMemberId} no encontrado", command.Id);
                return Result.Failure<bool>(FamilyMemberErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar FamilyMember con ID {FamilyMemberId}", command.Id);
            return Result.Failure<bool>(FamilyMemberErrores.ErrorDelete);
        }
    }
}
