using application.Configuration.SecretsManager;

namespace Microsoft.Extensions.Configuration;

public static class SecretsManagerExtensions
{
    public static IConfigurationBuilder AddSecretsManager(this IConfigurationBuilder configurationBuilder, string region, string secretName)
    {
        var configurationSource = new SecretsManagerConfigurationSource(region, secretName);

        configurationBuilder.Add(configurationSource);

        return configurationBuilder;
    }
}