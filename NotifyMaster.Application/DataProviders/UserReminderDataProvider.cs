using AutoMapper;
using NotifyMaster.Application.DataProviders.Intefaces;
using NotifyMaster.Application.Dtos;
using NotifyMaster.Core.Entities;
using NotifyMaster.Infrastructure.Repositories;

namespace NotifyMaster.Application.DataProviders;

public class UserReminderDataProvider : IUserReminderDataProvider
{
    private readonly UserReminderRepository _userReminderRepository;
    private readonly IMapper _mapper;

    public UserReminderDataProvider(UserReminderRepository userReminderRepository, IMapper mapper)
    {
        _userReminderRepository = userReminderRepository;   
        _mapper = mapper;
    }

    public async Task AddUserReminderAsync(long userId, string jobId, DateTime scheduleTime, long reminderMessageId)
    {
        var userReminder = GetUserReminder(userId, jobId, scheduleTime, reminderMessageId);

        await _userReminderRepository.AddAsync(userReminder);
    }

    public async Task<List<UserReminderDto>> GetUserRemindersAsync(long userId)
    {
        var userReminders = await _userReminderRepository.GetAllByUserIdAsync(userId);
       
        return _mapper.Map<List<UserReminderDto>>(userReminders);
    }

    public async Task DeleteByUserId(long userId)
    {
        var userReminders = await _userReminderRepository.GetAllByUserIdAsync(userId);

        await _userReminderRepository.DeleteRangeAsync(userReminders);
    }

    #region Private methods

    private UserReminder GetUserReminder(long userId, string jobId, DateTime scheduleTime, long reminderMessageId)
    {
        var userReminderDto = new UserReminderDto()
        {
            UserId = userId,
            JobId = jobId,
            ScheduledTime = scheduleTime,
            MessageReminderId = reminderMessageId
        };

        return _mapper.Map<UserReminder>(userReminderDto);
    }

    #endregion
}
