using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.MemberStatuses;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Masters.MemberStatuses.Commands.CreateMemberStatus;

public class CreateMemberStatusCommandHandler(
    IGenericRepository<MemberStatus> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateMemberStatusCommandHandler> _logger
) : ICommandHandler<CreateMemberStatusCommand, long>
{
    public async Task<Result<long>> Handle(CreateMemberStatusCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de MemberStatus");

     bool existsStatusName = await _repository.ExistsAsync(c => c.StatusName == command.StatusName, cancellationToken);
     if (existsStatusName)
     {
       return Result.Failure<long>(MemberStatusErrores.Duplicado);
     }

        var entity = MemberStatus.Create(
            command.StatusName,
            command.Description,
            command.CanAccess,
            command.CanReserve,
            command.CanParticipate,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("MemberStatus con ID {MemberStatusId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear MemberStatus");
            return Result.Failure<long>(MemberStatusErrores.ErrorSave);
        }
    }
}
