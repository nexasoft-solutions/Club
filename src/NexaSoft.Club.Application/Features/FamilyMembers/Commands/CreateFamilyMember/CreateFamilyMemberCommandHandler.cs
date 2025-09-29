using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.FamilyMembers;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.FamilyMembers.Commands.CreateFamilyMember;

public class CreateFamilyMemberCommandHandler(
    IGenericRepository<FamilyMember> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateFamilyMemberCommandHandler> _logger
) : ICommandHandler<CreateFamilyMemberCommand, long>
{
    public async Task<Result<long>> Handle(CreateFamilyMemberCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de FamilyMember");

     bool existsDni = await _repository.ExistsAsync(c => c.Dni == command.Dni, cancellationToken);
     if (existsDni)
     {
       return Result.Failure<long>(FamilyMemberErrores.Duplicado);
     }

        var entity = FamilyMember.Create(
            command.MemberId,
            command.Dni,
            command.FirstName,
            command.LastName,
            command.Relationship,
            command.BirthDate,
            command.IsAuthorized,
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
            _logger.LogInformation("FamilyMember con ID {FamilyMemberId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear FamilyMember");
            return Result.Failure<long>(FamilyMemberErrores.ErrorSave);
        }
    }
}
