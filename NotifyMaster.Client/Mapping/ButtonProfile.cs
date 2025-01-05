using AutoMapper;
using NotifyMaster.Client.Models.ButtonVM;
using NotifyMaster.Common.Dtos;

namespace NotifyMaster.Client.Mapping;

public class ButtonProfile : Profile
{
    public ButtonProfile()
    {
        CreateMap<ButtonDto, ButtonViewModel>();

        CreateMap<ButtonViewModel, ButtonDto>();
    }
}
