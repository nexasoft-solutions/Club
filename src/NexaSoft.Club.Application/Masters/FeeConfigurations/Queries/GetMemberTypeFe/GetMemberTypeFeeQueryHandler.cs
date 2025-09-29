using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Queries.GetMemberTypeFee;

public class GetMemberTypeFeeQueryHandler(
    IGenericRepository<MemberTypeFee> _repository
) : IQueryHandler<GetMemberTypeFeeQuery, List<MemberTypeFeeResponse>>
{
    public async Task<Result<List<MemberTypeFeeResponse>>> Handle(GetMemberTypeFeeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new MemberTypeFeeSpecification(query.SpecParams);
            var result = await _repository.ListAsync<MemberTypeFeeResponse>(spec, cancellationToken);
            //var totalItems = await _repository.CountAsync(spec, cancellationToken);

          
            return Result.Success(result.ToList());

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<MemberTypeFeeResponse>>(MemberTypeFeeErrores.ErrorConsulta);
        }
    }
}
