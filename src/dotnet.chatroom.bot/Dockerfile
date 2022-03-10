FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./dotnet.chatroom.bot/Dotnet.Chatroom.Bot/Dotnet.Chatroom.Bot.csproj", "Dotnet.Chatroom.Bot/"]
RUN dotnet restore "./Dotnet.Chatroom.Bot/Dotnet.Chatroom.Bot.csproj"
COPY ["./dotnet.chatroom.bot", "."]
WORKDIR "/src/Dotnet.Chatroom.Bot"
RUN dotnet build "Dotnet.Chatroom.Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet.Chatroom.Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet.Chatroom.Bot.dll"]