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
COPY ["src/ViteCent.Basic/ViteCent.Basic.Api/ViteCent.Basic.Api.csproj", "src/ViteCent.Basic/ViteCent.Basic.Api/"]
COPY ["src/ViteCent.Data/ViteCent.Basic.Data/ViteCent.Basic.Data.csproj", "src/ViteCent.Data/ViteCent.Basic.Data/"]
COPY ["src/ViteCent.Basic/ViteCent.Basic.Application/ViteCent.Basic.Application.csproj", "src/ViteCent.Basic/ViteCent.Basic.Application/"]
COPY ["src/ViteCent.Basic/ViteCent.Basic.Domain/ViteCent.Basic.Domain.csproj", "src/ViteCent.Basic/ViteCent.Basic.Domain/"]
COPY ["src/ViteCent.Basic/ViteCent.Basic.Entity/ViteCent.Basic.Entity.csproj", "src/ViteCent.Basic/ViteCent.Basic.Entity/"]
RUN dotnet restore "./src/ViteCent.Basic/ViteCent.Basic.Api/ViteCent.Basic.Api.csproj"
COPY . .
WORKDIR "/src/src/ViteCent.Basic/ViteCent.Basic.Api"
RUN dotnet build "./ViteCent.Basic.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 此阶段用于发布要复制到最终阶段的服务项目
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ViteCent.Basic.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 此阶段在生产中使用，或在常规模式下从 VS 运行时使用(在不使用调试配置时为默认值)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ViteCent.Basic.Api.dll"]