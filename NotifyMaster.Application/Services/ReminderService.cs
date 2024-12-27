using Hangfire;
using Hangfire.Server;
using Microsoft.Extensions.Logging;
using NotifyMaster.Application.DataProviders.Intefaces;
using NotifyMaster.Common.Dtos;
using NotifyMaster.Application.Handlers.Interfaces;
using NotifyMaster.Application.Services.Interfaces;
using NotifyMaster.Common.Enums;
using Telegram.Bot;

namespace NotifyMaster.Application.Services;

public class ReminderService : IReminderService
{
    private readonly IMessageReminderDataProvider _messageReminderDataProvider;
    private readonly IUserReminderDataProvider _userReminderDataProvider;
    private readonly ISendMessageHandler _sendMessageHandler;
    private readonly IUserService _userService;
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<ReminderService> _logger;

    public ReminderService(IMessageReminderDataProvider messageReminderDataProvider, IUserReminderDataProvider userReminderDataProvider,
        ISendMessageHandler sendMessageHandler, IUserService userService, ITelegramBotClient botClient, ILogger<ReminderService> logger)
    {
        _messageReminderDataProvider = messageReminderDataProvider;
        _userReminderDataProvider = userReminderDataProvider;
        _sendMessageHandler = sendMessageHandler;
        _userService = userService;
        _botClient = botClient;
        _logger = logger;
    }

    public async Task HandleScheduleReminder(long chatId, long userId, string callbackData, string button, NotificationPhase notificationPhase)
    {
        var reminderMessageDto = await _messageReminderDataProvider.GetReminderMessageDtoAsync(notificationPhase);
        var userDto = await _userService.GetUserDtoAsync(userId);
     
        var jobId = BackgroundJob.Schedule<ReminderService>(x =>
            x.HandleSendingReminderMessage(chatId, userId, callbackData, button, notificationPhase, null), reminderMessageDto.Delay);

        await _userReminderDataProvider.AddUserReminderAsync(userDto.Id, jobId);

        if (notificationPhase == NotificationPhase.Welcome)
        {
            await ScheduleNextJobs(userDto, chatId, userId, callbackData, button);
        }
    }

    public async Task CancelReminders(long userId)
    {
        var userReminderDtos = await _userReminderDataProvider.GetUserRemindersAsync(userId);

        foreach (var dto in userReminderDtos)
        {
            BackgroundJob.Delete(dto.JobId);
        }

        await _userReminderDataProvider.DeleteByUserId(userId);
    }

    public async Task HandleSendingReminderMessage(long chatId, long userId, string callbackData, string button, NotificationPhase notificationPhase, PerformContext context)
    {
        var reminderMessageDto = await _messageReminderDataProvider.GetReminderMessageDtoAsync(notificationPhase);

        await SendReminderMessage(chatId, reminderMessageDto.Message, callbackData, button, reminderMessageDto.VideoUrl);
    }

    public async Task ScheduleNextJobs(UserDto userDto, long chatId, long userId, string callbackData, string button)
    {
        var earlyReminderMessageDto = await _messageReminderDataProvider.GetReminderMessageDtoAsync(NotificationPhase.EarlyReminder);

        var jobId = BackgroundJob.Schedule<ReminderService>(x =>
             x.HandleSendingReminderMessage(chatId, userId, callbackData, button, NotificationPhase.EarlyReminder, null), earlyReminderMessageDto.Delay);

        await _userReminderDataProvider.AddUserReminderAsync(userDto.Id, jobId);

        var lateReminderMessageDto = await _messageReminderDataProvider.GetReminderMessageDtoAsync(NotificationPhase.LateReminder);

        jobId = BackgroundJob.Schedule<ReminderService>(x =>
                x.HandleSendingReminderMessage(chatId, userId, callbackData, button, NotificationPhase.LateReminder, null), lateReminderMessageDto.Delay);

        await _userReminderDataProvider.AddUserReminderAsync(userDto.Id, jobId);
    }

    public async Task SendReminderMessage(long chatId, string message, string callbackData, string button, string? videoUrl = null)
    {
        try
        {
            await _sendMessageHandler.SendMessage(_botClient, chatId, message, videoUrl, callbackData, button);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error sending reminder message: {Message}", ex.Message);
        }
    }
}

