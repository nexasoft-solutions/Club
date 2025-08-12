using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Queries.GetColaborador;

public sealed record GetColaboradorQuery(
    Guid Id
) : IQuery<ColaboradorResponse>;
