
using MediatR;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberVisits;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Features.MemberVisits.Commands.ExitVisitMember;

public class ExitVisitMemberCommandHandler(
    IGenericRepository<MemberVisit> _visitRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<ExitVisitMemberCommandHandler> _logger
) : ICommandHandler<ExitVisitMemberCommand, bool>
{
    public async Task<Result<bool>> Handle(ExitVisitMemberCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registrando salida para member ID: {MemberId}", command.MemberId);

        try
        {
            // Buscar visita activa usando especificación
            var activeVisitSpec = new ActiveMemberVisitSpecification(command.MemberId);
            var activeVisits = await _visitRepository.ListAsync(activeVisitSpec, cancellationToken);
            var activeVisit = activeVisits.FirstOrDefault();

            if (activeVisit == null)
            {
                _logger.LogWarning("No se encontró visita activa para member ID: {MemberId}", command.MemberId);
                return Result.Failure<bool>(MemberVisitErrores.NoActiveVisit);
            }

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Registrar salida
            activeVisit.RegisterExit(
                TimeOnly.FromDateTime(_dateTimeProvider.CurrentTime),
                command.CheckOutBy,
                command.Notes
            );

            await _visitRepository.UpdateAsync(activeVisit);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Salida registrada exitosamente para member ID: {MemberId}, Visit ID: {VisitId}",
                command.MemberId, activeVisit.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al registrar salida para member ID: {MemberId}", command.MemberId);
            return Result.Failure<bool>(new Error("EXIT_REGISTRATION_ERROR", "Error al registrar la salida"));
        }
    }
}
