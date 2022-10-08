using System.Reflection;
using application.Codes;
using application.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (ServerEnvironmentCodes.IsLocal() || ServerEnvironmentCodes.IsDevelopment() || ServerEnvironmentCodes.IsQA())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Serilog로 HTTP Request 로깅 
app.UseSerilogRequestLogging();

// CDN에서 http를 https로 리디렉션하기 때문에 주석 처리
// app.UseHttpsRedirection();

// 인증 미들웨어 - 사용자를 인증하는 미들웨어
// https://docs.microsoft.com/ko-kr/aspnet/core/security/authentication/?view=aspnetcore-6.0
app.UseAuthentication();

// 권한 미들웨어 - 사용자에게 요청에 대해 권한이 있는지 확인하는 미들웨어
// https://docs.microsoft.com/ko-kr/aspnet/core/security/authorization/introduction?view=aspnetcore-6.0
app.UseAuthorization();

#region API 라우팅

app.MapControllers();

app.MapGet("/minimal-api/get", () => "Minimal API!!");

#endregion

#region 상태 검사 엔드포인트 지정

// 모든 상태 검사 서비스가 실행됨
app.MapHealthChecks("/health-check1"); 

// 상태 검사 서비스 등록할 때 tags에 "tags" 문자열을 추가한 서비스만 실행
app.MapHealthChecks("/health-check2", new HealthCheckOptions()
{
    Predicate = healthCheck => healthCheck.Tags.Contains("tags")
});

// 상태 검사 서비스 등록할 때 tags에 "sample" 문자열을 추가한 서비스만 실행
app.MapHealthChecks("/health-check3", new HealthCheckOptions()
{
    Predicate = healthCheck => healthCheck.Tags.Contains("sample")
});

// 상태 검사 서비스 등록할 때 tags에 "lambda" 문자열을 추가한 서비스만 실행
app.MapHealthChecks("/health-check4", new HealthCheckOptions()
{
    Predicate = healthCheck => healthCheck.Tags.Contains("lambda")
});

#endregion

app.Run();