using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;
using NexaSoft.Agro.Domain.Masters.Constantes.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.CreateEmpresa;

public class CreateEmpresaCommandHandler(
    IGenericRepository<Empresa> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateEmpresaCommandHandler> _logger
) : ICommandHandler<CreateEmpresaCommand, long>
{
  public async Task<Result<long>> Handle(CreateEmpresaCommand command, CancellationToken cancellationToken)
  {

    _logger.LogInformation("Iniciando proceso de creación de Empresa");
    var validator = new CreateEmpresaCommandValidator();
    var validationResult = validator.Validate(command);
    if (!validationResult.IsValid)
    {
      var errors = validationResult.Errors
         .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

      throw new ValidationExceptions(errors);
    }


    bool existsRazonSocial = await _repository.ExistsAsync(c => c.RazonSocial == command.RazonSocial, cancellationToken);
    if (existsRazonSocial)
    {
      return Result.Failure<long>(EmpresaErrores.Duplicado);
    }

    bool existsRucEmpresa = await _repository.ExistsAsync(c => c.RucEmpresa == command.RucEmpresa, cancellationToken);
    if (existsRucEmpresa)
    {
      return Result.Failure<long>(EmpresaErrores.Duplicado);
    }

    bool existsContactoEmpresa = await _repository.ExistsAsync(c => c.ContactoEmpresa == command.ContactoEmpresa, cancellationToken);
    if (existsContactoEmpresa)
    {
      return Result.Failure<long>(EmpresaErrores.Duplicado);
    }

    bool existsTelefonoContactoEmpresa = await _repository.ExistsAsync(c => c.TelefonoContactoEmpresa == command.TelefonoContactoEmpresa, cancellationToken);
    if (existsTelefonoContactoEmpresa)
    {
      return Result.Failure<long>(EmpresaErrores.Duplicado);
    }

    var entity = Empresa.Create(
        command.RazonSocial,
        command.RucEmpresa,
        command.ContactoEmpresa,
        command.TelefonoContactoEmpresa,
        command.DepartamentoEmpresaId,
        command.ProvinciaEmpresaId,
        command.DistritoEmpresaId,
        command.Direccion,
        command.LatitudEmpresa,
        command.LongitudEmpresa,
        command.OrganizacionId,
        (int)EstadosEnum.Activo,
        _dateTimeProvider.CurrentTime.ToUniversalTime(),
        command.UsuarioCreacion
    );

    try
    {
      await _unitOfWork.BeginTransactionAsync(cancellationToken);
      await _repository.AddAsync(entity, cancellationToken);
      await _unitOfWork.SaveChangesAsync(cancellationToken);
      entity.RaiseDomainEvent(new ConstanteCreateDomainEvent(entity.Id));
      await _unitOfWork.CommitAsync(cancellationToken);
      _logger.LogInformation("Empresa con ID {EmpresaId} creado satisfactoriamente", entity.Id);

      return Result.Success(entity.Id);
    }
    catch (Exception ex)
    {
      await _unitOfWork.RollbackAsync(cancellationToken);
      _logger.LogError(ex, "Error al crear Empresa");
      return Result.Failure<long>(EmpresaErrores.ErrorSave);
    }
  }
}
