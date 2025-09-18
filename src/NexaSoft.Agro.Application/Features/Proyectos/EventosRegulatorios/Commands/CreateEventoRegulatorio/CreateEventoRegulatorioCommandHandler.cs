using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.CreateEventoRegulatorio;

public class CreateEventoRegulatorioCommandHandler(
    IGenericRepository<EventoRegulatorio> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateEventoRegulatorioCommandHandler> _logger
) : ICommandHandler<CreateEventoRegulatorioCommand, long>
{
    public async Task<Result<long>> Handle(CreateEventoRegulatorioCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de EventoRegulatorio");
        var validator = new CreateEventoRegulatorioCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var entity = EventoRegulatorio.Create(
            command.NombreEvento,
            command.TipoEventoId,
            command.FrecuenciaEventoId,
            command.FechaExpedición,
            command.FechaVencimiento,
            command.Descripcion,
            command.NotificarDíasAntes,
            command.ResponsableId,
            (int)EstadosEventosEnum.Programado,
            command.EstudioAmbientalId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioCreacion,
            command.ResponsablesAdicionales
        );

        try
        {
            
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            entity.RaiseDomainEvent(new EventoRegulatorioCreadoDomainEvent(entity.Id));
            entity.RaiseDomainEvent(new EventoRegulatorioCreateDomainEvent(entity.Id));


            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("EventoRegulatorio con ID {EventoRegulatorioId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear EventoRegulatorio");
            return Result.Failure<long>(EventoRegulatorioErrores.ErrorSave);
        }
    }
}
