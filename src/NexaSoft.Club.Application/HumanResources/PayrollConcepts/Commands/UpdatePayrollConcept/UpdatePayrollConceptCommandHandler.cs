using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.UpdatePayrollConcept;

public class UpdatePayrollConceptCommandHandler(
    IGenericRepository<PayrollConcept> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePayrollConceptCommandHandler> _logger
) : ICommandHandler<UpdatePayrollConceptCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePayrollConceptCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PayrollConcept con ID {PayrollConceptId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollConcept con ID {PayrollConceptId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollConceptErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.ConceptTypePayrollId,
            command.PayrollFormulaId,
            command.ConceptCalculationTypeId,
            command.FixedValue,
            command.PorcentajeValue,
            command.ConceptApplicationTypesId,
            command.AccountingChartId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("PayrollConcept con ID {PayrollConceptId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PayrollConcept con ID {PayrollConceptId}", command.Id);
            return Result.Failure<bool>(PayrollConceptErrores.ErrorEdit);
        }
    }
}
