using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.CreatePayrollConceptsForType;

public class CreatePayrollConceptsForTypeCommandHandler(
    IGenericRepository<PayrollConceptType> _repository,
    IUnitOfWork _unitOfWork,
    ILogger<CreatePayrollConceptsForTypeCommandHandler> _logger,
    IDateTimeProvider _dateTimeProvider
) : ICommandHandler<CreatePayrollConceptsForTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(CreatePayrollConceptsForTypeCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando asignación masiva de conceptos al tipo de planilla {PayrollTypeId}", command.PayrollTypeId);

        try
        {
            // Validar que no existan duplicados
            var existingRelations = await _repository.ListAsync(
                pct => pct.PayrollTypeId == command.PayrollTypeId &&
                       command.PayrollConceptIds.Contains(pct.PayrollConceptId),
                cancellationToken
            );

            if (existingRelations.Any())
            {
                var existingIds = existingRelations.Select(x => x.PayrollConceptId).ToList();
                _logger.LogWarning("Existen relaciones duplicadas para los conceptos: {ConceptIds}", existingIds);
                return Result.Failure<bool>(PayrollConceptTypeErrores.Duplicado);
            }

            // Crear las entidades
            var payrollConceptTypes = command.PayrollConceptIds.Select(conceptId =>
                PayrollConceptType.Create(conceptId, command.PayrollTypeId, _dateTimeProvider.CurrentTime.ToUniversalTime(), command.CreatedBy)
            ).ToList();

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Usar tu método genérico AddRangeAsync
            await _repository.AddRangeAsync(payrollConceptTypes, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation(
                "Se asignaron {Count} conceptos al tipo de planilla {PayrollTypeId} satisfactoriamente",
                payrollConceptTypes.Count,
                command.PayrollTypeId
            );

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al asignar conceptos al tipo de planilla {PayrollTypeId}", command.PayrollTypeId);
            return Result.Failure<bool>(PayrollConceptTypeErrores.ErrorSave);
        }
    }
}
