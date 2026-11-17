# Build context is the repository root (Directory.Build.props, Directory.Packages.props, global.json):
#   docker build -f src/AwesomeGithubStats.Api/Dockerfile .

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /repo
COPY ["Directory.Build.props", "Directory.Packages.props", "global.json", "./"]
COPY ["src/AwesomeGithubStats.Api/AwesomeGithubStats.Api.csproj", "src/AwesomeGithubStats.Api/"]
COPY ["src/AwesomeGithubStats.Core/AwesomeGithubStats.Core.csproj", "src/AwesomeGithubStats.Core/"]
RUN dotnet restore "src/AwesomeGithubStats.Api/AwesomeGithubStats.Api.csproj"
COPY ["src/", "src/"]
WORKDIR "/repo/src/AwesomeGithubStats.Api"
RUN dotnet build "AwesomeGithubStats.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwesomeGithubStats.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwesomeGithubStats.Api.dll"]
