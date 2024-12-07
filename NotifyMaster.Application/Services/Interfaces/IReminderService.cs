namespace NotifyMaster.Application.Services.Interfaces;

public interface IReminderService
{
    void ScheduleReminder(long chatId, long userId, string message, string callbackData, string button, TimeSpan delay);
    void CancelReminders(long userId);
}
