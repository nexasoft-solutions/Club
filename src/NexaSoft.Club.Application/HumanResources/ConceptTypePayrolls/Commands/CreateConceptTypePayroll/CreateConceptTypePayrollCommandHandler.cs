using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Commands.CreateConceptTypePayroll;

public class CreateConceptTypePayrollCommandHandler(
    IGenericRepository<ConceptTypePayroll> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateConceptTypePayrollCommandHandler> _logger
) : ICommandHandler<CreateConceptTypePayrollCommand, long>
{
    public async Task<Result<long>> Handle(CreateConceptTypePayrollCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de ConceptTypePayroll");

     bool existsCode = await _repository.ExistsAsync(c => c.Code == command.Code, cancellationToken);
     if (existsCode)
     {
       return Result.Failure<long>(ConceptTypePayrollErrores.Duplicado);
     }

     bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
     if (existsName)
     {
       return Result.Failure<long>(ConceptTypePayrollErrores.Duplicado);
     }

        var entity = ConceptTypePayroll.Create(
            command.Code,
            command.Name,
            command.Description,
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
            _logger.LogInformation("ConceptTypePayroll con ID {ConceptTypePayrollId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear ConceptTypePayroll");
            return Result.Failure<long>(ConceptTypePayrollErrores.ErrorSave);
        }
    }
}
