using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.DeleteEmpresa;

public class DeleteEmpresaCommandHandler(
    IGenericRepository<Empresa> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteEmpresaCommandHandler> _logger
) : ICommandHandler<DeleteEmpresaCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteEmpresaCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de Empresa con ID {EmpresaId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Empresa con ID {EmpresaId} no encontrado", command.Id);
                return Result.Failure<bool>(EmpresaErrores.NoEncontrado);
            }

         entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime());

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al eliminar Empresa con ID {EmpresaId}", command.Id);
            return Result.Failure<bool>(EmpresaErrores.ErrorDelete);
        }
    }
}
