using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.MemberTypes;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Masters.MemberTypes.Commands.CreateMemberType;

public class CreateMemberTypeCommandHandler(
    IGenericRepository<MemberType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateMemberTypeCommandHandler> _logger
) : ICommandHandler<CreateMemberTypeCommand, long>
{
    public async Task<Result<long>> Handle(CreateMemberTypeCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de MemberType");

        bool existsTypeName = await _repository.ExistsAsync(c => c.TypeName == command.TypeName, cancellationToken);
        if (existsTypeName)
        {
            return Result.Failure<long>(MemberTypeErrores.Duplicado);
        }

        var entity = MemberType.Create(
            command.TypeName,
            command.Description,            
            command.HasFamilyDiscount,
            command.FamilyDiscountRate,
            command.MaxFamilyMembers,
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
            _logger.LogInformation("MemberType con ID {MemberTypeId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear MemberType");
            return Result.Failure<long>(MemberTypeErrores.ErrorSave);
        }
    }
}
