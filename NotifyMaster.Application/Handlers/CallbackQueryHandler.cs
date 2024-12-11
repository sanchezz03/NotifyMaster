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

    private readonly TimeSpan TEN_MINUTES = TimeSpan.FromMinutes(1);

    private const string JOIN_CLUB_CALLBACK = "join_club";
    private const string SING_UP_CALLBACK = "sing_up";
    private const string SING_UP_BUTTON = "Записаться";

    public CallbackQueryHandler(ISendMessageHandler sendMessageHandler, IReminderService reminderService, IUserService userService)
    {
        _sendMessageHandler = sendMessageHandler;
        _reminderService = reminderService;
        _userService = userService;
    }

    public async Task HandleCallbackQueryAsync(ITelegramBotClient bot, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        switch (callbackQuery.Data)
        {
            case JOIN_CLUB_CALLBACK:
                await HandleSendingWelcomeMessageAsync(bot, callbackQuery.Message.Chat.Id, callbackQuery.From.Id);
                await SendNextEventReminderAsync(bot, callbackQuery.Message.Chat.Id, callbackQuery.From.Id);
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

    private async Task HandleSendingWelcomeMessageAsync(ITelegramBotClient bot, long chatId, long userId)
    {
        var message = "Добро пожаловать! Мы рады видеть вас среди тех, кто горит стремлением к лучшему. " +
                      "Это полезное видео — ваш первый инструмент на пути к совершенствованию. " +
                      "[Сюда вставить тему первого видео, переписать предложение, чтобы гармонично сочеталось с тематикой]";

        await _reminderService.CancelReminders(userId);
        await _userService.UpdateUserStatusAsync(userId, GroupStatus.Member);

        await _sendMessageHandler.SendMessage(bot, chatId, message);
    }

    private async Task SendNextEventReminderAsync(ITelegramBotClient bot, long chatId, long userId)
    {
        var message = "Вы сделали важный шаг, и я поздравляю вас. " +
                      "Но перемены требуют действий, шаг за шагом. " +
                      "Наше следующее мероприятие — это ваш шанс учиться, расти и становиться более уверенным. " +
                      "Стоимость участия $15. Запишитесь прямо сейчас и получите скидку 50% на первое занятие.";

        var reminerMessage = "Напоминаем вам, что наше занятие уже близко. " +
            "Запишитесь сейчас и начни свой путь обретению уверенности и харизмы." +
            "Стоимость участия $15. На первое занятие скидка 50%.";

        await _reminderService.HandleScheduleReminder(chatId, userId, SING_UP_CALLBACK, SING_UP_BUTTON, NotificationPhase.EventPromotion);

        await _sendMessageHandler.SendMessage(bot, chatId, message, SING_UP_CALLBACK, SING_UP_BUTTON);
    }

    #endregion
}