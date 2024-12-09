using Microsoft.Extensions.Logging;
using NotifyMaster.Application.Handlers.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace NotifyMaster.Application.Handlers;

public class BotRequestHandler : IUpdateHandler
{
    private readonly IMessageHandler _messageHandler;
    private readonly ICallbackQueryHandler _callbackQueryHandler;
    private readonly IUnknownRequestHandler _unknownRequestHandler;
    private readonly ILogger<BotRequestHandler> _logger;

    public BotRequestHandler(IMessageHandler messageHandler, ICallbackQueryHandler callbackQueryHandler,
        IUnknownRequestHandler unknownRequestHandler, ILogger<BotRequestHandler> logger)
    {
        _messageHandler = messageHandler;
        _callbackQueryHandler = callbackQueryHandler;
        _unknownRequestHandler = unknownRequestHandler;
        _logger = logger;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        switch (update)
        {
            case { Message: { } message }:
                await _messageHandler.HandleMessageAsync(bot, update.Message, cancellationToken);
                break;

            case { EditedMessage: { } message }:
                await _messageHandler.HandleMessageAsync(bot, update.Message, cancellationToken);
                break;

            case { CallbackQuery: { } callbackQuery }:
                await _callbackQueryHandler.HandleCallbackQueryAsync(bot, update.CallbackQuery, cancellationToken);
                break;

            default:
                await _unknownRequestHandler.UnknownUpdateHandlerAsync(update);
                break;
        }
    }

    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HandleError: {Exception}", exception);

        if (exception is RequestException)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }
    }
}