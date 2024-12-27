using AutoMapper;
using NotifyMaster.Client.Models.UserVM;
using NotifyMaster.Common.Dtos;
using NotifyMaster.Common.Helpers;

namespace NotifyMaster.Client.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, UserViewModel>()
            .ForMember(dest => dest.GroupStatus, opt => opt.MapFrom(src => src.GroupStatus.ToDisplayText()));
        
        CreateMap<UserDto, UserDetailViewModel>()
            .ForMember(dest => dest.GroupStatus, opt => opt.MapFrom(src => src.GroupStatus.ToDisplayText()));
    }
}
