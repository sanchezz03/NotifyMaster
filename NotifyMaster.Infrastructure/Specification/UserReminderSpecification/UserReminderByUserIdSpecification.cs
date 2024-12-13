using NotifyMaster.Core.Entities;

namespace NotifyMaster.Infrastructure.Specification.UserReminderSpecification;

public class UserReminderByUserIdSpecification : BaseSpecification<UserReminder>
{
    public UserReminderByUserIdSpecification(long userId)
        : base(u => u.UserId == userId)
    {
        AddInclude(u => u.ReminderMessage);
    }
}
