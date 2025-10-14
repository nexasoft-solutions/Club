using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberFees;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Features.MemberFees.Queries.GetMemberFeesStatus;

public class GetMemberFeesStatusQueryHandler(
    IGenericRepository<MemberFee> _repository
) : IQueryHandler<GetMemberFeesStatusQuery, List<MemberFeeResponse>>
{
    public async Task<Result<List<MemberFeeResponse>>> Handle(GetMemberFeesStatusQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new MemberFeeSpecification(query.MemberId, query.StatusId, query.OrderBy);
            var items = await _repository.ListAsync<MemberFeeResponse>(spec,cancellationToken);

            return Result.Success(items.ToList());

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<MemberFeeResponse>>(MemberFeeErrores.ErrorConsulta);
        }
    }
}
