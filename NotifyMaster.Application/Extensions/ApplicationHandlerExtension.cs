using Microsoft.Extensions.DependencyInjection;
using NotifyMaster.Application.Handlers;
using NotifyMaster.Application.Handlers.Interfaces;
using Telegram.Bot.Polling;

namespace NotifyMaster.Application.Extensions;

public static class ApplicationHandlerExtension
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<ICallbackQueryHandler, CallbackQueryHandler>()
            .AddScoped<IUnknownRequestHandler, UnknownRequestHandler>()
            .AddScoped<IMessageHandler, MessageHandler>()
            .AddScoped<IUpdateHandler, BotRequestHandler>()

            .AddSingleton<ISendMessageHandler, SendMessageHandler>();
    }
}
