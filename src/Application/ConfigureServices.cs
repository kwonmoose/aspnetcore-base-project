
using System.Reflection;
using System.Text.Json;
using Amazon.SecretsManager;
using application.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Context;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                // To preserve the default behavior, capture the original delegate to call later.
                var builtInFactory = options.InvalidModelStateResponseFactory;

                options.InvalidModelStateResponseFactory = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                    using (LogContext.PushProperty("LogProperty", new
                           {
                                modelState = JsonSerializer.Serialize(context.ModelState)
                           }))
                    {
                        logger.LogError("모델 바인딩 및 유효성 검사 중 오류 발생");
                    }
                    
                    // Perform logging here.
                    // ...

                    // Invoke the default behavior, which produces a ValidationProblemDetails response.
                    // To produce a custom response, return a different implementation of IActionResult instead.
                    return builtInFactory(context);
                };
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        #region 상태 검사 서비스 추가

        services.AddHealthChecks()
            .AddCheck<TagsHealthCheck>(
                "TagsHealthCheck",
                // failureStatus: HealthStatus.Degraded,
                tags: new[] { "tags" }
            )
            .AddCheck<SampleHealthCheck>(
                "SampleHealthCheck",
                tags: new[] { "sample" }
            )
            .AddCheck(
                "Sample", 
                () =>
                {
                    Console.WriteLine($"LambdaHealthCheck - {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
                    return HealthCheckResult.Healthy("A lambda healthy result");
                }, 
                tags: new [] {"lambda"}
            );

        #endregion

        #region AWS Setting

        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        
        // if (serverEnvironment.IsLocal())
        // {
        //     services.AddSingleton<IAmazonSQS>(p =>
        //     {
        //         return new AmazonSQSClient(new AmazonSQSConfig()
        //         {
        //             ServiceURL = $"{configuration["AWSServiceConfiguration:Local:Ip"]}:{configuration["AWSServiceConfiguration:Local:Port:SQS"]}",
        //             UseHttp = true
        //         });
        //     });
        // }
        // else
        // {
        services.AddAWSService<IAmazonSecretsManager>();
        // }

        #endregion
        
        #region Serilog 설정

        Log.Logger = new LoggerConfiguration()
            .Enrich.WithProperty("Application", Assembly.GetEntryAssembly().GetName().Name)
            .Enrich.WithProperty("Version", Assembly.GetEntryAssembly().GetName().Version)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        #endregion

        
        return services;
    }
}