using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberFees;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.MemberFees.Commands.CreateMemberFee;
/*
public class CreateMemberFeeCommandHandler(
    IGenericRepository<MemberFee> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateMemberFeeCommandHandler> _logger
) : ICommandHandler<CreateMemberFeeCommand, long>
{
    public async Task<Result<long>> Handle(CreateMemberFeeCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de MemberFee");

     bool existsPeriod = await _repository.ExistsAsync(c => c.Period == command.Period, cancellationToken);
     if (existsPeriod)
     {
       return Result.Failure<long>(MemberFeeErrores.Duplicado);
     }

        var entity = MemberFee.Create(
            command.MemberId,
            command.ConfigId,
            command.Period,
            command.Amount,
            command.DueDate,
            command.Status,
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
            _logger.LogInformation("MemberFee con ID {MemberFeeId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear MemberFee");
            return Result.Failure<long>(MemberFeeErrores.ErrorSave);
        }
    }
}
*/