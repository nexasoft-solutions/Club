using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.CreatePayrollConcept;

public class CreatePayrollConceptCommandHandler(
    IGenericRepository<PayrollConcept> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePayrollConceptCommandHandler> _logger
) : ICommandHandler<CreatePayrollConceptCommand, long>
{
    public async Task<Result<long>> Handle(CreatePayrollConceptCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de PayrollConcept");

        bool existsCode = await _repository.ExistsAsync(c => c.Code == command.Code, cancellationToken);
        if (existsCode)
        {
            return Result.Failure<long>(PayrollConceptErrores.Duplicado);
        }

        bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
        if (existsName)
        {
            return Result.Failure<long>(PayrollConceptErrores.Duplicado);
        }

        var entity = PayrollConcept.Create(
            command.Code,
            command.Name,
            command.ConceptTypePayrollId,
            command.PayrollFormulaId,
            command.ConceptCalculationTypeId,
            command.FixedValue,
            command.PorcentajeValue,
            command.ConceptApplicationTypesId,
            command.AccountingChartId,
            //command.PayrollTypeId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("PayrollConcept con ID {PayrollConceptId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear PayrollConcept");
            return Result.Failure<long>(PayrollConceptErrores.ErrorSave);
        }
    }
}
