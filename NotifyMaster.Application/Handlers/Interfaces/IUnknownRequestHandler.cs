using Telegram.Bot.Types;

namespace NotifyMaster.Application.Handlers.Interfaces;

public interface IUnknownRequestHandler
{
    Task UnknownUpdateHandlerAsync(Update update);
}
