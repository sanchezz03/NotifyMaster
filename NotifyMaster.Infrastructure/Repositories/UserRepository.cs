using NotifyMaster.Core.Entities;
using NotifyMaster.Infrastructure.Data;
using NotifyMaster.Infrastructure.Specification.UserSpecification;

namespace NotifyMaster.Infrastructure.Repositories;

public class UserRepository : BaseRelationalRepository<User>
{
    public UserRepository(NotifyMasterDbContext dbContext)
        : base(dbContext) { }

    public async Task<User> GetByUserIdAsync(long userId)
    {
        var specification = new UserByUserIdSpecification(userId);
        var entities = await FindBySpecificationAsync(specification);
        var entity = entities.FirstOrDefault(e => e.UserId == userId);
        return entity;
    }
}


