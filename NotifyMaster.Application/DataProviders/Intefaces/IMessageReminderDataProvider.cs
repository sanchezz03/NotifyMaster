using NotifyMaster.Common.Dtos;
using NotifyMaster.Common.Enums;

namespace NotifyMaster.Application.DataProviders.Intefaces;

public interface IMessageReminderDataProvider
{
    Task<MessageReminderDto> GetAsync(long id);
    Task<List<MessageReminderDto>> GetListReminderMessageDtoAsync();
    Task<List<MessageReminderDto>> GetListReminderMessageDtoAsync(NotificationPhase notificationPhase);
    Task<MessageReminderDto> GetReminderMessageDtoAsync(NotificationPhase notificationPhase);
    Task Update(MessageReminderDto messageReminderDto);
}
