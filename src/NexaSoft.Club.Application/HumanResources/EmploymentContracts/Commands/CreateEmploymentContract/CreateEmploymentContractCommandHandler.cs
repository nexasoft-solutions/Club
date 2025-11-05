using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.EmploymentContracts;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.EmploymentContracts.Commands.CreateEmploymentContract;

public class CreateEmploymentContractCommandHandler(
    IGenericRepository<EmploymentContract> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateEmploymentContractCommandHandler> _logger
) : ICommandHandler<CreateEmploymentContractCommand, long>
{
    public async Task<Result<long>> Handle(CreateEmploymentContractCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de EmploymentContract");

        bool existsDocumentPath = await _repository.ExistsAsync(c => c.DocumentPath == command.DocumentPath, cancellationToken);
        if (existsDocumentPath)
        {
            return Result.Failure<long>(EmploymentContractErrores.Duplicado);
        }

        var entity = EmploymentContract.Create(
            command.EmployeeId,
            command.ContractTypeId,
            command.StartDate,
            command.EndDate,
            command.Salary,
            command.WorkingHours,
            command.DocumentPath,
            command.IsActive,
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
            _logger.LogInformation("EmploymentContract con ID {EmploymentContractId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear EmploymentContract");
            return Result.Failure<long>(EmploymentContractErrores.ErrorSave);
        }
    }
}
