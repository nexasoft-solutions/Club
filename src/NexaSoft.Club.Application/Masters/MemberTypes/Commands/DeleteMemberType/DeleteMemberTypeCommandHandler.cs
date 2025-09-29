using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.MemberTypes;

namespace NexaSoft.Club.Application.Masters.MemberTypes.Commands.DeleteMemberType;

public class DeleteMemberTypeCommandHandler(
    IGenericRepository<MemberType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteMemberTypeCommandHandler> _logger
) : ICommandHandler<DeleteMemberTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteMemberTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de MemberType con ID {MemberTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("MemberType con ID {MemberTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(MemberTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar MemberType con ID {MemberTypeId}", command.Id);
            return Result.Failure<bool>(MemberTypeErrores.ErrorDelete);
        }
    }
}
