using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.MemberFees;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.MemberFees.Queries.GetMemberFees;

public class GetMemberFeesQueryHandler(
    IGenericRepository<MemberFee> _repository
) : IQueryHandler<GetMemberFeesQuery, Pagination<MemberFeeResponse>>
{
    public async Task<Result<Pagination<MemberFeeResponse>>> Handle(GetMemberFeesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new MemberFeeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<MemberFeeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<MemberFeeResponse>(
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
            return Result.Failure<Pagination<MemberFeeResponse>>(MemberFeeErrores.ErrorConsulta);
        }
    }
}
