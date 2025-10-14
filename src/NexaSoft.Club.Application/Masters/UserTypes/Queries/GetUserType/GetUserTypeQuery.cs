using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.UserTypes;

namespace NexaSoft.Club.Application.Masters.UserTypes.Queries.GetUserType;

public sealed record GetUserTypeQuery(
    long Id
) : IQuery<UserTypeResponse>;
