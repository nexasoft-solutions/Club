using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.EmploymentContracts;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.EmploymentContracts.Queries.GetEmploymentContract;

public class GetEmploymentContractQueryHandler(
    IGenericRepository<EmploymentContract> _repository
) : IQueryHandler<GetEmploymentContractQuery, EmploymentContractResponse>
{
    public async Task<Result<EmploymentContractResponse>> Handle(GetEmploymentContractQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new EmploymentContractSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<EmploymentContractResponse>(EmploymentContractErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<EmploymentContractResponse>(EmploymentContractErrores.ErrorConsulta);
        }
    }
}
