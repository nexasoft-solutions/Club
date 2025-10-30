using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.LegalParameters;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters.Commands.CreateLegalParameter;

public class CreateLegalParameterCommandHandler(
    IGenericRepository<LegalParameter> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateLegalParameterCommandHandler> _logger
) : ICommandHandler<CreateLegalParameterCommand, long>
{
  public async Task<Result<long>> Handle(CreateLegalParameterCommand command, CancellationToken cancellationToken)
  {

    _logger.LogInformation("Iniciando proceso de creación de LegalParameter");

    bool existsCode = await _repository.ExistsAsync(c => c.Code == command.Code, cancellationToken);
    if (existsCode)
    {
      return Result.Failure<long>(LegalParameterErrores.Duplicado);
    }

    bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
    if (existsName)
    {
      return Result.Failure<long>(LegalParameterErrores.Duplicado);
    }

    bool existsValue = await _repository.ExistsAsync(c => c.Value == command.Value, cancellationToken);
    if (existsValue)
    {
      return Result.Failure<long>(LegalParameterErrores.Duplicado);
    }

    

    var entity = LegalParameter.Create(
        command.Code,
        command.Name,
        command.Value,
        command.ValueText,
        command.EffectiveDate,
        command.EndDate,
        command.Category,
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
      _logger.LogInformation("LegalParameter con ID {LegalParameterId} creado satisfactoriamente", entity.Id);

      return Result.Success(entity.Id);
    }
    catch (Exception ex)
    {
      await _unitOfWork.RollbackAsync(cancellationToken);
      _logger.LogError(ex, "Error al crear LegalParameter");
      return Result.Failure<long>(LegalParameterErrores.ErrorSave);
    }
  }
}
