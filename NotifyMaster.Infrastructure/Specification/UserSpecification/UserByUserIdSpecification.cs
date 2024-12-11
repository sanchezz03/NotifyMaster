using NotifyMaster.Core.Entities;

namespace NotifyMaster.Infrastructure.Specification.UserSpecification;

public class UserByUserIdSpecification : BaseSpecification<User>
{
    public UserByUserIdSpecification(long userId)
        : base(u => u.UserId == userId)
    {
    }
}
