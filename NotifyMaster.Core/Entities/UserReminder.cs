namespace NotifyMaster.Core.Entities;

public class UserReminder : Base<long>
{
    public string JobId { get; set; }
    public DateTime ScheduledTime { get; set; }

    public long ReminderMessageId { get; set; }
    public long UserId { get; set; }

    #region Navigation properties
    public MessageReminder ReminderMessage { get; set; }
    public User User { get; set; }

    #endregion
}
