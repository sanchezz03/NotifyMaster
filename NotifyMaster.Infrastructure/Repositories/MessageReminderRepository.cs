using NotifyMaster.Common.Enums;
using NotifyMaster.Core.Entities;
using NotifyMaster.Infrastructure.Data;
using NotifyMaster.Infrastructure.Specification.MessageReminderSpecification;

namespace NotifyMaster.Infrastructure.Repositories;

public class MessageReminderRepository : BaseRelationalRepository<MessageReminder>
{
    public MessageReminderRepository(NotifyMasterDbContext dbContext)
        : base(dbContext) { }

    public async Task<MessageReminder> GetByNotificationPhaseAsync(NotificationPhase notificationPhase)
    {
        var specification = new MessageReminderByNotificationPhaseSpecification(notificationPhase);
        var entities = await FindBySpecificationAsync(specification);
        var entity = entities.FirstOrDefault(e => e.NotificationPhase.Equals(notificationPhase));
        return entity;
    }
}
