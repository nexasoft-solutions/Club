using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.EmployeesInfo.Queries.GetEmployeesInfo;

public class GetEmployeesInfoQueryHandler(
    IGenericRepository<EmployeeInfo> _repository
) : IQueryHandler<GetEmployeesInfoQuery, Pagination<EmployeeInfoResponse>>
{
    public async Task<Result<Pagination<EmployeeInfoResponse>>> Handle(GetEmployeesInfoQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new EmployeeInfoSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<EmployeeInfoResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<EmployeeInfoResponse>(
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
            return Result.Failure<Pagination<EmployeeInfoResponse>>(EmployeeInfoErrores.ErrorConsulta);
        }
    }
}
