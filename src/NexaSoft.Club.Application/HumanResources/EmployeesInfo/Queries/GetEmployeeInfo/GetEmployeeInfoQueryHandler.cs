using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.EmployeesInfo.Queries.GetEmployeeInfo;

public class GetEmployeeInfoQueryHandler(
    IGenericRepository<EmployeeInfo> _repository
) : IQueryHandler<GetEmployeeInfoQuery, EmployeeInfoResponse>
{
    public async Task<Result<EmployeeInfoResponse>> Handle(GetEmployeeInfoQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new EmployeeInfoSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<EmployeeInfoResponse>(EmployeeInfoErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<EmployeeInfoResponse>(EmployeeInfoErrores.ErrorConsulta);
        }
    }
}
