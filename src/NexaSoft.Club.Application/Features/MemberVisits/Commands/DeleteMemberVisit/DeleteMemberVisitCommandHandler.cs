using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberVisits;

namespace NexaSoft.Club.Application.Features.MemberVisits.Commands.DeleteMemberVisit;

public class DeleteMemberVisitCommandHandler(
    IGenericRepository<MemberVisit> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteMemberVisitCommandHandler> _logger
) : ICommandHandler<DeleteMemberVisitCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteMemberVisitCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de MemberVisit con ID {MemberVisitId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("MemberVisit con ID {MemberVisitId} no encontrado", command.Id);
            return Result.Failure<bool>(MemberVisitErrores.NoEncontrado);
        }

        entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(), command.DeletedBy);

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
            _logger.LogError(ex, "Error al eliminar MemberVisit con ID {MemberVisitId}", command.Id);
            return Result.Failure<bool>(MemberVisitErrores.ErrorDelete);
        }
    }
}
