using NexaSoft.Agro.Application.Abstractions.Time;

namespace NexaSoft.Agro.Infrastructure.Abstractions.Time;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentTime => DateTime.UtcNow;
}

