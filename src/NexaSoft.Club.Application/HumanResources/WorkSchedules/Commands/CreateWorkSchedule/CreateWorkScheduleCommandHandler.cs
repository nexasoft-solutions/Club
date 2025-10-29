using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.WorkSchedules;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.WorkSchedules.Commands.CreateWorkSchedule;

public class CreateWorkScheduleCommandHandler(
    IGenericRepository<WorkSchedule> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateWorkScheduleCommandHandler> _logger
) : ICommandHandler<CreateWorkScheduleCommand, long>
{
    public async Task<Result<long>> Handle(CreateWorkScheduleCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de WorkSchedule");

        var entity = WorkSchedule.Create(
            command.EmployeeId,
            command.DayOfWeek,
            command.StartTime,
            command.EndTime,
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
            _logger.LogInformation("WorkSchedule con ID {WorkScheduleId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear WorkSchedule");
            return Result.Failure<long>(WorkScheduleErrores.ErrorSave);
        }
    }
}
