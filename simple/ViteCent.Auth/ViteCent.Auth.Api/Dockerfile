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
COPY ["src/ViteCent.Auth/ViteCent.Auth.Api/ViteCent.Auth.Api.csproj", "src/ViteCent.Auth/ViteCent.Auth.Api/"]
COPY ["src/ViteCent.Data/ViteCent.Auth.Data/ViteCent.Auth.Data.csproj", "src/ViteCent.Data/ViteCent.Auth.Data/"]
COPY ["src/ViteCent.Auth/ViteCent.Auth.Application/ViteCent.Auth.Application.csproj", "src/ViteCent.Auth/ViteCent.Auth.Application/"]
COPY ["src/ViteCent.Auth/ViteCent.Auth.Domain/ViteCent.Auth.Domain.csproj", "src/ViteCent.Auth/ViteCent.Auth.Domain/"]
COPY ["src/ViteCent.Auth/ViteCent.Auth.Entity/ViteCent.Auth.Entity.csproj", "src/ViteCent.Auth/ViteCent.Auth.Entity/"]
RUN dotnet restore "./src/ViteCent.Auth/ViteCent.Auth.Api/ViteCent.Auth.Api.csproj"
COPY . .
WORKDIR "/src/src/ViteCent.Auth/ViteCent.Auth.Api"
RUN dotnet build "./ViteCent.Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 此阶段用于发布要复制到最终阶段的服务项目
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ViteCent.Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 此阶段在生产中使用，或在常规模式下从 VS 运行时使用(在不使用调试配置时为默认值)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ViteCent.Auth.Api.dll"]