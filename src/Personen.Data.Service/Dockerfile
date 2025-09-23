# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
# Disable config change monitoring for better container performance
# See https://github.com/Contrast-Security-OSS/agent-operator/blob/master/docs/public/08-tips.md
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false
WORKDIR /app

# This stage is used to build the service project
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
WORKDIR /src

COPY ["Personen.Data.Service/Personen.Data.Service.csproj", "Personen.Data.Service/"]
COPY ["Rvig.Base.App/Rvig.Base.App.csproj", "Rvig.Base.App/"]
COPY ["Brp.Shared.Infrastructure/Brp.Shared.Infrastructure.csproj", "Brp.Shared.Infrastructure/"]
COPY ["Serilog.Sinks.PersistentFile/Serilog.Sinks.PersistentFile.csproj", "Serilog.Sinks.PersistentFile/"]
COPY ["Rvig.Data.Base.WebApi/Rvig.Data.Base.WebApi.csproj", "Rvig.Data.Base.WebApi/"]
COPY ["Rvig.Data.Base/Rvig.Data.Base.csproj", "Rvig.Data.Base/"]
COPY ["Rvig.HaalCentraalApi.Shared/Rvig.HaalCentraalApi.Shared.csproj", "Rvig.HaalCentraalApi.Shared/"]
COPY ["Rvig.Data.Base.Postgres/Rvig.Data.Base.Postgres.csproj", "Rvig.Data.Base.Postgres/"]
COPY ["Rvig.Data.Personen/Rvig.Data.Personen.csproj", "Rvig.Data.Personen/"]
COPY ["Rvig.HaalCentraalApi.Personen/Rvig.HaalCentraalApi.Personen.csproj", "Rvig.HaalCentraalApi.Personen/"]

RUN dotnet restore "./Personen.Data.Service/Personen.Data.Service.csproj"

COPY . .

WORKDIR "/src/Personen.Data.Service"
RUN dotnet build "./Personen.Data.Service.csproj" -c "$BUILD_CONFIGURATION" -o /app/build --arch "$TARGETARCH"

FROM --platform=$BUILDPLATFORM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
RUN dotnet publish "./Personen.Data.Service.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false --arch "$TARGETARCH"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN addgroup -g 1000 -S brp-api && \
    adduser -u 1000 -S -D -g brp-api brp-api-user && \
    chown -R brp-api-user:brp-api /app

# Set user for running the application
USER brp-api-user

ENTRYPOINT ["dotnet", "Personen.Data.Service.dll"]