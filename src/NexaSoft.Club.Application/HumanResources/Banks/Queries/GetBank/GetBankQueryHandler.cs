using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.Banks;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.Banks.Queries.GetBank;

public class GetBankQueryHandler(
    IGenericRepository<Bank> _repository
) : IQueryHandler<GetBankQuery, BankResponse>
{
    public async Task<Result<BankResponse>> Handle(GetBankQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new BankSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<BankResponse>(BankErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<BankResponse>(BankErrores.ErrorConsulta);
        }
    }
}
