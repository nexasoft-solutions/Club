using MediatR;
using NexaSoft.Club.Application.Storages;
using NexaSoft.Club.Domain.ServicesModel.Reniec;

namespace NexaSoft.Club.Application.Masters.Reniec.Queries;

public sealed class GetReniecDniQueryHandler : IRequestHandler<GetReniecDniQuery, ReniecDniResponse?>
{
    private readonly IReniecService _reniecService;

    public GetReniecDniQueryHandler(IReniecService reniecService)
    {
        _reniecService = reniecService;
    }

    public async Task<ReniecDniResponse?> Handle(GetReniecDniQuery request, CancellationToken cancellationToken)
    {
        return await _reniecService.GetDniInfoAsync(request.Dni);
    }
}
