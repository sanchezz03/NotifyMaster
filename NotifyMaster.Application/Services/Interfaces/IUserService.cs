using NotifyMaster.Common.Dtos;
using NotifyMaster.Common.Enums;

namespace NotifyMaster.Application.Services.Interfaces;

public interface IUserService
{
    Task AddUserAsync(long userId, string? userName, string? firstName, string? lastName, GroupStatus groupStatus = GroupStatus.Unregistered);
    Task<List<UserDto>> GetUserDtosAsync();
    Task<UserDto> GetUserDtoAsync(long userId);
    Task UpdateUserStatusAsync(long userId, GroupStatus newStatus);
}
