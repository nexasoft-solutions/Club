using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Commands.CreateAttendanceStatusType;

public class CreateAttendanceStatusTypeCommandHandler(
    IGenericRepository<AttendanceStatusType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateAttendanceStatusTypeCommandHandler> _logger
) : ICommandHandler<CreateAttendanceStatusTypeCommand, long>
{
    public async Task<Result<long>> Handle(CreateAttendanceStatusTypeCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de AttendanceStatusType");

     bool existsCode = await _repository.ExistsAsync(c => c.Code == command.Code, cancellationToken);
     if (existsCode)
     {
       return Result.Failure<long>(AttendanceStatusTypeErrores.Duplicado);
     }

     bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
     if (existsName)
     {
       return Result.Failure<long>(AttendanceStatusTypeErrores.Duplicado);
     }

        var entity = AttendanceStatusType.Create(
            command.Code,
            command.Name,
            command.IsPaid,
            command.Description,
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
            _logger.LogInformation("AttendanceStatusType con ID {AttendanceStatusTypeId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear AttendanceStatusType");
            return Result.Failure<long>(AttendanceStatusTypeErrores.ErrorSave);
        }
    }
}
