using Hangfire;
using Microsoft.Extensions.Logging;
using NotifyMaster.Application.Handlers.Interfaces;
using NotifyMaster.Application.Services.Interfaces;
using System.Collections.Concurrent;
using Telegram.Bot;

namespace NotifyMaster.Application.Services;

public class ReminderService : IReminderService
{
    private readonly ConcurrentDictionary<long, List<string>> _userReminders;
    private readonly ISendMessageHandler _sendMessageHandler;
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<ReminderService> _logger;

    public ReminderService(ISendMessageHandler sendMessageHandler, ITelegramBotClient botClient, ILogger<ReminderService> logger)
    {
        _sendMessageHandler = sendMessageHandler;
        _userReminders = new();
        _botClient = botClient;
        _logger = logger;
    }

    public void ScheduleReminder(long chatId, long userId, string message, string callbackData, string button, TimeSpan delay)
    {
        var jobId = BackgroundJob.Schedule(() => SendReminderMessage(chatId, message, callbackData, button), delay);

        if (!_userReminders.ContainsKey(userId))
        {
            _userReminders[userId] = new List<string>();
        }

        _userReminders[userId].Add(jobId);
    }

    public void CancelReminders(long userId)
    {
        if (_userReminders.TryRemove(userId, out var jobIds))
        {
            foreach (var jobId in jobIds)
            {
                BackgroundJob.Delete(jobId);
            }
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