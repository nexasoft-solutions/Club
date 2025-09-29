using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.CreateMemberTypeFeesBulk;

public class CreateMemberTypeFeesBulkCommandHandler(
    IGenericRepository<MemberTypeFee> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateMemberTypeFeesBulkCommandHandler> _logger
) : ICommandHandler<CreateMemberTypeFeesBulkCommand, int>
{
    public async Task<Result<int>> Handle(CreateMemberTypeFeesBulkCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando creación masiva de MemberTypeFee para MemberType {MemberTypeId}", command.MemberTypeId);

        var entities = new List<MemberTypeFee>();
        var utcNow = _dateTimeProvider.CurrentTime.ToUniversalTime();

        foreach (var dto in command.Fees)
        {
            // Validar duplicado en BD
            bool exists = await _repository.ExistsAsync(
                c => c.MemberTypeId == command.MemberTypeId
                    && c.FeeConfigurationId == dto.FeeConfigurationId,
                cancellationToken
            );

            if (exists)
            {
                return Result.Failure<int>(MemberTypeFeeErrores.Duplicado);
            }

            var entity = MemberTypeFee.Create(
                command.MemberTypeId,
                dto.FeeConfigurationId,
                dto.Amount,
                utcNow,
                command.CreatedBy
            );

            entities.Add(entity);
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddRangeAsync(entities, cancellationToken); // 👈 insert masivo
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Se crearon {Count} MemberTypeFee para MemberType {MemberTypeId}", entities.Count, command.MemberTypeId);
            return Result.Success(entities.Count);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear MemberTypeFee masivamente");
            return Result.Failure<int>(MemberTypeFeeErrores.ErrorSave);
        }
    }
}
