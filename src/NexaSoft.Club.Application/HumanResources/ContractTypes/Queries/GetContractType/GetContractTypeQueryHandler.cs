using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.ContractTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.ContractTypes.Queries.GetContractType;

public class GetContractTypeQueryHandler(
    IGenericRepository<ContractType> _repository
) : IQueryHandler<GetContractTypeQuery, ContractTypeResponse>
{
    public async Task<Result<ContractTypeResponse>> Handle(GetContractTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new ContractTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<ContractTypeResponse>(ContractTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ContractTypeResponse>(ContractTypeErrores.ErrorConsulta);
        }
    }
}
