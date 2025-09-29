using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Application.Features.Members.Commands.UpdateMember;

public class UpdateMemberCommandHandler(
    IGenericRepository<Member> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateMemberCommandHandler> _logger
) : ICommandHandler<UpdateMemberCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateMemberCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de Member con ID {MemberId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Member con ID {MemberId} no encontrado", command.Id);
                return Result.Failure<bool>(MemberErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Dni,
            command.FirstName,
            command.LastName,
            command.Email,
            command.Phone,
            command.Address,
            command.BirthDate,
            command.MemberTypeId,
            command.StatusId,
            command.JoinDate,
            command.ExpirationDate,
            command.Balance,
            command.QrCode,
            command.QrExpiration,
            command.ProfilePictureUrl,
            command.EntryFeePaid,
            command.LastPaymentDate,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Member con ID {MemberId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar Member con ID {MemberId}", command.Id);
            return Result.Failure<bool>(MemberErrores.ErrorEdit);
        }
    }
}
