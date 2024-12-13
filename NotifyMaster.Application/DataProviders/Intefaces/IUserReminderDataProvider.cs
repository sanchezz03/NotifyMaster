using NotifyMaster.Application.Dtos;

namespace NotifyMaster.Application.DataProviders.Intefaces;

public interface IUserReminderDataProvider
{
    Task AddUserReminderAsync(long userId, string jobId, DateTime scheduleTime, long reminderMessageId);
    Task<List<UserReminderDto>> GetUserRemindersAsync(long userId);
    Task DeleteByUserId(long userId);
}
