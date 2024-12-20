using NotifyMaster.Core.Entities;
using NotifyMaster.Infrastructure.Data;

namespace NotifyMaster.Infrastructure.Repositories;

public class ButtonRepository : BaseRelationalRepository<Button>
{
    public ButtonRepository(NotifyMasterDbContext dbContext)
        : base(dbContext) { }
}
