using Microsoft.Extensions.DependencyInjection;
using NotifyMaster.Application.Services;
using NotifyMaster.Application.Services.Interfaces;

namespace NotifyMaster.Application.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IReminderService, ReminderService>();
    }
}