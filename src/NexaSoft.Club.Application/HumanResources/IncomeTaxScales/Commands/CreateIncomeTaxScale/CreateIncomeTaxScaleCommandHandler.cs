using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Commands.CreateIncomeTaxScale;

public class CreateIncomeTaxScaleCommandHandler(
    IGenericRepository<IncomeTaxScale> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateIncomeTaxScaleCommandHandler> _logger
) : ICommandHandler<CreateIncomeTaxScaleCommand, long>
{
    public async Task<Result<long>> Handle(CreateIncomeTaxScaleCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de IncomeTaxScale");

     bool existsScaleYear = await _repository.ExistsAsync(c => c.ScaleYear == command.ScaleYear, cancellationToken);
     if (existsScaleYear)
     {
       return Result.Failure<long>(IncomeTaxScaleErrores.Duplicado);
     }

     bool existsMinIncome = await _repository.ExistsAsync(c => c.MinIncome == command.MinIncome, cancellationToken);
     if (existsMinIncome)
     {
       return Result.Failure<long>(IncomeTaxScaleErrores.Duplicado);
     }

     bool existsMaxIncome = await _repository.ExistsAsync(c => c.MaxIncome == command.MaxIncome, cancellationToken);
     if (existsMaxIncome)
     {
       return Result.Failure<long>(IncomeTaxScaleErrores.Duplicado);
     }

     bool existsFixedAmount = await _repository.ExistsAsync(c => c.FixedAmount == command.FixedAmount, cancellationToken);
     if (existsFixedAmount)
     {
       return Result.Failure<long>(IncomeTaxScaleErrores.Duplicado);
     }

     bool existsRate = await _repository.ExistsAsync(c => c.Rate == command.Rate, cancellationToken);
     if (existsRate)
     {
       return Result.Failure<long>(IncomeTaxScaleErrores.Duplicado);
     }

     bool existsExcessOver = await _repository.ExistsAsync(c => c.ExcessOver == command.ExcessOver, cancellationToken);
     if (existsExcessOver)
     {
       return Result.Failure<long>(IncomeTaxScaleErrores.Duplicado);
     }

        var entity = IncomeTaxScale.Create(
            command.ScaleYear,
            command.MinIncome,
            command.MaxIncome,
            command.FixedAmount,
            command.Rate,
            command.ExcessOver,
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
            _logger.LogInformation("IncomeTaxScale con ID {IncomeTaxScaleId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear IncomeTaxScale");
            return Result.Failure<long>(IncomeTaxScaleErrores.ErrorSave);
        }
    }
}
