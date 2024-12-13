using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotifyMaster.Common.ConfigurationModels;

namespace NotifyMaster.Common.ConfigurationOptions;

public class DatabaseConfigurationOption : IConfigureOptions<DatabaseConfiguration>
{
    private readonly IConfigurationSection _configurationSection;

    public DatabaseConfigurationOption(IConfiguration configuration)
    {
        _configurationSection = configuration.GetSection(Constants.CONFIGURATION_SECTION_DATABASE);
    }

    public void Configure(DatabaseConfiguration options)
    {
        options.ConnectionString = _configurationSection?
            .GetValue<string>(nameof(DatabaseConfiguration.ConnectionString)) ??
            string.Empty;
    }
}
