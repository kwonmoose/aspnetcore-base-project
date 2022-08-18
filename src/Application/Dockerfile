FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

# 포트 설정
EXPOSE 5000

#=======================================================================================================================

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

# 프로젝트 파일 복사
COPY ./src/application/application.csproj ./application/

# 프로젝트 복원
RUN dotnet restore "/src/application/application.csproj"

# 소스 복사
#COPY . .
COPY ./src/application/ ./application/

# 프로젝트 경로 이동
WORKDIR /src/application

# 프로젝트 빌드
RUN dotnet build -c Release -o /app/build

#=======================================================================================================================

FROM build AS publish

# 프로젝트 게시
RUN dotnet publish -c Release -o /app/publish

#=======================================================================================================================

FROM base AS final

WORKDIR /app

# 프로젝트 게시 결과 복사
COPY --from=publish /app/publish .

# Application 시작
ENTRYPOINT ["dotnet", "application.dll"]
