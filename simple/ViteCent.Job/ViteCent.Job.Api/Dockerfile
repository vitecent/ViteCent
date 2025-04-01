# 请参阅 https://aka.ms/customizecontainer 以了解如何自定义调试容器，以及 Visual Studio 如何使用此 Dockerfile 生成映像以更快地进行调试。

# 此阶段用于在快速模式(默认为调试配置)下从 VS 运行时
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# 此阶段用于生成服务项目
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ViteCent.Job.Api/ViteCent.Job.Api.csproj", "src/ViteCent.Job.Api/"]
COPY ["src/ViteCent.Job.Application/ViteCent.Job.Application.csproj", "src/ViteCent.Job.Application/"]
COPY ["src/ViteCent.Job.Domain/ViteCent.Job.Doaman.csproj", "src/ViteCent.Job.Domain/"]
COPY ["src/ViteCent.Job.Entity/ViteCent.Job.Entity.csproj", "src/ViteCent.Job.Entity/"]
RUN dotnet restore "./src/ViteCent.Job.Api/ViteCent.Job.Api.csproj"
COPY . .
WORKDIR "/src/src/ViteCent.Job.Api"
RUN dotnet build "./ViteCent.Job.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 此阶段用于发布要复制到最终阶段的服务项目
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ViteCent.Job.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 此阶段在生产中使用，或在常规模式下从 VS 运行时使用(在不使用调试配置时为默认值)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ViteCent.Job.Api.dll"]