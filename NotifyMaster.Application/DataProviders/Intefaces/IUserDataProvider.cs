using NotifyMaster.Application.Dtos;
using NotifyMaster.Common.Enums;

namespace NotifyMaster.Application.DataProviders.Intefaces;

public interface IUserDataProvider
{
    Task AddUserAsync(long userId, string? userName, string? firstName, string? lastName, GroupStatus groupStatus = GroupStatus.Unregistered);
    Task<UserDto> GetUserDtoAsync(long userId);
    Task UpdateUserAsync(UserDto userDto);
}
