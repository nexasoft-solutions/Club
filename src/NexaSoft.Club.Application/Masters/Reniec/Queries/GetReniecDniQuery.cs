using MediatR;
using NexaSoft.Club.Domain.ServicesModel.Reniec;

namespace NexaSoft.Club.Application.Masters.Reniec.Queries;

public sealed record GetReniecDniQuery(string Dni) : IRequest<ReniecDniResponse?>;
