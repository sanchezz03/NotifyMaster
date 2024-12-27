using NotifyMaster.Application.DataProviders.Intefaces;
using NotifyMaster.Common.Dtos;
using NotifyMaster.Application.Handlers.Interfaces;
using NotifyMaster.Application.Services.Interfaces;
using NotifyMaster.Common.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NotifyMaster.Application.Handlers;

public class CallbackQueryHandler : ICallbackQueryHandler
{
    private readonly ISendMessageHandler _sendMessageHandler;
    private readonly IReminderService _reminderService;
    private readonly IUserService _userService;

    private readonly IMessageReminderDataProvider _messageReminderDataProvider;
    private readonly IButtonDataProvider _buttonDataProvider;

    private const string JOIN_CLUB_CALLBACK = "join_club";
    private const string SING_UP_CALLBACK = "sing_up";

    public CallbackQueryHandler(ISendMessageHandler sendMessageHandler, IReminderService reminderService, IUserService userService,
        IMessageReminderDataProvider messageReminderDataProvider, IButtonDataProvider buttonDataProvider)
    {
        _sendMessageHandler = sendMessageHandler;
        _reminderService = reminderService;
        _userService = userService;

        _messageReminderDataProvider = messageReminderDataProvider;
        _buttonDataProvider = buttonDataProvider;
    }

    public async Task HandleCallbackQueryAsync(ITelegramBotClient bot, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        switch (callbackQuery.Data)
        {
            case JOIN_CLUB_CALLBACK:
                var reminderMessageDtos = await _messageReminderDataProvider.GetListReminderMessageDtoAsync(NotificationPhase.EventPromotion);
                await HandleSendingWelcomeMessageAsync(bot, callbackQuery.Message.Chat.Id, callbackQuery.From.Id, reminderMessageDtos);
                await SendNextEventReminderAsync(bot, callbackQuery.Message.Chat.Id, callbackQuery.From.Id, reminderMessageDtos);
                break;

            case SING_UP_CALLBACK:
                await _reminderService.CancelReminders(callbackQuery.From.Id);
                await _userService.UpdateUserStatusAsync(callbackQuery.From.Id, GroupStatus.Pending);
                await _sendMessageHandler.SendMessage(bot, callbackQuery.Message.Chat.Id, "Google Form");
                break;

            default:
                break;
        }
    }

    #region Private methods

    private async Task HandleSendingWelcomeMessageAsync(ITelegramBotClient bot, long chatId, long userId, List<MessageReminderDto> messageReminderDtos)
    {
        var messageDto = messageReminderDtos.FirstOrDefault();
        var buttonDto = await _buttonDataProvider.GetAsync(messageDto.ButtonId);

        var userDto = await _userService.GetUserDtoAsync(userId);
        await _reminderService.CancelReminders(userDto.Id);

        await _userService.UpdateUserStatusAsync(userId, GroupStatus.Member);

        await _sendMessageHandler.SendMessage(bot, chatId, messageDto.Message, null, null, buttonDto.Name);
    }

    private async Task SendNextEventReminderAsync(ITelegramBotClient bot, long chatId, long userId, List<MessageReminderDto> messageReminderDtos)
    {
        var messageDto = messageReminderDtos.LastOrDefault();
        var buttonDto = await _buttonDataProvider.GetAsync(messageDto.ButtonId);

        await _sendMessageHandler.SendMessage(bot, chatId, messageDto.Message, messageDto.VideoUrl, SING_UP_CALLBACK, buttonDto.Name);

        await _reminderService.HandleScheduleReminder(chatId, userId, SING_UP_CALLBACK, buttonDto.Name, NotificationPhase.EventPromotionReminder);
    }

    #endregion
}