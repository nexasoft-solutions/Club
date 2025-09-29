using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberFees;

namespace NexaSoft.Club.Application.Features.MemberFees.Commands.DeleteMemberFee;

public class DeleteMemberFeeCommandHandler(
    IGenericRepository<MemberFee> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteMemberFeeCommandHandler> _logger
) : ICommandHandler<DeleteMemberFeeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteMemberFeeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de MemberFee con ID {MemberFeeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("MemberFee con ID {MemberFeeId} no encontrado", command.Id);
                return Result.Failure<bool>(MemberFeeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar MemberFee con ID {MemberFeeId}", command.Id);
            return Result.Failure<bool>(MemberFeeErrores.ErrorDelete);
        }
    }
}
