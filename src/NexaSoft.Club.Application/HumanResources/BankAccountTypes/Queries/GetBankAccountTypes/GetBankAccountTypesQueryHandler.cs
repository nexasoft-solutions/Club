using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.BankAccountTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.BankAccountTypes.Queries.GetBankAccountTypes;

public class GetBankAccountTypesQueryHandler(
    IGenericRepository<BankAccountType> _repository
) : IQueryHandler<GetBankAccountTypesQuery, Pagination<BankAccountTypeResponse>>
{
    public async Task<Result<Pagination<BankAccountTypeResponse>>> Handle(GetBankAccountTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new BankAccountTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<BankAccountTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<BankAccountTypeResponse>(
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
            return Result.Failure<Pagination<BankAccountTypeResponse>>(BankAccountTypeErrores.ErrorConsulta);
        }
    }
}
