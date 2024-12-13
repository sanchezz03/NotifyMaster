using AutoMapper;
using NotifyMaster.Application.Dtos;
using NotifyMaster.Core.Entities;

namespace NotifyMaster.Application.Mapping;

public class UserReminderProfile : Profile
{
    public UserReminderProfile()
    {
        CreateMap<UserReminder, UserReminderDto>();

        CreateMap<UserReminderDto, UserReminder>();
    }
}
