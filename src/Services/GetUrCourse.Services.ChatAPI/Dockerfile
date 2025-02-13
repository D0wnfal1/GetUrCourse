FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GetUrCourse.Services.ChatAPI/GetUrCourse.Services.ChatAPI.csproj", "GetUrCourse.Services.ChatAPI/"]
RUN dotnet restore "GetUrCourse.Services.ChatAPI/GetUrCourse.Services.ChatAPI.csproj"
COPY . .
WORKDIR "/src/GetUrCourse.Services.ChatAPI"
RUN dotnet build "GetUrCourse.Services.ChatAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GetUrCourse.Services.ChatAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GetUrCourse.Services.ChatAPI.dll"]
