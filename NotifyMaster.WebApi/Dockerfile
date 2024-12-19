FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
ENV DOTNET_URLS=http://+:5000

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

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NotifyMaster.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotifyMaster.WebApi.dll"]