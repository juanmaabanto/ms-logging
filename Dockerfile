FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /

COPY "MS-Logging.sln" "MS-Logging.sln"

COPY "src/Logging.Api/Logging.Api.csproj" "src/Logging.Api/Logging.Api.csproj"
COPY "src/Logging.Application/Logging.Application.csproj" "src/Logging.Application/Logging.Application.csproj"
COPY "src/Logging.Core/Logging.Core.csproj" "src/Logging.Core/Logging.Core.csproj"
COPY "tests/Logging.IntegrationTests/Logging.IntegrationTests.csproj" "tests/Logging.IntegrationTests/Logging.IntegrationTests.csproj"
COPY "tests/Logging.UnitTests/Logging.UnitTests.csproj" "tests/Logging.UnitTests/Logging.UnitTests.csproj"

RUN dotnet restore "MS-Logging.sln"

COPY src src
WORKDIR /src/Logging.Api
RUN dotnet publish --no-restore -c Release -o /app

FROM build as publish

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sofisoft.Enterprise.Logging.Api.dll"]