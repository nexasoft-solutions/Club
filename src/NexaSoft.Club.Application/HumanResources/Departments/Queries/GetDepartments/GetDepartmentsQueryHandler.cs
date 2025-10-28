using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.Departments;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.Departments.Queries.GetDepartments;

public class GetDepartmentsQueryHandler(
    IGenericRepository<Department> _repository
) : IQueryHandler<GetDepartmentsQuery, Pagination<DepartmentResponse>>
{
    public async Task<Result<Pagination<DepartmentResponse>>> Handle(GetDepartmentsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new DepartmentSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<DepartmentResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<DepartmentResponse>(
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
            return Result.Failure<Pagination<DepartmentResponse>>(DepartmentErrores.ErrorConsulta);
        }
    }
}
