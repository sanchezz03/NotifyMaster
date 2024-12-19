using Telegram.Bot;

namespace NotifyMaster.Application.Handlers.Interfaces;

public interface ISendMessageHandler
{
    Task SendMessage(ITelegramBotClient bot, long chatId, string message, string videuUrl = null,
        string callbackData = null, string button = null);
}