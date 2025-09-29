using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Spaces;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Masters.Spaces.Commands.CreateSpace;

public class CreateSpaceCommandHandler(
    IGenericRepository<Space> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateSpaceCommandHandler> _logger
) : ICommandHandler<CreateSpaceCommand, long>
{
    public async Task<Result<long>> Handle(CreateSpaceCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Space");

     bool existsSpaceName = await _repository.ExistsAsync(c => c.SpaceName == command.SpaceName, cancellationToken);
     if (existsSpaceName)
     {
       return Result.Failure<long>(SpaceErrores.Duplicado);
     }

        var entity = Space.Create(
            command.SpaceName,
            command.SpaceType,
            command.Capacity,
            command.Description,
            command.StandardRate,
            command.IsActive,
            command.RequiresApproval,
            command.MaxReservationHours,
            command.IncomeAccountId,
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
            _logger.LogInformation("Space con ID {SpaceId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Space");
            return Result.Failure<long>(SpaceErrores.ErrorSave);
        }
    }
}
