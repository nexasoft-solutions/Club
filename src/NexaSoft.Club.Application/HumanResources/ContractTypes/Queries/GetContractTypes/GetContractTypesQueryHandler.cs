using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.ContractTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.ContractTypes.Queries.GetContractTypes;

public class GetContractTypesQueryHandler(
    IGenericRepository<ContractType> _repository
) : IQueryHandler<GetContractTypesQuery, Pagination<ContractTypeResponse>>
{
    public async Task<Result<Pagination<ContractTypeResponse>>> Handle(GetContractTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ContractTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<ContractTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<ContractTypeResponse>(
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
            return Result.Failure<Pagination<ContractTypeResponse>>(ContractTypeErrores.ErrorConsulta);
        }
    }
}
