import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    host: '0.0.0.0',
    port: 5173,
    proxy: {
      '/api': {
        // Use environment variable or default to localhost:5000 for Docker
        target: process.env.VITE_API_PROXY_URL || 'http://backend:5000',
        changeOrigin: true,
        secure: false,
      },
    },
  },
})
