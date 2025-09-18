using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Consultoras;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Commands.CreateConsultora;

public class CreateConsultoraCommandHandler(
    IGenericRepository<Consultora> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateConsultoraCommandHandler> _logger
) : ICommandHandler<CreateConsultoraCommand, long>
{
    public async Task<Result<long>> Handle(CreateConsultoraCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Consultora");
        var validator = new CreateConsultoraCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }


        bool existsNombreConsultora = await _repository.ExistsAsync(c => c.NombreConsultora == command.NombreConsultora, cancellationToken);
        if (existsNombreConsultora)
        {
            return Result.Failure<long>(ConsultoraErrores.Duplicado);
        }

        bool existsRucConsultora = await _repository.ExistsAsync(c => c.RucConsultora == command.RucConsultora, cancellationToken);
        if (existsRucConsultora)
        {
            return Result.Failure<long>(ConsultoraErrores.Duplicado);
        }

        var entity = Consultora.Create(
            command.NombreConsultora,
            command.DireccionConsultora,
            command.RepresentanteConsultora,
            command.RucConsultora,
            command.CorreoOrganizacional,
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
            _logger.LogInformation("Consultora con ID {ConsultoraId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Consultora");
            return Result.Failure<long>(ConsultoraErrores.ErrorSave);
        }
    }
}
