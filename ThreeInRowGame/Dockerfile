FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ThreeInRowGame/ThreeInRowGame.csproj", "ThreeInRowGame/"]
RUN dotnet restore "ThreeInRowGame/ThreeInRowGame.csproj"
COPY . .
WORKDIR "/src/ThreeInRowGame"
RUN dotnet build "ThreeInRowGame.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThreeInRowGame.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThreeInRowGame.dll"]
