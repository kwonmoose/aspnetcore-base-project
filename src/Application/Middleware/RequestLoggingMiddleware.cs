using System.Diagnostics;
using Serilog.Context;

namespace application.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    
    private readonly IServiceProvider _serviceProvider;

    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, IServiceProvider serviceProvider, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;

        _serviceProvider = serviceProvider;

        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = new Stopwatch();
        long requestTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        
        try
        {
            watch.Start();
        }
        finally
        {
            context.Response.OnStarting(() =>
            {
                string domain = $"{context.Request.Scheme}://{context.Request.Host.Host}";
                string requestMethod = context.Request.Method;
                string requestPath = context.Request.Path.Value ?? "";
                int statusCode = context.Response.StatusCode;
                long elapsedTime = 0;
                long responseTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                watch.Stop();

                elapsedTime = watch.ElapsedMilliseconds;

                using (LogContext.PushProperty("LogProperty", new
                       {
                           domain = domain,
                           requestMethod = requestMethod,
                           requestPath = requestPath,
                           statusCode = statusCode,
                           elapsedTime = elapsedTime,
                           requestTime = requestTime,
                           responseTime = responseTime
                       }))
                {
                    _logger.LogWarning("Http Request Logging");
                }
                
                return Task.CompletedTask;
            });  
            
            await _next(context);
        }
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}