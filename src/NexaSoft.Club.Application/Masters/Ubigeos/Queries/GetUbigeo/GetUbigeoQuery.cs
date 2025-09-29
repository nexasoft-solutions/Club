using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Ubigeos;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Queries.GetUbigeo;

public sealed record GetUbigeoQuery(
    long Id
) : IQuery<UbigeoResponse>;
