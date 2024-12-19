using NotifyMaster.Common.Enums;
using NotifyMaster.Core.Entities;
using NotifyMaster.Infrastructure.Data;
using NotifyMaster.Infrastructure.Specification.MessageReminderSpecification;

namespace NotifyMaster.Infrastructure.Repositories;

public class MessageReminderRepository : BaseRelationalRepository<MessageReminder>
{
    public MessageReminderRepository(NotifyMasterDbContext dbContext)
        : base(dbContext) { }

    public async Task<List<MessageReminder>> GetListByNotificationPhaseAsync(NotificationPhase notificationPhase)
    {
        var specification = new MessageReminderByNotificationPhaseSpecification(notificationPhase);
        var entities = await FindBySpecificationAsync(specification);

        return entities.ToList();
    }
}
