using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.AccountTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.AccountTypes.Queries.GetAccountType;

public class GetAccountTypeQueryHandler(
    IGenericRepository<AccountType> _repository
) : IQueryHandler<GetAccountTypeQuery, AccountTypeResponse>
{
    public async Task<Result<AccountTypeResponse>> Handle(GetAccountTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new AccountTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<AccountTypeResponse>(AccountTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<AccountTypeResponse>(AccountTypeErrores.ErrorConsulta);
        }
    }
}
