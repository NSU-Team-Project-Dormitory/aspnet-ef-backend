# Установим базовый образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Скопируем и восстановим зависимости для DataAccess
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["WebApiApplication/WebApiApplication.csproj", "WebApiApplication/"]
RUN dotnet restore "WebApiApplication/WebApiApplication.csproj"

# Скопируем все файлы и постоим проект
COPY . .
WORKDIR "/src/WebApiApplication"
RUN dotnet build "WebApiApplication.csproj" -c Release -o /app/build

# Публикация проекта
FROM build AS publish
RUN dotnet publish "WebApiApplication.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Запуск в финальном образе
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
EXPOSE 80
EXPOSE 7180
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiApplication.dll"]
