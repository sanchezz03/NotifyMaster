using NotifyMaster.Application.Dtos;
using NotifyMaster.Common.Enums;

namespace NotifyMaster.Application.DataProviders.Intefaces;

public interface IMessageReminderDataProvider
{
    Task<MessageReminderDto> GetReminderMessageDtoAsync(NotificationPhase notificationPhase);
}
