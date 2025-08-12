using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Ubigeos;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Masters.Ubigeos.Commands.CreateUbigeo;

public class CreateUbigeoCommandHandler(
    IGenericRepository<Ubigeo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateUbigeoCommandHandler> _logger
) : ICommandHandler<CreateUbigeoCommand, Result<Guid>>
{
    public async Task<Result<Result<Guid>>> Handle(CreateUbigeoCommand command, CancellationToken cancellationToken)
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
            return Result.Failure<Guid>(UbigeoErrores.Duplicado);
        }

        var padre = command.Nivel == 1 ? (Guid?)null : command.PadreId;

        var entity = Ubigeo.Create(
            command.Descripcion,
            command.Nivel,
            padre,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime()
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
            return Result.Failure<Guid>(UbigeoErrores.ErrorSave);
        }
    }
}
