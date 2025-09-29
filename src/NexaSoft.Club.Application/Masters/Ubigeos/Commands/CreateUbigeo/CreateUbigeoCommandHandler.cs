using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
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
        var validator = new CreateUbigeoCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }


        bool existsDescripcin = await _repository.ExistsAsync(c => c.Nivel == command.Nivel && c.Descripcion == command.Descripcion, cancellationToken);
        if (existsDescripcin)
        {
            return Result.Failure<long>(UbigeoErrores.Duplicado);
        }

        var padre = command.Nivel == 1 ? (long?)null : command.PadreId;

        var entity = Ubigeo.Create(
            command.Descripcion,
            command.Nivel,
            padre,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioCreacion
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
