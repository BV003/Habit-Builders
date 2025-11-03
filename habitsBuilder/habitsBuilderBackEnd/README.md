# BackEnd

## 设置docker
```
# 进入目录
cd ./habitsBuilderBackEnd

# 构建docker容器
docker build -t backend .

# 运行容器
docker run -p 5000:5000 backend


```
安装的依赖有点多，可不可以先进入一个docker容器里面，逐步运行，逐步看需要安装什么东西