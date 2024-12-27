using AutoMapper;
using NotifyMaster.Client.Models.MessageReminderVM;
using NotifyMaster.Common.Dtos;

namespace NotifyMaster.Client.Mapping;

public class MessageReminderProfile : Profile
{
    public MessageReminderProfile()
    {
        CreateMap<MessageReminderDto, MessageReminderViewModel>();
        
        CreateMap<MessageReminderViewModel, MessageReminderDto>();
    }
}
