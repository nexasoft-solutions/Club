using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Masters.Users;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Features.MemberVisits;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.MemberVisits.Commands.CreateMemberVisit;

public class CreateMemberVisitCommandHandler(
    IGenericRepository<MemberVisit> _repository,
    IUserRoleRepository _userRoleRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateMemberVisitCommandHandler> _logger
) : ICommandHandler<CreateMemberVisitCommand, long>
{
    public async Task<Result<long>> Handle(CreateMemberVisitCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de MemberVisit");
        // Verificar que el member existe y está activo
        var member = await _userRoleRepository.GetUserWithMemberAsync(command.MemberId, cancellationToken);
        if (member == null)
        {
            _logger.LogWarning("Member con ID {MemberId} no encontrado", command.MemberId);
            return Result.Failure<long>(MemberErrores.NoEncontrado);
        }

        // Validar QR code
        if (member.QrCode != command.QrCodeUsed || member.QrExpiration < DateOnly.FromDateTime(_dateTimeProvider.CurrentTime))
        {
            _logger.LogWarning("QR code inválido o expirado para member ID: {MemberId}", command.MemberId);
            return Result.Failure<long>(MemberErrores.QrInvalidoOExpirado);
        }

        // Crear especificación para buscar visitas activas
        var activeVisitSpec = new ActiveMemberVisitSpecification(command.MemberId);
        var activeVisits = await _repository.ListAsync(activeVisitSpec, cancellationToken);
        var activeVisit = activeVisits.FirstOrDefault();

        if (activeVisit != null)
        {
            _logger.LogWarning("Member ID {MemberId} ya tiene una visita activa", command.MemberId);
            return Result.Failure<long>(MemberErrores.YaTieneVisitaActiva);
        }


        var entity = MemberVisit.Create(
            command.MemberId,
            DateOnly.FromDateTime(_dateTimeProvider.CurrentTime),
            TimeOnly.FromDateTime(_dateTimeProvider.CurrentTime),
            command.QrCodeUsed,
            command.Notes,
            command.CreatedBy,
            (int)VisitTypeEnum.Normal,
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
            _logger.LogInformation("MemberVisit con ID {MemberVisitId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear MemberVisit");
            return Result.Failure<long>(MemberVisitErrores.ErrorSave);
        }
    }
}
