using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.Banks;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.Banks.Queries.GetBanks;

public class GetBanksQueryHandler(
    IGenericRepository<Bank> _repository
) : IQueryHandler<GetBanksQuery, Pagination<BankResponse>>
{
    public async Task<Result<Pagination<BankResponse>>> Handle(GetBanksQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new BankSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<BankResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<BankResponse>(
                    query.SpecParams.PageIndex,
                    query.SpecParams.PageSize,
                    totalItems,
                    responses
              );

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<BankResponse>>(BankErrores.ErrorConsulta);
        }
    }
}
