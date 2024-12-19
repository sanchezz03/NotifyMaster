using Microsoft.Extensions.Logging;
using NotifyMaster.Application.DataProviders.Intefaces;
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

    private readonly IMessageReminderDataProvider _messageReminderDataProvider;
    private readonly IButtonDataProvider _buttonDataProvider;

    private readonly ILogger<MessageHandler> _logger;

    private const string JOIN_CLUB_CALLBACK = "join_club";

    public MessageHandler(ISendMessageHandler sendMessageHandler, IUserService userService, IReminderService reminderService, 
        IMessageReminderDataProvider messageReminderDataProvider, IButtonDataProvider buttonDataProvider, ILogger<MessageHandler> logger)
    {
        _sendMessageHandler = sendMessageHandler;
        _userService = userService;
        _reminderService = reminderService;
        _buttonDataProvider = buttonDataProvider;
        _messageReminderDataProvider = messageReminderDataProvider;
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

        await HandleCommandAsync(command, message, bot);
    }

    #region Private methods

    private async Task HandleCommandAsync(BotCommand command, Message message, ITelegramBotClient bot)
    {
        switch (command)
        {
            case BotCommand.Start:
                var messageDto = await _messageReminderDataProvider.GetReminderMessageDtoAsync(NotificationPhase.None);
                var buttonDto = await _buttonDataProvider.GetAsync(messageDto.ButtonId);
                await HandleScheduleReminders(message.Chat.Id, message.From.Id, buttonDto.Name, message.From.Username, message.From.FirstName, message.From.LastName);
                await _sendMessageHandler.SendMessage(bot, message.Chat.Id, messageDto.Message, null, JOIN_CLUB_CALLBACK, buttonDto.Name);
                break;

            default:
                break;
        }
    }

    public BotCommand GetCommandFromMessageText(string messageText)
    {
        var commandText = messageText.Split(' ')[0];

        return Enum.TryParse(commandText.TrimStart('/'), true, out BotCommand command) ? command : BotCommand.Unknown;
    }

    private async Task HandleScheduleReminders(long chatId, long userId, string buttonName, string? userName, string? firstName, string? lastName)
    {
        await _userService.AddUserAsync(userId, userName, firstName, lastName);
        await _reminderService.HandleScheduleReminder(chatId, userId, JOIN_CLUB_CALLBACK, buttonName, NotificationPhase.Welcome);
    }

    #endregion
}