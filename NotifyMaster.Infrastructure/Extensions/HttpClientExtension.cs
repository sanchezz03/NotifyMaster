using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NotifyMaster.Common.ConfigurationModels;
using NotifyMaster.Common.ConfigurationOptions;
using Telegram.Bot;

namespace NotifyMaster.Infrastructure.Extensions;

public static class HttpClientExtension
{
    private const string HTTP_CLIENT_NAME = "tgwebhook";

    public static IServiceCollection AddTelegramBotClient(this IServiceCollection services)
    {
        services.ConfigureOptions();

        var configuration = services
            .BuildServiceProvider()
            .GetRequiredService(typeof(IOptions<BotConfiguration>)) as IOptions<BotConfiguration>;

        if (configuration == null || configuration.Value == null || configuration.Value.Token == null)
        {
            throw new Exception("Not found Telegram bot configuration in config file");
        }

        services.AddHttpClient(HTTP_CLIENT_NAME)
            .RemoveAllLoggers()
            .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(configuration.Value.Token, httpClient));

        return services;
    }

    #region Private methods

    private static IServiceCollection ConfigureOptions(this IServiceCollection services)
    {
        return services.AddTransient<IConfigureOptions<BotConfiguration>, BotConfigurationOption>();
    }

    #endregion
}
