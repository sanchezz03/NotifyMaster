using AutoMapper;
using NotifyMaster.Common.Dtos;
using NotifyMaster.Core.Entities;

namespace NotifyMaster.Application.Mapping;

public class ButtonProfile : Profile
{
    public ButtonProfile()
    {
        CreateMap<Button, ButtonDto>();

        CreateMap<ButtonDto, Button>();
    }
}
