using NotifyMaster.Application.DataProviders.Intefaces;
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

    public async Task<NotificationPhase> CheckStatus(long userId)
    {
        var userDto = await _userDataProvider.GetUserDtoAsync(userId);

        switch (userDto.GroupStatus)
        {
            case GroupStatus.Unregistered:
                return NotificationPhase.EarlyReminder;

            case GroupStatus.Member:
                return NotificationPhase.LateReminder;

            case GroupStatus.Pending:
                return NotificationPhase.EventPromotion;

            default:
                return NotificationPhase.None;
        }
    }

    public async Task UpdateUserStatusAsync(long userId, GroupStatus newStatus)
    {
        var userDto = await _userDataProvider.GetUserDtoAsync(userId);

        if (userDto == null)
        {
            throw new InvalidOperationException($"User with ID {userId} does not exist.");
        }

        userDto.GroupStatus = newStatus;

        _userDataProvider.UpdateUser(userDto);
    }
}
