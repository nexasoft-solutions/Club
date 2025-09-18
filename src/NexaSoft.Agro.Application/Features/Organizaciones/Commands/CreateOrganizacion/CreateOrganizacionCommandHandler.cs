using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Organizaciones;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Commands.CreateOrganizacion;

public class CreateOrganizacionCommandHandler(
    IGenericRepository<Organizacion> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateOrganizacionCommandHandler> _logger
) : ICommandHandler<CreateOrganizacionCommand, long>
{
    public async Task<Result<long>> Handle(CreateOrganizacionCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Organizacion");
        var validator = new CreateOrganizacionCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }


        bool existsNombreOrganizacion = await _repository.ExistsAsync(c => c.NombreOrganizacion == command.NombreOrganizacion, cancellationToken);
        if (existsNombreOrganizacion)
        {
            return Result.Failure<long>(OrganizacionErrores.Duplicado);
        }

        var entity = Organizacion.Create(
            command.NombreOrganizacion,
            command.ContactoOrganizacion,
            command.TelefonoContacto,
            command.SectorId,
            command.RucOrganizacion,
            command.Observaciones,
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
            _logger.LogInformation("Organizacion con ID {OrganizacionId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Organizacion");
            return Result.Failure<long>(OrganizacionErrores.ErrorSave);
        }
    }
}
