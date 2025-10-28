using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.BankAccountTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.BankAccountTypes.Queries.GetBankAccountType;

public class GetBankAccountTypeQueryHandler(
    IGenericRepository<BankAccountType> _repository
) : IQueryHandler<GetBankAccountTypeQuery, BankAccountTypeResponse>
{
    public async Task<Result<BankAccountTypeResponse>> Handle(GetBankAccountTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new BankAccountTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<BankAccountTypeResponse>(BankAccountTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<BankAccountTypeResponse>(BankAccountTypeErrores.ErrorConsulta);
        }
    }
}
