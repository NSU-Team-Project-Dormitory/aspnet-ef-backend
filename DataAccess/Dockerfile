FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
RUN dotnet tool install --global dotnet-ef
RUN dotnet restore "DataAccess/DataAccess.csproj"
COPY . .
WORKDIR "/src/DataAccess"
RUN dotnet build "DataAccess.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataAccess.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS migrations
WORKDIR /app
COPY --from=publish /app/publish .
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "ef", "database", "update"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataAccess.dll"]
