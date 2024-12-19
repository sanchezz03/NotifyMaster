using NotifyMaster.Application.DataProviders.Intefaces;
using NotifyMaster.Application.Dtos;
using NotifyMaster.Application.Services.Interfaces;
using NotifyMaster.Common.Enums;

namespace NotifyMaster.Application.Services;

public class UserService : IUserService
{
    private readonly IUserDataProvider _userDataProvider;

    public UserService(IUserDataProvider userDataProvider)
    {
        _userDataProvider = userDataProvider;
    }

    public async Task AddUserAsync(long userId, string? userName, string? firstName, string? lastName, GroupStatus groupStatus = GroupStatus.Unregistered)
    {
        await _userDataProvider.AddUserAsync(userId, userName, firstName, lastName, groupStatus); 
    }

    public async Task<UserDto> GetUserDtoAsync(long userId)
    {
        return await _userDataProvider.GetUserDtoAsync(userId);
    }

    public async Task UpdateUserStatusAsync(long userId, GroupStatus newStatus)
    {
        var userDto = await _userDataProvider.GetUserDtoAsync(userId);

        if (userDto == null)
        {
            throw new InvalidOperationException($"User with ID {userId} does not exist.");
        }

        userDto.GroupStatus = newStatus;

        await _userDataProvider.UpdateUserAsync(userDto);
    }
}
