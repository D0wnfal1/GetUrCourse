FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GetUrCourse.Services.TaskAPI/GetUrCourse.Services.TaskAPI.csproj", "GetUrCourse.Services.TaskAPI/"]
RUN dotnet restore "GetUrCourse.Services.TaskAPI/GetUrCourse.Services.TaskAPI.csproj"
COPY . .
WORKDIR "/src/GetUrCourse.Services.TaskAPI"
RUN dotnet build "GetUrCourse.Services.TaskAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GetUrCourse.Services.TaskAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GetUrCourse.Services.TaskAPI.dll"]
