# 使用微软官方的.NET 9.0 ASP.NET运行时镜像作为基础镜像
# 该镜像包含了运行ASP.NET应用所需的最小运行时环境
# AS base 为这个阶段命名，方便后续引用
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

# 设置容器内的工作目录为/app
# 后续的COPY等命令的目标路径将相对于此目录
WORKDIR /app

# 将发布文件夹(./publish)中的所有内容复制到容器的当前工作目录
# 这里的.表示当前工作目录(/app)
COPY ./publish .

# 设置容器启动时要执行的命令
# 使用dotnet命令运行ViteCent.Auth.Api.dll
# ENTRYPOINT指令确保容器像可执行程序一样运行
ENTRYPOINT ["dotnet", "ViteCent.Auth.Api.dll"]