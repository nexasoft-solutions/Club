using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Features.MemberVisits;
using NexaSoft.Club.Domain.Features.Reservations;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Features.Members.Queries.GetMemberMetrics;

public class GetMemberMetricQueryHandler(
    IGenericRepository<Member> _memberRepository,
    IGenericRepository<MemberVisit> _visitRepository,
    IGenericRepository<Reservation> _reservationRepository,
    IDateTimeProvider _dateTimeProvider,
    ILogger<GetMemberMetricQueryHandler> _logger
) : IQueryHandler<GetMemberMetricQuery, MemberDataResponse>
{
    public async Task<Result<MemberDataResponse>> Handle(GetMemberMetricQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obteniendo metricias del member con ID: {MemberId}", query.MemberId);

        try
        {
            // Obtener el member
            var member = await _memberRepository.GetByIdAsync(query.MemberId, cancellationToken);
            if (member == null)
            {
                _logger.LogWarning("Member con ID {MemberId} no encontrado", query.MemberId);
                return Result.Failure<MemberDataResponse>(MemberErrores.NoEncontrado);
            }

            // Obtener reservas activas
            var activeBookings = await GetActiveBookingsCount(query.MemberId, cancellationToken);

            // Obtener visitas del mes actual
            var monthlyVisits = await GetMonthlyVisitsCount(query.MemberId, cancellationToken);

            // Calcular días restantes en la membresía
            var remainingDays = CalculateRemainingDays(member.ExpirationDate);

            var response = new MemberDataResponse
            (
                member.Balance,
                activeBookings,
                monthlyVisits,
                remainingDays
            );

            _logger.LogInformation("Data del member obtenida exitosamente para ID: {MemberId}", query.MemberId);
            return Result.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener metricas del member con ID: {MemberId}", query.MemberId);
            return Result.Failure<MemberDataResponse>(MemberErrores.ErrorDataMetrics);
        }
    }

    private async Task<int> GetMonthlyVisitsCount(long memberId, CancellationToken cancellationToken)
    {
        var now = _dateTimeProvider.CurrentTime;
        var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        var firstDayOfMonthDateOnly = DateOnly.FromDateTime(firstDayOfMonth);
        var lastDayOfMonthDateOnly = DateOnly.FromDateTime(lastDayOfMonth);

        // Crear y usar la especificación
        var monthlyVisitsSpec = new MonthlyMemberVisitsSpecification(
            memberId,
            firstDayOfMonthDateOnly,
            lastDayOfMonthDateOnly
        );

        var monthlyVisits = await _visitRepository.CountAsync(monthlyVisitsSpec, cancellationToken);
        return monthlyVisits;
    }

    private int CalculateRemainingDays(DateOnly? expirationDate)
    {
        if (!expirationDate.HasValue)
            return 0;

        var today = DateOnly.FromDateTime(_dateTimeProvider.CurrentTime);
        var remaining = expirationDate.Value.DayNumber - today.DayNumber;
        return Math.Max(0, remaining);
    }

    private async Task<int> GetActiveBookingsCount(long memberId, CancellationToken cancellationToken)
    {
        var now = DateOnly.FromDateTime(_dateTimeProvider.CurrentTime);

        // Crear una especificación para contar reservas activas
        var activeBookingsSpec = new ActiveReservationsSpecification(memberId, now);
        var activeBookings = await _reservationRepository.CountAsync(activeBookingsSpec, cancellationToken);

        return activeBookings;
    }
}
