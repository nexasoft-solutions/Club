using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollTypes.Queries.GetPayrollTypes;

public class GetPayrollTypesQueryHandler(
    IGenericRepository<PayrollType> _repository
) : IQueryHandler<GetPayrollTypesQuery, Pagination<PayrollTypeResponse>>
{
    public async Task<Result<Pagination<PayrollTypeResponse>>> Handle(GetPayrollTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PayrollTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PayrollTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PayrollTypeResponse>(
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
            return Result.Failure<Pagination<PayrollTypeResponse>>(PayrollTypeErrores.ErrorConsulta);
        }
    }
}
