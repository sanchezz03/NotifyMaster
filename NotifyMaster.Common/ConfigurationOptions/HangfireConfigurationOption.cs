using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotifyMaster.Common.ConfigurationModels;

namespace NotifyMaster.Common.ConfigurationOptions;

public class HangfireConfigurationOption : IConfigureOptions<HangfireConfiguration>
{
    private readonly IConfigurationSection _configurationSection;

    public HangfireConfigurationOption(IConfiguration configuration)
    {
        _configurationSection = configuration.GetSection(Constants.CONFIGURATION_SECTION_HANFIRE);
    }

    public void Configure(HangfireConfiguration options)
    {
        options.ConnectionString = _configurationSection?
            .GetValue<string>(nameof(HangfireConfiguration.ConnectionString)) ??
            string.Empty;
    }
}
