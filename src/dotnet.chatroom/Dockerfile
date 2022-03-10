FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./dotnet.chatroom/Dotnet.Chatroom/Dotnet.Chatroom.csproj", "Dotnet.Chatroom/"]
RUN dotnet restore "./Dotnet.Chatroom/Dotnet.Chatroom.csproj"
COPY ["./dotnet.chatroom", "."]
WORKDIR "/src/Dotnet.Chatroom"
RUN dotnet build "Dotnet.Chatroom.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet.Chatroom.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet.Chatroom.dll"]