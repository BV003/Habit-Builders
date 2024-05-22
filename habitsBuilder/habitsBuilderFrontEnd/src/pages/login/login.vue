<template>
    <div class="login-container" :style="{ height: `${height}px`,width : `${width}px`}">
      <van-image
          fit="cover"
          round
          width="5rem"
          height="5rem"
          src="https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg"
          class="imageicon"
/>
      <van-form @submit="onSubmit">
        
  <van-cell-group inset>
    <van-field
      v-model="username"
      name="用户名"
      label="用户名"
      placeholder="用户名"
      :rules="[{ required: true, message: '请填写用户名' }]"
    />
    <van-field
      v-model="password"
      type="password"
      name="密码"
      label="密码"
      placeholder="密码"
      :rules="[{ required: true, message: '请填写密码' }]"
    />
  </van-cell-group>
  <div style="margin: 16px;">
    <van-space direction="vertical" fill>
      <van-button round block type="primary" native-type="submit">登录</van-button>
      <van-button round block type="primary" @click="signup">注册</van-button>
    </van-space>
  </div>
</van-form>
    </div>
  </template>
  
  <script setup>
  import { useWindowSize } from '@vant/use';
  import { ref ,watch} from 'vue';
  import { state } from '../../state/state.js';
  import { useRouter } from 'vue-router';
  
  const username = ref('');
  const password = ref('');
  const router = useRouter();

  const onSubmit = (values) => {
      console.log('submit', values);
      login();
    }

    const signup = () => {
      console.log('signup');

    }
  
  const login = () => {
    // 模拟一个登录请求
    const user = {
      username: username.value,
      password: password.value,
    };
    state.setUser(user);
    router.push('/');
  };

  const { width, height } = useWindowSize();
  </script>

<style scoped>
.login-container {

  background-color: #f8f8f8;
  display: flex;
  flex-direction: column;
  justify-content: center; /* 水平居中 */
  align-items: center; /* 垂直居中 */
}
.imageicon{
  margin-bottom: 40px;
}
</style>
  