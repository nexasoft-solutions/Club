using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Queries.GetCompanyBankAccount;

public class GetCompanyBankAccountQueryHandler(
    IGenericRepository<CompanyBankAccount> _repository
) : IQueryHandler<GetCompanyBankAccountQuery, CompanyBankAccountResponse>
{
    public async Task<Result<CompanyBankAccountResponse>> Handle(GetCompanyBankAccountQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new CompanyBankAccountSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<CompanyBankAccountResponse>(CompanyBankAccountErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<CompanyBankAccountResponse>(CompanyBankAccountErrores.ErrorConsulta);
        }
    }
}
