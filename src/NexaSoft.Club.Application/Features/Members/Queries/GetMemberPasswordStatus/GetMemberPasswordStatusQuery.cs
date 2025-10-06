using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Members.Queries.GetMemberPasswordStatus;

public sealed record  GetMemberPasswordStatusQuery
(
    string Dni,
    DateOnly BirthDate
) : IQuery<bool>;