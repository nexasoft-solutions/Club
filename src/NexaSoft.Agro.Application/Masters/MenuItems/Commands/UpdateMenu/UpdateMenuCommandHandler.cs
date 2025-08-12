using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Menus;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Commands.UpdateMenu;

public class UpdateMenuCommandHandler(
    IGenericRepository<MenuItemOption> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateMenuCommandHandler> _logger
) : ICommandHandler<UpdateMenuCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateMenuCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Menu con ID {MenuId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Menu con ID {MenuId} no encontrado", command.Id);
            return Result.Failure<bool>(MenuItemErrores.NoEncontrado);
        }

        entity.Update(
            command.Label,
            command.Icon,
            command.Route,
            _dateTimeProvider.CurrentTime.ToUniversalTime()
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Menu con ID {ManuId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            var errores = ex.Message;
            _logger.LogError(ex, "Error al actualizar Menú con ID {MenuId}", command.Id);
            return Result.Failure<bool>(MenuItemErrores.ErrorEdit);
        }
    }
}
