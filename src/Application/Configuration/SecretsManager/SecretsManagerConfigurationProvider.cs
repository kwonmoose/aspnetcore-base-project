using System.Text.Json;
using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;

namespace application.Configuration.SecretsManager;

public class SecretsManagerConfigurationProvider : ConfigurationProvider
{
    private readonly string _region;
    private readonly string _secretName;

    private CancellationTokenSource? _cancellationToken;
    
    public SecretsManagerConfigurationProvider(string region, string secretName)
    {
        _region = region;
        _secretName = secretName;
    }

    public override void Load()
    {
        LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();
    }
    
    private async Task LoadAsync()
    {
        var configuration = await GetConfigurationAsync(default).ConfigureAwait(false);

        Data = configuration.ToDictionary(p => p.Item1, p => p.Item2, StringComparer.InvariantCultureIgnoreCase);
    }

    private async Task<IReadOnlyList<SecretListEntry>> GetSecretsAsync(CancellationToken cancellationToken)
    {
        var response = default(ListSecretsResponse);
        var result = new List<SecretListEntry>();

        do
        {
            var nextToken = response?.NextToken;

            var filter = new List<Filter>();
            filter.Add(new Filter(){Key = "name", Values = new List<string> {"dev/aspnetcore-base-project/"}});

            var request = new ListSecretsRequest { NextToken = nextToken, Filters = filter};

            using (var client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(_region)))
            {
                response = await client.ListSecretsAsync(request, cancellationToken).ConfigureAwait(false);
                
                result.AddRange(response.SecretList);
            }
        } while (response.NextToken != null);

        return result;
    }
    
    private async Task<HashSet<(string, string)>> GetConfigurationAsync(CancellationToken cancellationToken)
    {
        var secrets = await GetSecretsAsync(cancellationToken).ConfigureAwait(false);
        var configuration = new HashSet<(string, string)>();

        foreach (var secret in secrets)
        {
            try
            {
                var request = new GetSecretValueRequest
                {
                    SecretId = secret.ARN
                };

                string secretString = default; 
                
                using (var client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(_region)))
                {
                    var response = await client.GetSecretValueAsync(request, cancellationToken).ConfigureAwait(false);

                    secretString = response.SecretString;
                }

                var secretName = secret.Name.Split("/").Last();

                if (TryParseJson(secretString, out var secretValue))
                {
                    var values = ExtractValues(secretValue.RootElement, secretName);

                    foreach (var (key, value) in values)
                    {
                        configuration.Add((key, value));
                    }
                }
                else
                {
                    configuration.Add((secretName, secretString));

                }
            }
            catch (ResourceNotFoundException ex)
            {
            }
        }

        return configuration;
    }

    private static bool TryParseJson(string data, out JsonDocument? jsonDocument)
    {
        jsonDocument = null;

        data = data.TrimStart();
        var firstChar = data.FirstOrDefault();

        if (firstChar != '[' && firstChar != '{')
        {
            return false;
        }

        try
        {
            jsonDocument = JsonDocument.Parse(data);

            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
    
    private static IEnumerable<(string key, string value)> ExtractValues(JsonElement jsonElement, string prefix)
    {
        switch (jsonElement.ValueKind)
        {
            case JsonValueKind.Array:
            {
                var array = jsonElement.EnumerateArray().ToArray();
                for (var i = 0; i < array.Count(); i++)
                {
                    var secretKey = $"{prefix}{ConfigurationPath.KeyDelimiter}{i}";
                    foreach (var (key, value) in ExtractValues(array[i], secretKey))
                    {
                        yield return (prefix, value);
                    }
                }
                
                break;
            }
            case JsonValueKind.Object:
            {
                foreach (var property in jsonElement.EnumerateObject())
                {
                    var secretKey = $"{prefix}{ConfigurationPath.KeyDelimiter}{property.Name}";

                    if (property.Value.ValueKind is JsonValueKind.Undefined)
                    {
                        foreach (var (key, value) in ExtractValues(property.Value, secretKey))
                        {
                            yield return (key, value);
                        }
                    }
                    else
                    {
                        var value = property.Value.ToString();
                        yield return (secretKey, value);
                    }
                }
                
                break;
            }
            case JsonValueKind.String:
            {
                var value = jsonElement.ToString();
                yield return (prefix, value);

                break;
            }
            default:
            {
                throw new FormatException("");
            }
        }
    }
}