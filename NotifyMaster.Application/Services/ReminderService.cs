using Hangfire;
using Microsoft.Extensions.Logging;
using NotifyMaster.Application.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace NotifyMaster.Application.Services;

public class ReminderService : IReminderService
{
    private readonly Dictionary<long, List<string>> _userReminders;
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<ReminderService> _logger;

    public ReminderService(ITelegramBotClient botClient, ILogger<ReminderService> logger)
    {
        _userReminders = new();
        _botClient = botClient;
        _logger = logger;
    }

    public void ScheduleReminder(long chatId, long userId, string message, string callbackData, TimeSpan delay)
    {
        var jobId = BackgroundJob.Schedule(() => SendReminderMessage(chatId, message, callbackData), delay);

        if (!_userReminders.ContainsKey(userId))
        {
            _userReminders[userId] = new List<string>();
        }

        _userReminders[userId].Add(jobId);
    }

    public void CancelReminders(long userId)
    {
        if (_userReminders.TryGetValue(userId, out var jobIds))
        {
            foreach (var jobId in jobIds)
            {
                BackgroundJob.Delete(jobId);
            }
            _userReminders.Remove(userId);
        }
    }

    public async Task SendReminderMessage(long chatId, string message, string callbackData)
    {
        try
        {
            var joinClubButton = new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Вструпить в групу", callbackData)
            );

            await _botClient.SendTextMessageAsync(
               chatId: chatId,
               text: message,
               parseMode: ParseMode.Markdown,
               replyMarkup: joinClubButton
           );
        }
        catch (Exception ex)
        {
            _logger.LogError("Error sending reminder message: {Message}", ex.Message);
        }
    }
}