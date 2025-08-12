using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.CreatePlano;

public class CreatePlanoCommandHandler(
    IGenericRepository<Plano> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePlanoCommandHandler> _logger
) : ICommandHandler<CreatePlanoCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePlanoCommand command, CancellationToken cancellationToken)
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


        var entity = Plano.Create(
            command.EscalaId,
            command.SistemaProyeccion,
            command.NombrePlano,
            command.CodigoPlano,
            command.ArchivoId,
            command.ColaboradorId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime()
        );

        // Geometry factory con SRID 4326 (WGS84)
        //var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        foreach (var detalleItem in command.Detalles)
        {
            /*Geometry geometry;

            if (detalleItem.Coordenadas.Count == 1)
            {
                // Punto
                var coord = detalleItem.Coordenadas[0];
                geometry = geometryFactory.CreatePoint(new Coordinate(coord[0], coord[1]));
            }
            else
            {
                // Polígono
                var coordinates = detalleItem.Coordenadas
                    .Select(c => new Coordinate(c[0], c[1]))
                    .ToList();

                // Asegurar que el polígono esté cerrado
                if (!coordinates.First().Equals2D(coordinates.Last()))
                {
                    coordinates.Add(coordinates[0]);
                }

                var linearRing = geometryFactory.CreateLinearRing(coordinates.ToArray());
                geometry = geometryFactory.CreatePolygon(linearRing);
            }*/

            var detalle = PlanoDetalle.Create(
                planoId: entity.Id,
                descripcion: detalleItem.Descripcion,
                coordenadas: detalleItem.Coordenadas,
                fechaCreacion: _dateTimeProvider.CurrentTime.ToUniversalTime()
            );

            entity.AgregarDetalle(detalle);
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
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
            return Result.Failure<Guid>(PlanoErrores.ErrorSave);
        }
    }
}
