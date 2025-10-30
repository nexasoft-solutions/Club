using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Queries.GetPayrollConceptEmployeeTypes;

public class GetPayrollConceptEmployeeTypesQueryHandler(
    IGenericRepository<PayrollConceptEmployeeType> _repository
) : IQueryHandler<GetPayrollConceptEmployeeTypesQuery, Pagination<PayrollConceptEmployeeTypeResponse>>
{
    public async Task<Result<Pagination<PayrollConceptEmployeeTypeResponse>>> Handle(GetPayrollConceptEmployeeTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PayrollConceptEmployeeTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PayrollConceptEmployeeTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PayrollConceptEmployeeTypeResponse>(
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
            return Result.Failure<Pagination<PayrollConceptEmployeeTypeResponse>>(PayrollConceptEmployeeTypeErrores.ErrorConsulta);
        }
    }
}
