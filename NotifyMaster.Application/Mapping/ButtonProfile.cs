using AutoMapper;
using NotifyMaster.Application.Dtos;
using NotifyMaster.Core.Entities;

namespace NotifyMaster.Application.Mapping;

public class ButtonProfile : Profile
{
    public ButtonProfile()
    {
        CreateMap<Button, ButtonDto>();
    }
}
