# ===== 构建阶段 =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 复制解决方案文件和项目文件
COPY ../habitsBuilderBackEnd.sln ./
COPY habitsBuilderBackEnd/*.csproj ./habitsBuilderBackEnd/

# 如果有子项目，需要 COPY 子项目文件夹
# COPY ./SubProjectFolder ./SubProjectFolder

# 还原依赖
RUN dotnet restore

# 复制全部源代码
COPY habitsBuilderBackEnd/ ./habitsBuilderBackEnd

WORKDIR /src/habitsBuilderBackEnd

# 发布到 /app 目录
RUN dotnet publish -c Release -o /app

# ===== 运行阶段 =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# 复制发布好的文件
COPY --from=build /app .

# 暴露 API 端口（根据 Program.cs 配置）
EXPOSE 5000

# 启动应用
ENTRYPOINT ["dotnet", "habitsBuilderBackEnd.dll"]
