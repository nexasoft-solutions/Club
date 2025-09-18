using MediatR;
using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.AddResposablesEvento;

public class AddResponsablesEventoCommandHandler(
    IGenericRepository<EventoRegulatorio> _eventoRegulatorioRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<AddResponsablesEventoCommandHandler> _logger
) : ICommandHandler<AddResponsablesEventoCommand, bool>
{

    async Task<Result<bool>> IRequestHandler<AddResponsablesEventoCommand, Result<bool>>.Handle(AddResponsablesEventoCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso para agregar responsables adicionales al evento {EventoRegulatorioId}", command.EventoRegulatorioId);

       
        var validator = new AddResponsablesEventoCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }

        var evento = await _eventoRegulatorioRepository.GetByIdAsync(command.EventoRegulatorioId, cancellationToken);

        if (evento is null)
        {
            _logger.LogWarning("EventoRegulatorio con ID {Id} no encontrado", command.EventoRegulatorioId);
            return Result.Failure<bool>(EventoRegulatorioErrores.NoEncontrado);
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            evento.AddResponsables(command.ResponsablesIds, _dateTimeProvider.CurrentTime, command.UsuarioCreacion);

            await _eventoRegulatorioRepository.UpdateAsync(evento);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Responsables agregados correctamente al evento {EventoRegulatorioId}", evento.Id);

            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al agregar responsables al evento {EventoRegulatorioId}", command.EventoRegulatorioId);
            await _unitOfWork.RollbackAsync(cancellationToken);
            return Result.Failure<bool>(EventoRegulatorioErrores.ErrorAgregarResponsables);
        }
    }
}