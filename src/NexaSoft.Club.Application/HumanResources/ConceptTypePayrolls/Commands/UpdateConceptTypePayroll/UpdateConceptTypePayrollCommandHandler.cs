using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls;

namespace NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Commands.UpdateConceptTypePayroll;

public class UpdateConceptTypePayrollCommandHandler(
    IGenericRepository<ConceptTypePayroll> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateConceptTypePayrollCommandHandler> _logger
) : ICommandHandler<UpdateConceptTypePayrollCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateConceptTypePayrollCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de ConceptTypePayroll con ID {ConceptTypePayrollId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("ConceptTypePayroll con ID {ConceptTypePayrollId} no encontrado", command.Id);
                return Result.Failure<bool>(ConceptTypePayrollErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
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
            _logger.LogInformation("ConceptTypePayroll con ID {ConceptTypePayrollId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar ConceptTypePayroll con ID {ConceptTypePayrollId}", command.Id);
            return Result.Failure<bool>(ConceptTypePayrollErrores.ErrorEdit);
        }
    }
}
