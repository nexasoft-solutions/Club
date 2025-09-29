using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.FamilyMembers;

namespace NexaSoft.Club.Application.Features.FamilyMembers.Commands.UpdateFamilyMember;

public class UpdateFamilyMemberCommandHandler(
    IGenericRepository<FamilyMember> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateFamilyMemberCommandHandler> _logger
) : ICommandHandler<UpdateFamilyMemberCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateFamilyMemberCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de FamilyMember con ID {FamilyMemberId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("FamilyMember con ID {FamilyMemberId} no encontrado", command.Id);
                return Result.Failure<bool>(FamilyMemberErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.MemberId,
            command.Dni,
            command.FirstName,
            command.LastName,
            command.Relationship,
            command.BirthDate,
            command.IsAuthorized,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("FamilyMember con ID {FamilyMemberId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar FamilyMember con ID {FamilyMemberId}", command.Id);
            return Result.Failure<bool>(FamilyMemberErrores.ErrorEdit);
        }
    }
}
