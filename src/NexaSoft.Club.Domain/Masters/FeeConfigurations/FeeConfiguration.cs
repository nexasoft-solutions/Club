using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Masters.AccountingCharts;
using NexaSoft.Club.Domain.Masters.Periodicities;

namespace NexaSoft.Club.Domain.Masters.FeeConfigurations;

public class FeeConfiguration : Entity
{
   
    public string? FeeName { get; private set; } = string.Empty;
    public long PeriodicityId { get; private set; }
    public Periodicity? Periodicity { get; set; }
    public int? DueDay { get; private set; }
    public decimal? DefaultAmount { get; private set; } // opcional
    public bool IsVariable { get; private set; } // Ej: mensualidad depende del MemberType
    public int Priority { get; private set; } // orden de cobro
    public bool AppliesToFamily { get; private set; }
    public long? IncomeAccountId { get; private set; }
    public AccountingChart? IncomeAccount { get; private set; }
    public int StateFeeConfiguration { get; private set; }

    private FeeConfiguration() { }

    private FeeConfiguration(
        string? feeName, 
        long periodicityId, 
        int? dueDay, 
        decimal? defaultAmount,
        bool isVariable,
        int priority,
        bool appliesToFamily,
        long? incomeAccountId,
        int stateFeeConfiguration,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        FeeName = feeName;
        PeriodicityId = periodicityId;     
        DefaultAmount = defaultAmount;
        IsVariable = isVariable;
        Priority = priority;
        DueDay = dueDay;
        AppliesToFamily = appliesToFamily;
        IncomeAccountId = incomeAccountId;
        StateFeeConfiguration = stateFeeConfiguration;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static FeeConfiguration Create(
        string? feeName, 
        long periodicityId, 
        int? dueDay, 
        decimal? defaultAmount,
        bool isVariable,
        int priority,
        bool appliesToFamily,
        long? incomeAccountId,
        int stateFeeConfiguration,
        DateTime createdAt,
        string? createdBy
    )
    {
        var entity = new FeeConfiguration(
            feeName,
            periodicityId,
            dueDay,
            defaultAmount,
            isVariable,
            priority,
            appliesToFamily,
            incomeAccountId,
            stateFeeConfiguration,
            createdAt,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? feeName, 
        long periodicityId, 
        int? dueDay, 
        decimal? defaultAmount,
        bool isVariable,
        int priority,
        bool appliesToFamily,
        long? incomeAccountId,
        DateTime utcNow,
        string? updatedBy
    )
    {
        FeeName = feeName;
        PeriodicityId = periodicityId;     
        DefaultAmount = defaultAmount;
        IsVariable = isVariable;
        Priority = priority;
        DueDay = dueDay;
        AppliesToFamily = appliesToFamily;
        IncomeAccountId = incomeAccountId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateFeeConfiguration = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
