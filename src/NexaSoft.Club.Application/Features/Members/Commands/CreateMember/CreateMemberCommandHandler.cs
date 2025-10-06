using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Members.Background;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Features.Members.Events;
using NexaSoft.Club.Domain.Masters.MemberTypes;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.Members.Commands.CreateMember;

public class CreateMemberCommandHandler(
    IGenericRepository<Member> _repository,
    IGenericRepository<MemberType> _memberTypeRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateMemberCommandHandler> _logger,
    IMemberBackgroundTaskService _backgroundTaskService
) : ICommandHandler<CreateMemberCommand, long>
{
  public async Task<Result<long>> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
  {

    _logger.LogInformation("Iniciando proceso de creación de Member");

    bool existsDni = await _repository.ExistsAsync(c => c.Dni == command.Dni, cancellationToken);
    if (existsDni)
    {
      return Result.Failure<long>(MemberErrores.Duplicado);
    }

    bool existsEmail = await _repository.ExistsAsync(c => c.Email == command.Email, cancellationToken);
    if (existsEmail)
    {
      return Result.Failure<long>(MemberErrores.Duplicado);
    }

    /*bool existsQrCode = await _repository.ExistsAsync(c => c.QrCode == command.QrCode, cancellationToken);
    if (existsQrCode)
    {
      return Result.Failure<long>(MemberErrores.Duplicado);
    }*/

    var memberType = await _memberTypeRepository.GetByIdAsync(command.MemberTypeId, cancellationToken);
    if (memberType is null) return Result.Failure<long>(MemberErrores.TipoNoExiste);

    await _unitOfWork.BeginTransactionAsync(cancellationToken);

    Member? entity = null;

    try
    {

      entity = Member.Create(
        command.Dni,
        command.FirstName,
        command.LastName,
        command.Email,
        command.Phone,
        command.Address,
        command.BirthDate,
        command.MemberTypeId,
        command.StatusId,
        command.JoinDate,
        command.ExpirationDate,
        command.Balance,
        //command.QrCode,
        //command.QrExpiration,
        command.ProfilePictureUrl,
        (int)EstadosEnum.Activo,
        false,
        null,
        _dateTimeProvider.CurrentTime.ToUniversalTime(),
        command.CreatedBy
    );

      // Marcar como processing
      entity.MarkAsProcessing();


      //await _unitOfWork.BeginTransactionAsync(cancellationToken);
      await _repository.AddAsync(entity, cancellationToken);
      await _unitOfWork.SaveChangesAsync(cancellationToken);

      _logger.LogInformation("Member con ID {MemberId} guardado, generando cuotas...", entity.Id);

      // 3. ENCOLAR GENERACIÓN DE CUOTAS EN BACKGROUND
      await _backgroundTaskService.QueueMemberFeesGenerationAsync(entity.Id, command, cancellationToken);

      // 4. DISPARAR EVENTO MANUALMENTE después de tener el ID     
      /*entity.RaiseDomainEvent(new MemberQrGenerationRequiredDomainEvent(
        entity.Id,
        $"{entity.FirstName} {entity.LastName}",
        entity.Dni!,
        entity.JoinDate,
        entity.ExpirationDate ?? entity.JoinDate.AddYears(1),
        command.CreatedBy,
        _dateTimeProvider.CurrentTime.ToUniversalTime()
      ));*/
      
      await _unitOfWork.CommitAsync(cancellationToken);
      _logger.LogInformation("Member con ID {MemberId} creado satisfactoriamente", entity.Id);

      return Result.Success(entity.Id);
    }
    catch (Exception ex)
    {
      await _unitOfWork.RollbackAsync(cancellationToken);
      _logger.LogError(ex, "Error al crear Member");
      return Result.Failure<long>(MemberErrores.ErrorSave);
    }
  }
}
