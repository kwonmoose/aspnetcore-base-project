var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CDN에서 http를 https로 리디렉션하기 때문에 주석 처리
// app.UseHttpsRedirection();

// 인증 미들웨어 - 사용자를 인증하는 미들웨어
// https://docs.microsoft.com/ko-kr/aspnet/core/security/authentication/?view=aspnetcore-6.0
app.UseAuthentication();

// 권한 미들웨어 - 사용자에게 요청에 대해 권한이 있는지 확인하는 미들웨어
// https://docs.microsoft.com/ko-kr/aspnet/core/security/authorization/introduction?view=aspnetcore-6.0
app.UseAuthorization();

app.MapControllers();

app.Run();