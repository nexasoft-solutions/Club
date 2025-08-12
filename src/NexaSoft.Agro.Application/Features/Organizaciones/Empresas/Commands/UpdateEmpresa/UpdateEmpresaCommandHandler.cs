using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.UpdateEmpresa;

public class UpdateEmpresaCommandHandler(
    IGenericRepository<Empresa> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateEmpresaCommandHandler> _logger
) : ICommandHandler<UpdateEmpresaCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateEmpresaCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de Empresa con ID {EmpresaId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Empresa con ID {EmpresaId} no encontrado", command.Id);
                return Result.Failure<bool>(EmpresaErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
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
            _dateTimeProvider.CurrentTime.ToUniversalTime()
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Empresa con ID {EmpresaId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar Empresa con ID {EmpresaId}", command.Id);
            return Result.Failure<bool>(EmpresaErrores.ErrorEdit);
        }
    }
}
