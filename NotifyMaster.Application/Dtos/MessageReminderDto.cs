using NotifyMaster.Common.Enums;

namespace NotifyMaster.Application.Dtos;

public class MessageReminderDto
{
    public long Id { get; set; }
    public string Message { get; set; }
    public TimeSpan Delay { get; set; }
    public NotificationPhase NotificationPhase { get; set; }
}
