using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Ubigeos;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Commands.CreateUbigeo;

public class CreateUbigeoCommandHandler(
    IGenericRepository<Ubigeo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateUbigeoCommandHandler> _logger
) : ICommandHandler<CreateUbigeoCommand, long>
{
    public async Task<Result<long>> Handle(CreateUbigeoCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Ubigeo");
       


        bool existsDescription = await _repository.ExistsAsync(c => c.Level == command.Level && c.Description == command.Description, cancellationToken);
        if (existsDescription)
        {
            return Result.Failure<long>(UbigeoErrores.Duplicado);
        }

        var parent = command.Level == 1 ? (long?)null : command.ParentId;

        var entity = Ubigeo.Create(
            command.Description,
            command.Level,
            parent,
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
            _logger.LogInformation("Ubigeo con ID {UbigeoId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Ubigeo");
            return Result.Failure<long>(UbigeoErrores.ErrorSave);
        }
    }
}
