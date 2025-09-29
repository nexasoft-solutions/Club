using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberFees;

namespace NexaSoft.Club.Application.Features.MemberFees.Commands.UpdateMemberFee;

/*
public class UpdateMemberFeeCommandHandler(
    IGenericRepository<MemberFee> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateMemberFeeCommandHandler> _logger
) : ICommandHandler<UpdateMemberFeeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateMemberFeeCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de MemberFee con ID {MemberFeeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("MemberFee con ID {MemberFeeId} no encontrado", command.Id);
            return Result.Failure<bool>(MemberFeeErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.MemberId,
            command.ConfigId,
            command.Period,
            command.Amount,
            command.DueDate,
            command.Status,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("MemberFee con ID {MemberFeeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar MemberFee con ID {MemberFeeId}", command.Id);
            return Result.Failure<bool>(MemberFeeErrores.ErrorEdit);
        }
    }
}
*/