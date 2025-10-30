using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;

namespace NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Commands.UpdateIncomeTaxScale;

public class UpdateIncomeTaxScaleCommandHandler(
    IGenericRepository<IncomeTaxScale> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateIncomeTaxScaleCommandHandler> _logger
) : ICommandHandler<UpdateIncomeTaxScaleCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateIncomeTaxScaleCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de IncomeTaxScale con ID {IncomeTaxScaleId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("IncomeTaxScale con ID {IncomeTaxScaleId} no encontrado", command.Id);
                return Result.Failure<bool>(IncomeTaxScaleErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.ScaleYear,
            command.MinIncome,
            command.MaxIncome,
            command.FixedAmount,
            command.Rate,
            command.ExcessOver,
            command.Description,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("IncomeTaxScale con ID {IncomeTaxScaleId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar IncomeTaxScale con ID {IncomeTaxScaleId}", command.Id);
            return Result.Failure<bool>(IncomeTaxScaleErrores.ErrorEdit);
        }
    }
}
