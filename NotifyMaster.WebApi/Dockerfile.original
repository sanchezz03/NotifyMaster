# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
ENV DOTNET_URLS=http://+:5000

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NotifyMaster.WebApi/NotifyMaster.WebApi.csproj", "NotifyMaster.WebApi/"]
COPY ["NotifyMaster.Application/NotifyMaster.Application.csproj", "NotifyMaster.Application/"]
COPY ["NotifyMaster.Common/NotifyMaster.Common.csproj", "NotifyMaster.Common/"]
COPY ["NotifyMaster.Core/NotifyMaster.Core.csproj", "NotifyMaster.Core/"]
COPY ["NotifyMaster.Infrastructure/NotifyMaster.Infrastructure.csproj", "NotifyMaster.Infrastructure/"]
RUN dotnet restore "./NotifyMaster.WebApi/NotifyMaster.WebApi.csproj"
COPY . .
WORKDIR "/src/NotifyMaster.WebApi"
RUN dotnet build "./NotifyMaster.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NotifyMaster.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotifyMaster.WebApi.dll"]