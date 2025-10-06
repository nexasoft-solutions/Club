using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Members.Commands.CreateMember;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberFees;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Infrastructure.Services;

public class MemberFeesBackgroundGenerator: IMemberFeesBackgroundGenerator
{
    private readonly IGenericRepository<Member> _memberRepository;
    private readonly IGenericRepository<MemberFee> _memberFeeRepository;
    private readonly IGenericRepository<MemberTypeFee> _memberTypeFeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<MemberFeesBackgroundGenerator> _logger;

    public MemberFeesBackgroundGenerator(
        IGenericRepository<Member> memberRepository,
        IGenericRepository<MemberFee> memberFeeRepository,
        IGenericRepository<MemberTypeFee> memberTypeFeeRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        ILogger<MemberFeesBackgroundGenerator> logger)
    {
        _memberRepository = memberRepository;
        _memberFeeRepository = memberFeeRepository;
        _memberTypeFeeRepository = memberTypeFeeRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task GenerateMemberFeesAsync(MemberFeesBackgroundData data, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Generando cuotas para member {MemberId} en background", data.MemberId);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. CARGAR DATOS
            var member = await _memberRepository.GetByIdAsync(data.MemberId, cancellationToken);
            if (member == null)
            {
                throw new InvalidOperationException($"Member {data.MemberId} no encontrado");
            }

            // 2. GENERAR CUOTAS
            await GenerateFeesForMemberAsync(member, data, cancellationToken);

            // 3. ACTUALIZAR MEMBER COMO COMPLETADO
            member.MarkAsFeesGenerationCompleted();
            await _memberRepository.UpdateAsync(member);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Cuotas generadas exitosamente para member {MemberId}", data.MemberId);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error generando cuotas para member {MemberId}", data.MemberId);
            throw; // Esta excepción activará la compensación
        }
    }

    private async Task GenerateFeesForMemberAsync(Member member, MemberFeesBackgroundData data, CancellationToken cancellationToken)
    {
        var specParams = new BaseSpecParams
        {
            NoPaging = true,
            SearchFields = "membertype",
            Search = member.MemberTypeId.ToString()
        };

        var spec = new MemberTypeFeeSpecification(specParams);
        var typeFees = await _memberTypeFeeRepository.ListAsync<MemberTypeFeeResponse>(spec, cancellationToken);

        foreach (var typeFee in typeFees)
        {
            switch ((PeriodicidadEnum)typeFee.PeriodicityId)
            {
                case PeriodicidadEnum.Mensual:
                    await GenerateMonthlyFees(member, typeFee, data.CreatedBy, cancellationToken);
                    break;

                case PeriodicidadEnum.UicaVez:
                    await GenerateOneTimeFee(member, typeFee, data.CreatedBy, cancellationToken);
                    break;

                case PeriodicidadEnum.Anual:
                    await GenerateYearlyFee(member, typeFee, data.CreatedBy, cancellationToken);
                    break;

                default:
                    await GenerateSpecialFee(member, typeFee, data.CreatedBy, cancellationToken);
                    break;
            }
        }
    }

    private async Task GenerateMonthlyFees(Member member, MemberTypeFeeResponse typeFee, string createdBy, CancellationToken cancellationToken)
    {
        var start = member.JoinDate;
        var end = member.ExpirationDate ?? start.AddYears(1);

        var months = MonthsBetweenInclusive(start, end);
        if (months <= 0) return;

        var amount = typeFee.Amount ?? typeFee.DefaultAmount ?? 0m;

        for (int i = 0; i <= months; i++)
        {
            var periodDate = start.AddMonths(i);
            var period = periodDate.ToString("yyyy-MM");

            var dueDate = new DateOnly(periodDate.Year, periodDate.Month, typeFee.DueDay ?? 5);

            var monthlyFee = MemberFee.Create(
                member.Id,
                typeFee.Id,
                period,
                amount,
                dueDate,
                i == 0 ? "Pendiente" : "Futura",
                0.00M,
                amount,
                (int)EstadosEnum.Activo,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                createdBy
            );

            await _memberFeeRepository.AddAsync(monthlyFee, cancellationToken);
        }
    }

    private async Task GenerateOneTimeFee(Member member, MemberTypeFeeResponse typeFee, string createdBy, CancellationToken cancellationToken)
    {
        var dueDate = member.JoinDate.AddDays(30);
        var amount = typeFee.Amount ?? typeFee.DefaultAmount ?? 0m;

        var oneTimeFee = MemberFee.Create(
            member.Id,
            typeFee.Id,
            "ENTRADA",
            amount,
            dueDate,
            "Pendiente",
            0.00M,
            amount,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            createdBy
        );

        await _memberFeeRepository.AddAsync(oneTimeFee, cancellationToken);
    }

    private async Task GenerateYearlyFee(Member member, MemberTypeFeeResponse typeFee, string createdBy, CancellationToken cancellationToken)
    {
        var year = member.JoinDate.Year;
        var month = member.JoinDate.Month;
        var day = Math.Min(typeFee.DueDay ?? 15, DateTime.DaysInMonth(year, month));

        var dueDate = new DateOnly(year, month, day);
        var amount = typeFee.Amount ?? typeFee.DefaultAmount ?? 0m;

        var yearlyFee = MemberFee.Create(
            member.Id,
            typeFee.Id,
            member.JoinDate.Year.ToString(),
            amount,
            dueDate,
            "Pendiente",
            0.00M,
            amount,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            createdBy
        );

        await _memberFeeRepository.AddAsync(yearlyFee, cancellationToken);
    }

    private async Task GenerateSpecialFee(Member member, MemberTypeFeeResponse typeFee, string createdBy, CancellationToken cancellationToken)
    {
        var dueDate = member.JoinDate.AddMonths(1);
        var amount = typeFee.Amount ?? typeFee.DefaultAmount ?? 0m;

        var specialFee = MemberFee.Create(
            member.Id,
            typeFee.Id,
            "ESPECIAL",
            amount,
            dueDate,
            "Pendiente",
            0.00M,
            amount,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            createdBy
        );

        await _memberFeeRepository.AddAsync(specialFee, cancellationToken);
    }

    private static int MonthsBetweenInclusive(DateOnly start, DateOnly end)
    {
        if (end < start) return 0;
        return (end.Year - start.Year) * 12 + (end.Month - start.Month) + 1;
    }
}
