using MediatR;
using NexaSoft.Club.Application.Storages;
using NexaSoft.Club.Domain.ServicesModel.Reniec;

namespace NexaSoft.Club.Application.Masters.Reniec.Queries;

public sealed class GetReniecRucQueryHandler : IRequestHandler<GetReniecRucQuery, ReniecRucResponse?>
{
    private readonly IReniecService _reniecService;

    public GetReniecRucQueryHandler(IReniecService reniecService)
    {
        _reniecService = reniecService;
    }

    public async Task<ReniecRucResponse?> Handle(GetReniecRucQuery request, CancellationToken cancellationToken)
    {
        return await _reniecService.GetRucInfoAsync(request.Ruc);
    }
}
