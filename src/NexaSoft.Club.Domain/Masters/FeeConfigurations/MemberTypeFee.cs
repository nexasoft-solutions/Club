using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.MemberTypes;

namespace NexaSoft.Club.Domain.Masters.FeeConfigurations;

public class MemberTypeFee : Entity
{
    public long MemberTypeId { get; private set; }
    public MemberType MemberType { get; private set; } = null!;
    public long FeeConfigurationId { get; private set; }
    public FeeConfiguration FeeConfiguration { get; private set; } = null!;
    public decimal? Amount { get; private set; } // sobreescribe DefaultAmount si aplica

    private MemberTypeFee() { }

    public static MemberTypeFee Create(
        long memberTypeId,
        long feeConfigurationId,
        decimal? amount,
        DateTime createdAt,
        string? createdBy
    )
    {
        return new MemberTypeFee
        {
            MemberTypeId = memberTypeId,
            FeeConfigurationId = feeConfigurationId,
            Amount = amount,
            CreatedAt = createdAt,
            CreatedBy = createdBy
        };
    }
}
