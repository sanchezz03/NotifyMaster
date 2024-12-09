using Microsoft.Extensions.Logging;
using NotifyMaster.Application.Handlers.Interfaces;
using Telegram.Bot.Types;

namespace NotifyMaster.Application.Handlers;

public class UnknownRequestHandler : IUnknownRequestHandler
{
    private readonly ILogger<UnknownRequestHandler> _logger;

    public UnknownRequestHandler(ILogger<UnknownRequestHandler> logger)
    {
        _logger = logger;
    }

    public Task UnknownUpdateHandlerAsync(Update update)
    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }
}