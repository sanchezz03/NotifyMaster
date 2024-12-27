using AutoMapper;
using NotifyMaster.Common.Dtos;
using NotifyMaster.Core.Entities;

namespace NotifyMaster.Application.Mapping;

public class MessageReminderProfile : Profile
{
    public MessageReminderProfile()
    {
        CreateMap<MessageReminder, MessageReminderDto>();

        CreateMap<MessageReminderDto, MessageReminder>();
    }
}
