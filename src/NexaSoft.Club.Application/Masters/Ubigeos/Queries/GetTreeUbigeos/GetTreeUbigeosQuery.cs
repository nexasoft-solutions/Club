using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Ubigeos;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Queries.GetTreeUbigeos;

public sealed record GetTreeUbigeosQuery
:IQuery<List<UbigeoResponse>>;
    