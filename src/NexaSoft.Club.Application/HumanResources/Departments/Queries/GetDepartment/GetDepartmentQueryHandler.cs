using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.Departments;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.Departments.Queries.GetDepartment;

public class GetDepartmentQueryHandler(
    IGenericRepository<Department> _repository
) : IQueryHandler<GetDepartmentQuery, DepartmentResponse>
{
    public async Task<Result<DepartmentResponse>> Handle(GetDepartmentQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new DepartmentSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<DepartmentResponse>(DepartmentErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<DepartmentResponse>(DepartmentErrores.ErrorConsulta);
        }
    }
}
