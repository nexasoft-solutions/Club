using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.TaxRates;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.TaxRates.Commands.CreateTaxRate;

public class CreateTaxRateCommandHandler(
    IGenericRepository<TaxRate> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateTaxRateCommandHandler> _logger
) : ICommandHandler<CreateTaxRateCommand, long>
{
  public async Task<Result<long>> Handle(CreateTaxRateCommand command, CancellationToken cancellationToken)
  {

    _logger.LogInformation("Iniciando proceso de creación de TaxRate");

    bool existsCode = await _repository.ExistsAsync(c => c.Code == command.Code, cancellationToken);
    if (existsCode)
    {
      return Result.Failure<long>(TaxRateErrores.Duplicado);
    }

    bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
    if (existsName)
    {
      return Result.Failure<long>(TaxRateErrores.Duplicado);
    }

    bool existsRateValue = await _repository.ExistsAsync(c => c.RateValue == command.RateValue, cancellationToken);
    if (existsRateValue)
    {
      return Result.Failure<long>(TaxRateErrores.Duplicado);
    }

    bool existsRateType = await _repository.ExistsAsync(c => c.RateType == command.RateType, cancellationToken);
    if (existsRateType)
    {
      return Result.Failure<long>(TaxRateErrores.Duplicado);
    }

    bool existsMinAmount = await _repository.ExistsAsync(c => c.MinAmount == command.MinAmount, cancellationToken);
    if (existsMinAmount)
    {
      return Result.Failure<long>(TaxRateErrores.Duplicado);
    }

    bool existsMaxAmount = await _repository.ExistsAsync(c => c.MaxAmount == command.MaxAmount, cancellationToken);
    if (existsMaxAmount)
    {
      return Result.Failure<long>(TaxRateErrores.Duplicado);
    }

    bool existsEffectiveDate = await _repository.ExistsAsync(c => c.EffectiveDate == command.EffectiveDate, cancellationToken);
    if (existsEffectiveDate)
    {
      return Result.Failure<long>(TaxRateErrores.Duplicado);
    }

    bool existsEndDate = await _repository.ExistsAsync(c => c.EndDate == command.EndDate, cancellationToken);
    if (existsEndDate)
    {
      return Result.Failure<long>(TaxRateErrores.Duplicado);
    }

    var entity = TaxRate.Create(
        command.Code,
        command.Name,
        command.RateValue,
        command.RateType,
        command.MinAmount,
        command.MaxAmount,
        command.EffectiveDate,
        command.EndDate,
        command.Category,
        command.Description,
        command.AppliesTo,
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
      _logger.LogInformation("TaxRate con ID {TaxRateId} creado satisfactoriamente", entity.Id);

      return Result.Success(entity.Id);
    }
    catch (Exception ex)
    {
      await _unitOfWork.RollbackAsync(cancellationToken);
      _logger.LogError(ex, "Error al crear TaxRate");
      return Result.Failure<long>(TaxRateErrores.ErrorSave);
    }
  }
}
