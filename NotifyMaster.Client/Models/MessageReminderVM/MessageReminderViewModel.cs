using NotifyMaster.Common.Enums;

namespace NotifyMaster.Client.Models.MessageReminderVM;

public class MessageReminderViewModel
{
    public long Id { get; set; }
    public string Message { get; set; }
    public string? VideoUrl { get; set; } = "";
    public TimeSpan Delay { get; set; }
    public string NotificationPhase { get; set; }
}
