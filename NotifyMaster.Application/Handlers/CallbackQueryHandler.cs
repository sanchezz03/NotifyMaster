using NotifyMaster.Application.Handlers.Interfaces;
using NotifyMaster.Application.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NotifyMaster.Application.Handlers;

public class CallbackQueryHandler : ICallbackQueryHandler
{
    private readonly ISendMessageHandler _sendMessageHandler;
    private readonly IReminderService _reminderService;

    private readonly TimeSpan TEN_MINUTES = TimeSpan.FromMinutes(1);

    private const string JOIN_CLUB_CALLBACK = "join_club";
    private const string SING_UP_CALLBACK = "sing_up";
    private const string SING_UP_BUTTON = "Записаться";

    public CallbackQueryHandler(ISendMessageHandler sendMessageHandler, IReminderService reminderService)
    {
        _sendMessageHandler = sendMessageHandler;
        _reminderService = reminderService;
    }

    public async Task HandleCallbackQueryAsync(ITelegramBotClient bot, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        switch (callbackQuery.Data)
        {
            case JOIN_CLUB_CALLBACK:
                await SendWelcomeMessageAsync(bot, callbackQuery.Message.Chat.Id, callbackQuery.From.Id);
                await SendNextEventReminderAsync(bot, callbackQuery.Message.Chat.Id, callbackQuery.From.Id);
                break;

            case SING_UP_CALLBACK:
                _reminderService.CancelReminders(callbackQuery.From.Id);
                await _sendMessageHandler.SendMessage(bot, callbackQuery.Message.Chat.Id, "Google Form");
                break;

            default:
                break;
        }
    }

    #region Private methods

    private async Task SendWelcomeMessageAsync(ITelegramBotClient bot, long chatId, long userId)
    {
        var message = "Добро пожаловать! Мы рады видеть вас среди тех, кто горит стремлением к лучшему. " +
                      "Это полезное видео — ваш первый инструмент на пути к совершенствованию. " +
                      "[Сюда вставить тему первого видео, переписать предложение, чтобы гармонично сочеталось с тематикой]";

        _reminderService.CancelReminders(userId);

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

        _reminderService.ScheduleReminder(chatId, userId, reminerMessage, SING_UP_CALLBACK, SING_UP_BUTTON, TEN_MINUTES);

        await _sendMessageHandler.SendMessage(bot, chatId, message, SING_UP_CALLBACK, SING_UP_BUTTON);
    }

    #endregion
}