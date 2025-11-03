# 使用官方 Node 镜像
FROM node:20-alpine

# 设置工作目录
WORKDIR /app

# 先复制依赖文件，利用 Docker 缓存加快构建
COPY package.json package-lock.json ./

# 安装依赖
RUN npm ci

# 复制整个项目代码
COPY . .

# 启动开发服务器
CMD ["npm", "run", "dev", "--", "--host"]
