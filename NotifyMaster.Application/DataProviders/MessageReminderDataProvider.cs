using AutoMapper;
using NotifyMaster.Application.DataProviders.Intefaces;
using NotifyMaster.Application.Dtos;
using NotifyMaster.Common.Enums;
using NotifyMaster.Infrastructure.Repositories;

namespace NotifyMaster.Application.DataProviders;

public class MessageReminderDataProvider : IMessageReminderDataProvider
{
    private readonly MessageReminderRepository _reminderRepository;
    private readonly IMapper _mapper;

    public MessageReminderDataProvider(MessageReminderRepository reminderRepository, IMapper mapper)
    {
        _reminderRepository = reminderRepository;
        _mapper = mapper;   
    }

    public async Task<MessageReminderDto> GetReminderMessageDtoAsync(NotificationPhase notificationPhase)
    {
        var reminderMessageDto = await _reminderRepository.GetByNotificationPhaseAsync(notificationPhase);

        return _mapper.Map<MessageReminderDto>(reminderMessageDto);
    }
}
