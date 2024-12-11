using AutoMapper;
using NotifyMaster.Application.Dtos;
using NotifyMaster.Core.Entities;

namespace NotifyMaster.Application.Mapping;

public class MessageReminderProfile : Profile
{
    public MessageReminderProfile()
    {
        CreateMap<MessageReminder, MessageReminderDto>();
    }
}
