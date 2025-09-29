using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.MemberTypes;

namespace NexaSoft.Club.Application.Masters.MemberTypes.Commands.UpdateMemberType;

public class UpdateMemberTypeCommandHandler(
    IGenericRepository<MemberType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateMemberTypeCommandHandler> _logger
) : ICommandHandler<UpdateMemberTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateMemberTypeCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de MemberType con ID {MemberTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("MemberType con ID {MemberTypeId} no encontrado", command.Id);
            return Result.Failure<bool>(MemberTypeErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.TypeName,
            command.Description,           
            command.HasFamilyDiscount,
            command.FamilyDiscountRate,
            command.MaxFamilyMembers,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("MemberType con ID {MemberTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar MemberType con ID {MemberTypeId}", command.Id);
            return Result.Failure<bool>(MemberTypeErrores.ErrorEdit);
        }
    }
}
