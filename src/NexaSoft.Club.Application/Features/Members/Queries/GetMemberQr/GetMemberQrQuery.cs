using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Members.Queries.GetMemberQr;

public sealed record  GetMemberQrQuery
(
    long UserId
) : IQuery<string>;