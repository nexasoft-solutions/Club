using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Ubigeos;

namespace NexaSoft.Agro.Application.Masters.Ubigeos.Queries.GetUbigeo;

public sealed record GetUbigeoQuery(
    Guid Id
) : IQuery<Result<UbigeoResponse>>;
