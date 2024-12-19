using NotifyMaster.Common.Enums;
using NotifyMaster.Core.Entities;

namespace NotifyMaster.Infrastructure.Specification.MessageReminderSpecification;

public class MessageReminderByNotificationPhaseSpecification : BaseSpecification<MessageReminder>
{
    public MessageReminderByNotificationPhaseSpecification(NotificationPhase notificationPhase)
        : base(m => m.NotificationPhase.Equals(notificationPhase))
    {
        AddInclude(m => m.Button);
    }
}
