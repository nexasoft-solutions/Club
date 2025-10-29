using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.EmploymentContracts;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.EmploymentContracts.Queries.GetEmploymentContracts;

public class GetEmploymentContractsQueryHandler(
    IGenericRepository<EmploymentContract> _repository
) : IQueryHandler<GetEmploymentContractsQuery, Pagination<EmploymentContractResponse>>
{
    public async Task<Result<Pagination<EmploymentContractResponse>>> Handle(GetEmploymentContractsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new EmploymentContractSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<EmploymentContractResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<EmploymentContractResponse>(
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
            return Result.Failure<Pagination<EmploymentContractResponse>>(EmploymentContractErrores.ErrorConsulta);
        }
    }
}
