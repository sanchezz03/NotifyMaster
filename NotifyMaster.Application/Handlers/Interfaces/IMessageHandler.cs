using Telegram.Bot.Types;
using Telegram.Bot;

namespace NotifyMaster.Application.Handlers.Interfaces;

public interface IMessageHandler
{
    Task HandleMessageAsync(ITelegramBotClient bot, Message message, CancellationToken cancellationToken);
}
