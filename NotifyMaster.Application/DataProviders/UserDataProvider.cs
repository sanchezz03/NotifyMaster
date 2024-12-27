using AutoMapper;
using NotifyMaster.Application.DataProviders.Intefaces;
using NotifyMaster.Common.Dtos;
using NotifyMaster.Common.Enums;
using NotifyMaster.Core.Entities;
using NotifyMaster.Infrastructure.Repositories;

namespace NotifyMaster.Application.DataProviders;

public class UserDataProvider : IUserDataProvider
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserDataProvider(UserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task AddUserAsync(long userId, string? userName, string? firstName, string? lastName, GroupStatus groupStatus = GroupStatus.Unregistered)
    {
        var user = await _userRepository.GetByUserIdAsync(userId);  

        if (user != null)
        {
            return;
        }

        var userDto = GetUserDto(userId, userName, firstName, lastName, groupStatus);
        user = _mapper.Map<User>(userDto);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<List<UserDto>> GetUserDtosAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<List<UserDto>>(users);   
    }

    public async Task<UserDto> GetUserDtoAsync(long userId)
    {
        var user = await _userRepository.GetByUserIdAsync(userId);

        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateUserAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }

    #region Private methods

    private UserDto GetUserDto(long userId, string? userName, string? firstName, string? lastName, GroupStatus groupStatus)
    {
        return new UserDto()
        {
            UserId = userId,
            UserName = userName,
            FirstName = firstName,
            LastName = lastName,
            GroupStatus = groupStatus,
        };
    }

    #endregion
}
