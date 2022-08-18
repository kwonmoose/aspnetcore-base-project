using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace application.HealthChecks;

public class SampleHealthCheck: IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = true;
        
        Console.WriteLine($"SampleHealthCheck - {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

        if (isHealthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("A sample healthy result"));
        }

        return Task.FromResult(
            new HealthCheckResult(context.Registration.FailureStatus, "An sample unhealthy result.")
        );
    }
}