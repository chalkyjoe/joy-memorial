FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["joy-memorial/joy-memorial.csproj", "joy-memorial/"]
COPY ["joy-database/joy-database.csproj", "joy-database/"]
RUN dotnet restore "joy-memorial/joy-memorial.csproj"
COPY . .
WORKDIR "/src/joy-memorial"
RUN dotnet build "joy-memorial.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "joy-memorial.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "joy-memorial.dll"]
