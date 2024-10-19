FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 9001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StarboardSocial.UserService.Server/StarboardSocial.UserService.Server.csproj", "StarboardSocial.UserService.Server/"]
RUN dotnet restore "StarboardSocial.UserService.Server/StarboardSocial.UserService.Server.csproj"
COPY . .
WORKDIR "/src/StarboardSocial.UserService.Server"
RUN dotnet build "StarboardSocial.UserService.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StarboardSocial.UserService.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:9001
ENTRYPOINT ["dotnet", "StarboardSocial.UserService.Server.dll"]
