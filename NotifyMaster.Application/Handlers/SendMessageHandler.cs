using NotifyMaster.Application.Handlers.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace NotifyMaster.Application.Handlers;

public class SendMessageHandler : ISendMessageHandler
{
    public async Task SendMessage(ITelegramBotClient bot, long chatId, string message, string callbackData = null, string button = null)
    {
        var replyMarkup = button != null && callbackData != null
            ? CreateButton(button, callbackData)
            : null;

        await bot.SendTextMessageAsync(
            chatId: chatId,
            text: message,
            parseMode: ParseMode.Markdown,
            replyMarkup: replyMarkup
        );
    }

    #region Private region

    private InlineKeyboardMarkup CreateButton(string buttonName, string callbackData)
    {
        return new InlineKeyboardMarkup(
            InlineKeyboardButton.WithCallbackData(buttonName, callbackData)
        );
    }

    #endregion
}