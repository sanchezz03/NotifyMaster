namespace NotifyMaster.Core.Entities;

public class UserReminder : Base<long>
{
    public string JobId { get; set; }

    public long UserId { get; set; }

    #region Navigation properties
    public User User { get; set; }

    #endregion
}
