using Telegram.Bot.Types;
using Telegram.Bot;

namespace NotifyMaster.Application.Handlers.Interfaces;

public interface ICallbackQueryHandler
{
    Task HandleCallbackQueryAsync(ITelegramBotClient bot, CallbackQuery callbackQuery, CancellationToken cancellationToken);
}