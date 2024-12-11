using Microsoft.Extensions.DependencyInjection;
using NotifyMaster.Application.DataProviders;
using NotifyMaster.Application.DataProviders.Intefaces;

namespace NotifyMaster.Application.Extensions;

public static class DataProvidersServiceExtension
{
    public static IServiceCollection AddDataProviders(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserDataProvider, UserDataProvider>()
            .AddScoped<IUserReminderDataProvider, UserReminderDataProvider>()
            .AddScoped<IMessageReminderDataProvider, MessageReminderDataProvider>();
    }
}
