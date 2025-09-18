using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Commands.UpdateColaborador;

public class UpdateColaboradorCommandHandler(
    IGenericRepository<Colaborador> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateColaboradorCommandHandler> _logger
) : ICommandHandler<UpdateColaboradorCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateColaboradorCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Colaborador con ID {ColaboradorId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Colaborador con ID {ColaboradorId} no encontrado", command.Id);
            return Result.Failure<bool>(ColaboradorErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
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
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioModificacion
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Colaborador con ID {ColaboradorId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar Colaborador con ID {ColaboradorId}", command.Id);
            return Result.Failure<bool>(ColaboradorErrores.ErrorEdit);
        }
    }
}
