using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Masters.Contadores;
using NexaSoft.Agro.Domain.Specifications;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Masters.Constantes.Commands.CreateConstante;

public class CreateConstanteCommandHandler(
    IGenericRepository<Constante> _repository,
    IGenericRepository<Contador> _contadorRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateConstanteCommandHandler> _logger
) : ICommandHandler<CreateConstanteCommand, long>
{
    public async Task<Result<long>> Handle(CreateConstanteCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Constante");
        var validator = new CreateConstanteCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }


        bool existsValor = await _repository.ExistsAsync(c => c.TipoConstante == command.TipoConstante && c.Valor == command.Valor, cancellationToken);
        if (existsValor)
        {
            return Result.Failure<long>(ConstanteErrores.Duplicado);
        }

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var contador = await _contadorRepository.GetEntityWithSpec(new ContadorRawSpec(command.TipoConstante!), cancellationToken);


        if (contador == null)
        {
            // Si no existe, creamos uno inicial
            var contadorNew = Contador.Create(
                command.TipoConstante!,
                string.Empty,
                0,
                string.Empty,
                "int",
                0,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                command.UsuarioCreacion
            );

            await _contadorRepository.AddAsync(contadorNew, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            contador = contadorNew;
        }

        // Incrementar valor actual
        var nuevoCodigo = contador.Incrementar(_dateTimeProvider.CurrentTime.ToUniversalTime(),command.UsuarioCreacion!);

        // Guardar el cambio del contador

        await _contadorRepository.UpdateAsync(contador);
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        var entity = Constante.Create(
            command.TipoConstante,
            int.Parse(nuevoCodigo),
            command.Valor,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioCreacion
        );

        try
        {
            //await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Rol con ID {ConstanteId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Constante");
            return Result.Failure<long>(ConstanteErrores.ErrorSave);
        }
    }
}
