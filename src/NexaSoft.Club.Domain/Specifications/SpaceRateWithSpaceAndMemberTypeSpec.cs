using NexaSoft.Club.Domain.Masters.SpaceRates;

namespace NexaSoft.Club.Domain.Specifications;

public class SpaceRateWithSpaceAndMemberTypeSpec: BaseSpecification<SpaceRate>
{
    public SpaceRateWithSpaceAndMemberTypeSpec(long spaceRateId) 
        : base(sr => sr.Id == spaceRateId)
    {
        AddInclude(sr => sr.Space!);
        AddInclude(sr => sr.MemberType!);
    }
}