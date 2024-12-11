using NotifyMaster.Common.Enums;

namespace NotifyMaster.Core.Entities;

public class User : Base<long>
{
    public long UserId { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public GroupStatus GroupStatus { get; set; }

    #region Navigation properties
    public ICollection<UserReminder> UserReminders { get; set; }
    #endregion
}
