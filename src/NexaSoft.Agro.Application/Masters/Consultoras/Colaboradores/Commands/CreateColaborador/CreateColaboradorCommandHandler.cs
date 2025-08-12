using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Commands.CreateColaborador;

public class CreateColaboradorCommandHandler(
    IGenericRepository<Colaborador> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateColaboradorCommandHandler> _logger
) : ICommandHandler<CreateColaboradorCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateColaboradorCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Colaborador");
        var validator = new CreateColaboradorCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }


        bool existsNombresColaborador = await _repository.ExistsAsync(c => c.NombresColaborador == command.NombresColaborador, cancellationToken);
        if (existsNombresColaborador)
        {
            return Result.Failure<Guid>(ColaboradorErrores.Duplicado);
        }

        bool existsApellidosColaborador = await _repository.ExistsAsync(c => c.ApellidosColaborador == command.ApellidosColaborador, cancellationToken);
        if (existsApellidosColaborador)
        {
            return Result.Failure<Guid>(ColaboradorErrores.Duplicado);
        }

        bool existsNumeroDocumentoIdentidad = await _repository.ExistsAsync(c => c.NumeroDocumentoIdentidad == command.NumeroDocumentoIdentidad, cancellationToken);
        if (existsNumeroDocumentoIdentidad)
        {
            return Result.Failure<Guid>(ColaboradorErrores.Duplicado);
        }

        var entity = Colaborador.Create(
            command.NombresColaborador,
            command.ApellidosColaborador,
            command.TipoDocumentoId,
            command.NumeroDocumentoIdentidad,
            command.FechaNacimiento,
            command.GeneroColaboradorId,
            command.EstadoCivilColaboradorId,
            command.Direccion,
            command.CorreoElectronico,
            command.TelefonoMovil,
            command.CargoId,
            command.DepartamentoId,
            command.FechaIngreso,
            command.Salario,
            command.FechaCese,
            command.Comentarios,
            command.ConsultoraId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime()
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Colaborador con ID {ColaboradorId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Colaborador");
            return Result.Failure<Guid>(ColaboradorErrores.ErrorSave);
        }
    }
}
