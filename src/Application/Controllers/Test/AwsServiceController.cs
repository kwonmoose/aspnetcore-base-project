using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Extensions.Caching;
using Microsoft.AspNetCore.Mvc;

namespace application.Controllers;

/// <summary>
/// AWS Service 테스트 API 컨트롤러
/// </summary>
[ApiController]
[Route("test/aws-service")]
public class AwsServiceController : ControllerBase
{
    // Log
    private readonly ILogger<AwsServiceController> _logger;

    private readonly IAmazonSecretsManager _amazonSecretsManager;

    public AwsServiceController(ILogger<AwsServiceController> logger, IAmazonSecretsManager amazonSecretsManager)
    {
        _logger = logger;

        _amazonSecretsManager = amazonSecretsManager;
    }
    
    [HttpGet("secrets-manager")]
    public async Task<IActionResult> GetSecretsManager()
    {
        SecretCacheConfiguration cacheConfiguration = new SecretCacheConfiguration
        {
            CacheItemTTL = 20000, // 20 seconds
            VersionStage = "AWSCURRENT",
        };

        SecretsManagerCache cache = new SecretsManagerCache(_amazonSecretsManager, cacheConfiguration);

        try
        {
            var value = await cache.GetSecretString("kwonmoose/local/aspnetcore-base-project");

            Console.WriteLine(value);
        }
        catch (Exception ex)
        {
    
        }

        // string secretName = "kwonmoose/local/db/accounts";
        // string region = "ap-northeast-2";
        //
        // IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
        //
        // GetSecretValueRequest request = new GetSecretValueRequest
        // {
        //     SecretId = secretName,
        //     VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
        // };
        //
        // GetSecretValueResponse response;
        //
        // try
        // {
        //     response = await client.GetSecretValueAsync(request);
        // }
        // catch (Exception e)
        // {
        //     // For a list of the exceptions thrown, see
        //     // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
        //     throw e;
        // }
        //
        // string secret = response.SecretString;



        return Ok();
    }
}