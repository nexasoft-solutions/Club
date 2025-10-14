using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;

namespace NexaSoft.Club.Domain.Specifications;

public class SpaceAvailabilitiesBySpaceSpec : BaseSpecification<SpaceAvailability>
{
    public SpaceAvailabilitiesBySpaceSpec(long spaceAvailabilityId) 
        : base(sa => sa.Id == spaceAvailabilityId) // Removemos && sa.IsAvailable porque necesitamos validar después
    {
        AddInclude(sa => sa.Space!);
    }
}

// Para disponibilidades por espacio y día de la semana
public class SpaceAvailabilitiesBySpaceAndDaySpec : BaseSpecification<SpaceAvailability>
{
    public SpaceAvailabilitiesBySpaceAndDaySpec(long spaceId, int dayOfWeek) 
        : base(sa => sa.SpaceId == spaceId && sa.DayOfWeek == dayOfWeek)
    {
        AddInclude(sa => sa.Space!);
        AddOrderBy(sa => sa.StartTime);
    }
}
