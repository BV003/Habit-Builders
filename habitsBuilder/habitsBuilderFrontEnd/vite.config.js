import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  // server:{
  //   host:"172.20.10.4",
  //   port:"5173",
  // },
  server: {
    // 其他服务器配置...
    proxy: {
      // 捕获所有以 /api 开头的请求
      '/api': {
        // 目标是后端服务器地址
        target: 'http://127.0.0.1:8888',
        // 重写请求路径，去掉 /api 前缀
        rewrite: (path) => path.replace('^/api/', ''),
        changeOrigin: true, // 改变原始请求的origin头为 target URL 的域名
        secure: false, // 如果是https的代理，需要设置secure为true
      },
      // 如果有更多代理规则，可以继续添加
    },
  },
})
