using NotifyMaster.Common.Enums;

namespace NotifyMaster.Application.Services.Interfaces;

public interface IReminderService
{
    Task HandleScheduleReminder(long chatId, long userId, string callbackData, string button, NotificationPhase notificationPhase);
    Task CancelReminders(long userId);
}