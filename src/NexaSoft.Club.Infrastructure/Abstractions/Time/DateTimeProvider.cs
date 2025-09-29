using NexaSoft.Club.Application.Abstractions.Time;

namespace NexaSoft.Club.Infrastructure.Abstractions.Time;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentTime => DateTime.UtcNow;
}

