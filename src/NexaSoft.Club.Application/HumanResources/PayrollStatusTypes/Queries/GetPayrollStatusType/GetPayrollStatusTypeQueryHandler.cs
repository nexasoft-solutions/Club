using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollStatusTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Queries.GetPayrollStatusType;

public class GetPayrollStatusTypeQueryHandler(
    IGenericRepository<PayrollStatusType> _repository
) : IQueryHandler<GetPayrollStatusTypeQuery, PayrollStatusTypeResponse>
{
    public async Task<Result<PayrollStatusTypeResponse>> Handle(GetPayrollStatusTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PayrollStatusTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PayrollStatusTypeResponse>(PayrollStatusTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PayrollStatusTypeResponse>(PayrollStatusTypeErrores.ErrorConsulta);
        }
    }
}
