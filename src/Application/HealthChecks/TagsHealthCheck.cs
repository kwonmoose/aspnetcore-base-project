using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace application.HealthChecks;

public class TagsHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = true;
        
        Console.WriteLine($"TagsHealthCheck - {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

        if (isHealthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("A tags healthy result"));
        }

        return Task.FromResult(
            new HealthCheckResult(context.Registration.FailureStatus, "An tags unhealthy result.")
        );
    }
}