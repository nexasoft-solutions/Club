using MediatR;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberFees;
using NexaSoft.Club.Domain.Features.Members.Events;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.Members.Commands.CreateMember;

public class MemberCreatedDomainEventHandler(
    IGenericRepository<MemberFee> _memberFeeRepository,
    IGenericRepository<MemberTypeFee> _memberTypeFeeRepository,
    IDateTimeProvider _dateTimeProvider,
    ILogger<MemberCreatedDomainEventHandler> _logger
) : INotificationHandler<MemberCreatedDomainEvent>
{
    public async Task Handle(MemberCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Procesando evento de creación de miembro ID: {MemberId}", notification.MemberId);


        try
        {
            BaseSpecParams SpecParams = new BaseSpecParams();
            SpecParams.NoPaging = true;
            SpecParams.SearchFields = "membertype";
            SpecParams.Search = notification.MemberTypeId.ToString();

            var spec = new MemberTypeFeeSpecification(SpecParams);
            var typeFees = await _memberTypeFeeRepository.ListAsync<MemberTypeFeeResponse>(spec, cancellationToken);

            foreach (var typeFee in typeFees)
            {

                switch ((PeriodicidadEnum)typeFee.PeriodicityId)
                {
                    case PeriodicidadEnum.Mensual:
                        await GenerateMonthlyFees(notification, typeFee, cancellationToken);
                        break;

                    case PeriodicidadEnum.UicaVez:
                        await GenerateOneTimeFee(notification, typeFee, cancellationToken);
                        break;

                    case PeriodicidadEnum.Anual:
                        await GenerateYearlyFee(notification, typeFee, cancellationToken);
                        break;

                    default:
                        await GenerateSpecialFee(notification, typeFee, cancellationToken);
                        break;
                }
            }

            _logger.LogInformation("Cuotas generadas exitosamente para miembro ID: {MemberId}", notification.MemberId);
        }
        catch (Exception ex)
        {
            //await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al generar cuotas para miembro ID: {MemberId}", notification.MemberId);
            throw; // Re-lanzar para que el command handler original también falle
        }
    }

    private async Task GenerateMonthlyFees(MemberCreatedDomainEvent notification, MemberTypeFeeResponse typeFee, CancellationToken cancellationToken)
    {
        // Generar mensualidades desde JoinDate hasta ExpirationDate
        //var start = notification.JoinDate;
        //var end = notification.ExpirationDate;

        var start = notification.JoinDate;
        var end = notification.ExpirationDate ?? start.AddYears(1); // fallback: 1 año si no hay expiración

        var months = MonthsBetweenInclusive(start, end);
        if (months <= 0) return;

        //var config = typeFee.FeeConfiguration;
        var amount = typeFee.Amount ?? typeFee.DefaultAmount ?? 0m;

        //var months = (end.Year - start.Year) * 12 + (end.Month - start.Month);

        for (int i = 0; i <= months; i++)
        {
            var periodDate = start.AddMonths(i);
            var period = periodDate.ToString("yyyy-MM");

            var dueDate = new DateOnly(periodDate.Year, periodDate.Month, typeFee.DueDay ?? 5);

            var monthlyFee = MemberFee.Create(
                notification.MemberId,
                typeFee.Id,
                period,
                amount,
                dueDate,
                i == 0 ? "Pendiente" : "Futura",
                0.00M,
                amount,
                (int)EstadosEnum.Activo,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                notification.CreatedBy
            );

            await _memberFeeRepository.AddAsync(monthlyFee, cancellationToken);
        }
    }

    private static int MonthsBetweenInclusive(DateOnly start, DateOnly end)
    {
        if (end < start) return 0;
        return (end.Year - start.Year) * 12 + (end.Month - start.Month) + 1;
    }

    private async Task GenerateOneTimeFee(MemberCreatedDomainEvent notification, MemberTypeFeeResponse typeFee, CancellationToken cancellationToken)
    {
        var dueDate = notification.JoinDate.AddDays(30);
        var amount = typeFee.Amount ?? typeFee.DefaultAmount ?? 0m;

        var oneTimeFee = MemberFee.Create(
            notification.MemberId,
            typeFee.Id,
            "ENTRADA",
            amount,
            dueDate,
            "Pendiente",
            0.00M,
            amount,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            notification.CreatedBy
        );

        await _memberFeeRepository.AddAsync(oneTimeFee, cancellationToken);
    }

    private async Task GenerateYearlyFee(MemberCreatedDomainEvent notification, MemberTypeFeeResponse typeFee, CancellationToken cancellationToken)
    {
        var year = notification.JoinDate.Year;
        var month = notification.JoinDate.Month; // o fijo: 1 para enero
        var day = typeFee.DueDay ?? 15;

        // Evita error de fechas inválidas
        day = Math.Min(day, DateTime.DaysInMonth(year, month));

        var dueDate = new DateOnly(year, month, day);
        var amount = typeFee.Amount ?? typeFee.DefaultAmount ?? 0m;

        var yearlyFee = MemberFee.Create(
            notification.MemberId,
            typeFee.Id,
            notification.JoinDate.Year.ToString(),
            amount,
            dueDate,
            "Pendiente",
            0.00M,
            amount,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            notification.CreatedBy
        );

        await _memberFeeRepository.AddAsync(yearlyFee, cancellationToken);
    }

    private async Task GenerateSpecialFee(MemberCreatedDomainEvent notification, MemberTypeFeeResponse typeFee, CancellationToken cancellationToken)
    {
        var dueDate = notification.JoinDate.AddMonths(1);
        var amount = typeFee.Amount ?? typeFee.DefaultAmount ?? 0m;

        var specialFee = MemberFee.Create(
            notification.MemberId,
            typeFee.Id,
            "ESPECIAL",
            amount,
            dueDate,
            "Pendiente",
            0.00M,
            amount,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            notification.CreatedBy
        );

        await _memberFeeRepository.AddAsync(specialFee, cancellationToken);
    }
}
