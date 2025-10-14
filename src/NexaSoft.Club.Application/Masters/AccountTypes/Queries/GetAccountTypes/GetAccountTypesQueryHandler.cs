using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.AccountTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.AccountTypes.Queries.GetAccountTypes;

public class GetAccountTypesQueryHandler(
    IGenericRepository<AccountType> _repository
) : IQueryHandler<GetAccountTypesQuery, Pagination<AccountTypeResponse>>
{
    public async Task<Result<Pagination<AccountTypeResponse>>> Handle(GetAccountTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new AccountTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<AccountTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<AccountTypeResponse>(
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
            return Result.Failure<Pagination<AccountTypeResponse>>(AccountTypeErrores.ErrorConsulta);
        }
    }
}
