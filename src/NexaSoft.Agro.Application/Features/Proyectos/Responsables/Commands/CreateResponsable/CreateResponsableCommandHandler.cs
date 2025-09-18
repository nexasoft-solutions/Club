using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Features.Proyectos.Responsables.Commands.CreateResponsable;

public class CreateResponsableCommandHandler(
    IGenericRepository<Responsable> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateResponsableCommandHandler> _logger
) : ICommandHandler<CreateResponsableCommand, long>
{
    public async Task<Result<long>> Handle(CreateResponsableCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Responsable");
        var validator = new CreateResponsableCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }


        bool existsNombreResponsable = await _repository.ExistsAsync(c => c.NombreResponsable == command.NombreResponsable, cancellationToken);
        if (existsNombreResponsable)
        {
            return Result.Failure<long>(ResponsableErrores.Duplicado);
        }

        var entity = Responsable.Create(
            command.NombreResponsable,
            command.CargoResponsable,
            command.CorreoResponsable,
            command.TelefonoResponsable,
            command.Observaciones,
            command.EstudioAmbientalId,
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
            _logger.LogInformation("Responsable con ID {ResponsableId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Responsable");
            return Result.Failure<long>(ResponsableErrores.ErrorSave);
        }
    }
}
