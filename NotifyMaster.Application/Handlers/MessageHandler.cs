using Microsoft.Extensions.Logging;
using NotifyMaster.Application.Handlers.Interfaces;
using NotifyMaster.Application.Services.Interfaces;
using System.Collections.Concurrent;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NotifyMaster.Application.Handlers;

public class MessageHandler : IMessageHandler
{
    private readonly ISendMessageHandler _sendMessageHandler;
    private readonly IReminderService _reminderService;
    private readonly ILogger<MessageHandler> _logger;
    private readonly ConcurrentDictionary<int, string> _reminderSchedule;

    private const string JOIN_CLUB_CALLBACK = "join_club";
    private const string JOIN_CLUB_BUTTON = "Вступить в группу";

    public MessageHandler(ISendMessageHandler sendMessageHandler, IReminderService reminderService, ILogger<MessageHandler> logger)
    {
        _sendMessageHandler = sendMessageHandler;
        _reminderService = reminderService;
        _logger = logger;
        _reminderSchedule = new()
        {
            [3] = "Любые изменения всегда начинаются с маленького с первого шага, с малого, но решительного выбора, в сторону лучшего себя.",
            [15] = "Если вы ожидали знака, того самого момента, чтобы подняться над собой, обрести харизму, уверенность и силу слова, то это именно он, знак.",
            [180] = "Сюда вставить видео кружочек"
        };
    }

    public async Task HandleMessageAsync(ITelegramBotClient bot, Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", message.Type);

        if (message.Text is not { } messageText)
        {
            return;
        }

        switch (messageText.Split(' ')[0])
        {
            case "/start":
                ScheduleReminders(message.Chat.Id, message.From.Id);
                await SendWelcomeMessage(bot, message.Chat.Id);
                break;

            default:
                break;
        }
    }

    #region Private methods

    private void ScheduleReminders(long chatId, long userId)
    {
        foreach (var (delay, message) in _reminderSchedule)
        {
            _reminderService.ScheduleReminder(chatId, userId, message, JOIN_CLUB_CALLBACK, JOIN_CLUB_BUTTON, TimeSpan.FromMinutes(delay));
        }
    }

    private async Task SendWelcomeMessage(ITelegramBotClient bot, long chatId)
    {
        const string welcomeMessage = """
    Знакомо это?
    Вы стоите перед группой и внимание на вас. От волнения охватывает
    дрожь, ладони сыреют, а мысли рассыпаются.
    Вас перебивают, а в споре, вместо того чтобы отстаивать своё, вы
    уступаете.
    Ваши истории кажутся скучными, а народ то и дело отвлекается на
    телефон.
    Мы знаем, каково это — страх быть не услышанным и не быть способным
    убедительно донести свою точку зрения.
    Но есть хорошая новость: всё это можно изменить! Наш клуб — это
    пространство, где вы:
    - Станете уверенным и красноречивым.
    - Научитесь отстаивать свои границы и распознавать манипуляции.
    - Будете рассказывать истории так, чтобы вас слушали с замиранием.
    - Сможете мыслить чётко и говорить ярко даже в самых стрессовых
      ситуациях.
    
    Присоединяйтесь к нам. Здесь вы найдёте знания, поддержку и вдохновение.
    И, как первый шаг, мы отправим вам видео, которое поможет начать.
    """;

        await _sendMessageHandler.SendMessage(bot, chatId, welcomeMessage, JOIN_CLUB_CALLBACK, JOIN_CLUB_BUTTON);
    }

    #endregion
}