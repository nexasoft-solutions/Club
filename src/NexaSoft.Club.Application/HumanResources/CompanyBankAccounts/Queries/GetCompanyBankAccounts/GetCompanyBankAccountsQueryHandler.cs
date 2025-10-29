using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Queries.GetCompanyBankAccounts;

public class GetCompanyBankAccountsQueryHandler(
    IGenericRepository<CompanyBankAccount> _repository
) : IQueryHandler<GetCompanyBankAccountsQuery, Pagination<CompanyBankAccountResponse>>
{
    public async Task<Result<Pagination<CompanyBankAccountResponse>>> Handle(GetCompanyBankAccountsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CompanyBankAccountSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<CompanyBankAccountResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<CompanyBankAccountResponse>(
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
            return Result.Failure<Pagination<CompanyBankAccountResponse>>(CompanyBankAccountErrores.ErrorConsulta);
        }
    }
}
