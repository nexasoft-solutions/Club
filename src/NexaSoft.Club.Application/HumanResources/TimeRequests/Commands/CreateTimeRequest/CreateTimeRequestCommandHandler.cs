using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.TimeRequests;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.TimeRequests.Commands.CreateTimeRequest;

public class CreateTimeRequestCommandHandler(
    IGenericRepository<TimeRequest> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateTimeRequestCommandHandler> _logger
) : ICommandHandler<CreateTimeRequestCommand, long>
{
    public async Task<Result<long>> Handle(CreateTimeRequestCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de TimeRequest");

        var entity = TimeRequest.Create(
            command.EmployeeId,
            command.TimeRequestTypeId,
            command.StartDate,
            command.EndDate,
            command.TotalDays,
            command.Reason,
            command.StatusId,
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
            _logger.LogInformation("TimeRequest con ID {TimeRequestId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear TimeRequest");
            return Result.Failure<long>(TimeRequestErrores.ErrorSave);
        }
    }
}
