using Hangfire;
using Hangfire.Server;
using Microsoft.Extensions.Logging;
using NotifyMaster.Application.DataProviders.Intefaces;
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

        var jobId = BackgroundJob.Schedule<ReminderService>(x =>
            x.HandleSendingReminderMessage(chatId, userId, callbackData, button, notificationPhase, null), TimeSpan.FromSeconds(20));

        BackgroundJob.ContinueWith(jobId, () =>
             ScheduleNextJobs(chatId, userId, callbackData, button));
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

        await SendReminderMessage(chatId, reminderMessageDto.Message, callbackData, button);

        await _userReminderDataProvider.AddUserReminderAsync(userId, context.BackgroundJob.Id, DateTime.Now.Add(reminderMessageDto.Delay), reminderMessageDto.Id);
    }

    public async Task ScheduleNextJobs(long chatId, long userId, string callbackData, string button)
    {
        var notificationPhase = await _userService.CheckStatus(userId);

        switch (notificationPhase)
        {
            case NotificationPhase.EarlyReminder:
                BackgroundJob.Schedule(() =>
                   HandleSendingReminderMessage(chatId, userId, callbackData, button, NotificationPhase.EarlyReminder, null),
                   TimeSpan.FromSeconds(30));
                break;

            case NotificationPhase.LateReminder:
                BackgroundJob.Schedule(() =>
                    HandleSendingReminderMessage(chatId, userId, callbackData, button, NotificationPhase.LateReminder, null),
                    TimeSpan.FromSeconds(30));
                break;

            case NotificationPhase.EventPromotion:
                BackgroundJob.Schedule(() =>
                    HandleSendingReminderMessage(chatId, userId, callbackData, button, NotificationPhase.EventPromotion, null),
                    TimeSpan.FromSeconds(60));
                break;

            default:
                _logger.LogInformation("No further jobs to schedule for user {UserId}", userId);
                break;
        }
    }

    public async Task SendReminderMessage(long chatId, string message, string callbackData, string button)
    {
        try
        {
            await _sendMessageHandler.SendMessage(_botClient, chatId, message, callbackData, button);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error sending reminder message: {Message}", ex.Message);
        }
    }
}

