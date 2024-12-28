FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GetUrCourse.Services.CourseAPI/GetUrCourse.Services.CourseAPI.csproj", "GetUrCourse.Services.CourseAPI/"]
RUN dotnet restore "GetUrCourse.Services.CourseAPI/GetUrCourse.Services.CourseAPI.csproj"
COPY . .
WORKDIR "/src/GetUrCourse.Services.CourseAPI"
RUN dotnet build "GetUrCourse.Services.CourseAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GetUrCourse.Services.CourseAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GetUrCourse.Services.CourseAPI.dll"]
