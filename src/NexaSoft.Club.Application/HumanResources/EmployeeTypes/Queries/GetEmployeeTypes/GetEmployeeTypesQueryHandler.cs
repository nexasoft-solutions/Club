using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.EmployeeTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.EmployeeTypes.Queries.GetEmployeeTypes;

public class GetEmployeeTypesQueryHandler(
    IGenericRepository<EmployeeType> _repository
) : IQueryHandler<GetEmployeeTypesQuery, Pagination<EmployeeTypeResponse>>
{
    public async Task<Result<Pagination<EmployeeTypeResponse>>> Handle(GetEmployeeTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new EmployeeTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<EmployeeTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<EmployeeTypeResponse>(
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
            return Result.Failure<Pagination<EmployeeTypeResponse>>(EmployeeTypeErrores.ErrorConsulta);
        }
    }
}
