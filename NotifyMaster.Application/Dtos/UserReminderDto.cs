namespace NotifyMaster.Application.Dtos;

public class UserReminderDto
{
    public long UserId { get; set; }
    public string JobId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public long MessageReminderId { get; set; }
}
