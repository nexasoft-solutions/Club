using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;
using NexaSoft.Agro.Domain.Masters.Contadores;
using NexaSoft.Agro.Domain.Specifications;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Commands.CreateEstudioAmbiental;

public class CreateEstudioAmbientalCommandHandler(
    IGenericRepository<EstudioAmbiental> _repository,
    IGenericRepository<Contador> _contadorRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateEstudioAmbientalCommandHandler> _logger
) : ICommandHandler<CreateEstudioAmbientalCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateEstudioAmbientalCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de EstudioAmbiental");
        var validator = new CreateEstudioAmbientalCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }


        bool existsProyecto = await _repository.ExistsAsync(c => c.Proyecto == command.Proyecto, cancellationToken);
        if (existsProyecto)
        {
            return Result.Failure<Guid>(EstudioAmbientalErrores.Duplicado);
        }

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var contador = await _contadorRepository.GetEntityWithSpec(new ContadorRawSpec("EstudioAmbiental"), cancellationToken);


        if (contador == null)
        {
            // Si no existe, creamos uno inicial
            var contadorNew = Contador.Create(
                "EstudioAmbiental",
                "EA",
                100,
                string.Empty,
                "string",
                10,
                _dateTimeProvider.CurrentTime.ToUniversalTime()
            );

            await _contadorRepository.AddAsync(contadorNew, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            contador = contadorNew;
        }

        // Incrementar valor actual
        var nuevoCodigo = contador.Incrementar(_dateTimeProvider.CurrentTime.ToUniversalTime());

        // Guardar el cambio del contador
      
        await _contadorRepository.UpdateAsync(contador); 
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var entity = EstudioAmbiental.Create(
            command.Proyecto,
            nuevoCodigo,
            command.FechaInicio,
            command.FechaFin,
            command.Detalles,
            command.EmpresaId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime()
        );

        try
        {
            //await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("EstudioAmbiental con ID {EstudioAmbientalId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear EstudioAmbiental");
            return Result.Failure<Guid>(EstudioAmbientalErrores.ErrorSave);
        }
    }
}
