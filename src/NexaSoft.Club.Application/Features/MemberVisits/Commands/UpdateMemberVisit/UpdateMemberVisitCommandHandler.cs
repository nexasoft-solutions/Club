using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberVisits;

namespace NexaSoft.Club.Application.Features.MemberVisits.Commands.UpdateMemberVisit;

public class UpdateMemberVisitCommandHandler(
    IGenericRepository<MemberVisit> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateMemberVisitCommandHandler> _logger
) : ICommandHandler<UpdateMemberVisitCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateMemberVisitCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de MemberVisit con ID {MemberVisitId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("MemberVisit con ID {MemberVisitId} no encontrado", command.Id);
            return Result.Failure<bool>(MemberVisitErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.MemberId,
            command.VisitDate,
            command.EntryTime,
            command.ExitTime,
            command.QrCodeUsed,
            command.Notes,
            command.CheckInBy,
            command.CheckOutBy,
            command.VisitType,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("MemberVisit con ID {MemberVisitId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar MemberVisit con ID {MemberVisitId}", command.Id);
            return Result.Failure<bool>(MemberVisitErrores.ErrorEdit);
        }
    }
}
