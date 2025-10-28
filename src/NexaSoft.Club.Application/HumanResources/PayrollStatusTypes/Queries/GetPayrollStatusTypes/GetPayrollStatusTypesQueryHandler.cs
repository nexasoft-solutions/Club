using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollStatusTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Queries.GetPayrollStatusTypes;

public class GetPayrollStatusTypesQueryHandler(
    IGenericRepository<PayrollStatusType> _repository
) : IQueryHandler<GetPayrollStatusTypesQuery, Pagination<PayrollStatusTypeResponse>>
{
    public async Task<Result<Pagination<PayrollStatusTypeResponse>>> Handle(GetPayrollStatusTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PayrollStatusTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PayrollStatusTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PayrollStatusTypeResponse>(
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
            return Result.Failure<Pagination<PayrollStatusTypeResponse>>(PayrollStatusTypeErrores.ErrorConsulta);
        }
    }
}
