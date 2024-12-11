using NotifyMaster.Common.Enums;
using NotifyMaster.Core.Entities;
using NotifyMaster.Infrastructure.Data;
using NotifyMaster.Infrastructure.Specification.UserReminderSpecification;

namespace NotifyMaster.Infrastructure.Repositories;

public class UserReminderRepository : BaseRelationalRepository<UserReminder>
{
    public UserReminderRepository(NotifyMasterDbContext dbContext)
        : base(dbContext) { }

    public async Task<List<UserReminder>> GetAllByUserIdAsync(long userId)
    {
        var specification = new UserReminderByUserIdSpecification(userId);
        var entities = await FindBySpecificationAsync(specification);
        return entities.ToList();
    }
}

