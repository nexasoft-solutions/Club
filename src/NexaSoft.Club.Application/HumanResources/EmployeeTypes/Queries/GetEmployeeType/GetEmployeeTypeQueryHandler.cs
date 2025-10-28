using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.EmployeeTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.EmployeeTypes.Queries.GetEmployeeType;

public class GetEmployeeTypeQueryHandler(
    IGenericRepository<EmployeeType> _repository
) : IQueryHandler<GetEmployeeTypeQuery, EmployeeTypeResponse>
{
    public async Task<Result<EmployeeTypeResponse>> Handle(GetEmployeeTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new EmployeeTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<EmployeeTypeResponse>(EmployeeTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<EmployeeTypeResponse>(EmployeeTypeErrores.ErrorConsulta);
        }
    }
}
