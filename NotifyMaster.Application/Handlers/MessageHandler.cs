using Microsoft.Extensions.Logging;
using NotifyMaster.Application.Handlers.Interfaces;
using NotifyMaster.Application.Services.Interfaces;
using NotifyMaster.Common.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using BotCommand = NotifyMaster.Common.Enums.BotCommand;

namespace NotifyMaster.Application.Handlers;

public class MessageHandler : IMessageHandler
{
    private readonly ISendMessageHandler _sendMessageHandler;
    private readonly IReminderService _reminderService;
    private readonly IUserService _userService;
    private readonly ILogger<MessageHandler> _logger;

    private const string JOIN_CLUB_CALLBACK = "join_club";
    private const string JOIN_CLUB_BUTTON = "Вступить в группу";

    public MessageHandler(ISendMessageHandler sendMessageHandler, IUserService userService,
        IReminderService reminderService, ILogger<MessageHandler> logger)
    {
        _sendMessageHandler = sendMessageHandler;
        _userService = userService;
        _reminderService = reminderService;
        _logger = logger;
    }

    public async Task HandleMessageAsync(ITelegramBotClient bot, Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", message.Type);

        if (message.Text is not { } messageText)
        {
            return;
        }

        var command = GetCommandFromMessageText(message.Text);

        switch (command)
        {
            case BotCommand.Start:
                await HandleScheduleReminders(message.Chat.Id, message.From.Id, message.From.Username, message.From.FirstName, message.From.LastName);
                await SendWelcomeMessage(bot, message.Chat.Id);
                break;

            default:
                break;
        }
    }

    #region Private methods

    public BotCommand GetCommandFromMessageText(string messageText)
    {
        var commandText = messageText.Split(' ')[0];

        return Enum.TryParse(commandText.TrimStart('/'), true, out BotCommand command) ? command : BotCommand.Unknown;
    }

    private async Task HandleScheduleReminders(long chatId, long userId, string? userName, string? firstName, string? lastName)
    {
        await _userService.AddUserAsync(userId, userName, firstName, lastName);
        await _reminderService.HandleScheduleReminder(chatId, userId, JOIN_CLUB_CALLBACK, JOIN_CLUB_BUTTON, NotificationPhase.Welcome);
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