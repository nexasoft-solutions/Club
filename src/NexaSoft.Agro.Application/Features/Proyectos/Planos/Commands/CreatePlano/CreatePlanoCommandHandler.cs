using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;
using NexaSoft.Agro.Domain.Masters.Contadores;
using NexaSoft.Agro.Domain.Specifications;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.CreatePlano;

public class CreatePlanoCommandHandler(
    IGenericRepository<Plano> _repository,
    IGenericRepository<Contador> _contadorRepository,
    IGenericRepository<Colaborador> _colaboradorRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePlanoCommandHandler> _logger
) : ICommandHandler<CreatePlanoCommand, long>
{
    public async Task<Result<long>> Handle(CreatePlanoCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Plano");
        var validator = new CreatePlanoCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }

        var colaborador = await _colaboradorRepository.GetByIdAsync(command.ColaboradorId, cancellationToken);

        if (colaborador is null)
        {
            _logger.LogWarning("Colaborador con ID {ColaboradorId} no encontrado", command.ColaboradorId);
            return Result.Failure<long>(ColaboradorErrores.NoEncontrado);
        }


        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var contador = await _contadorRepository.GetEntityWithSpec(new ContadorRawSpec("Plano", colaborador.UserName!.ToUpper()), cancellationToken);

        if (contador == null)
        {
            // Si no existe, creamos uno inicial
            var contadorNew = Contador.Create(
                "Plano",
                colaborador.UserName!.ToUpper(),
                0,
                string.Empty,
                "string",
                6,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                command.UsuarioCreacion
            );

            await _contadorRepository.AddAsync(contadorNew, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            contador = contadorNew;
        }

        // Incrementar valor actual
        var nuevoCodigo = contador.Incrementar(_dateTimeProvider.CurrentTime.ToUniversalTime(), command.UsuarioCreacion);

        // Guardar el cambio del contador

        await _contadorRepository.UpdateAsync(contador);
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        var entity = Plano.Create(
            command.EscalaId,
            command.SistemaProyeccion,
            command.NombrePlano,
            nuevoCodigo,
            command.ArchivoId,
            command.ColaboradorId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioCreacion!
        );

        // Geometry factory con SRID 4326 (WGS84)
        //var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        foreach (var detalleItem in command.Detalles)
        {

            var detalle = PlanoDetalle.Create(
                planoId: entity.Id,
                descripcion: detalleItem.Descripcion,
                coordenadas: detalleItem.Coordenadas,
                fechaCreacion: _dateTimeProvider.CurrentTime.ToUniversalTime(),
                command.UsuarioCreacion
            );

            entity.AgregarDetalle(detalle);
        }

        try
        {
            //await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Plano con ID {PlanoId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Plano");
            return Result.Failure<long>(PlanoErrores.ErrorSave);
        }
    }
}
