using NotifyMaster.Common.Enums;

namespace NotifyMaster.Core.Entities;

public class MessageReminder : Base<long>
{
    public string Message { get; set; }
    public string VideoUrl { get; set; }

    public TimeSpan Delay { get; set; }
    public NotificationPhase NotificationPhase { get; set; }

    public long ButtonId { get; set; }
    public Button Button { get; set; }
}
