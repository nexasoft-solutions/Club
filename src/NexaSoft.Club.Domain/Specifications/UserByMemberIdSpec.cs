using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Domain.Specifications;

public class UserByMemberIdSpec : BaseSpecification<User>
{
    public UserByMemberIdSpec(long memberId)
        : base(u => u.MemberId == memberId)
    {
    }
}
