using AutoMapper;
using Microsoft.IdentityModel.Tokens;
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

    public async Task<List<MessageReminderDto>> GetListReminderMessageDtoAsync(NotificationPhase notificationPhase)
    {
        var reminderMessageDtos = await _reminderRepository.GetListByNotificationPhaseAsync(notificationPhase);

        if (reminderMessageDtos.IsNullOrEmpty())
        {
            return new List<MessageReminderDto>();
        }

        return _mapper.Map<List<MessageReminderDto>>(reminderMessageDtos);
    }

    public async Task<MessageReminderDto> GetReminderMessageDtoAsync(NotificationPhase notificationPhase)
    {
        var reminderMessageDtos = await _reminderRepository.GetListByNotificationPhaseAsync(notificationPhase);
        var reminderMessageDto = reminderMessageDtos.FirstOrDefault();

        if (reminderMessageDto == null)
        {
            return new MessageReminderDto();
        }

        return _mapper.Map<MessageReminderDto>(reminderMessageDto);
    }
}
