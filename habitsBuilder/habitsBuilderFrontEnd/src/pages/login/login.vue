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
      v-model="userId"
      name="用户名"
      label="用户名"
      placeholder="用户名"
      :rules="[{ required: true, message: '请填写用户名' }]"
    />
    <van-field
      v-model="userName"
      v-if="isRegisterMode"
      name="昵称"
      label="昵称"
      placeholder="昵称"
      :rules="[{ required: isRegisterMode, message: '请填写昵称' }]"
    />
    <van-field
      v-model="password"
      type="password"
      name="密码"
      label="密码"
      placeholder="密码"
      :rules="passwordRules"
    />
    <van-field
      v-model="confirmPassword"
      v-if="isRegisterMode"
      type="password"
      name="确认密码"
      label="确认密码"
      placeholder="请再次输入密码"
      :rules="confirmPasswordRules"
    />
  </van-cell-group>
  <div style="margin: 16px;">
    <van-space direction="vertical" fill>
      <van-button round block type="primary" native-type="submit">
        {{ isRegisterMode ? '注册' : '登录' }}
      </van-button>
      <van-button round block type="default" @click="toggleMode" native-type="button">
        {{ isRegisterMode ? '已有账号？去登录' : '注册新账号' }}
      </van-button>
    </van-space>
  </div>
</van-form>
    </div>
  </template>
  
  <script setup>
  import { useWindowSize } from '@vant/use';
  import { ref ,watch, computed} from 'vue';
  import { state } from '../../state/state.js';
  import { useRouter } from 'vue-router';
  import {http} from '../../http'
  import { showLoadingToast, closeToast, showToast } from 'vant';
  
  const userId = ref('');
  const userName = ref('');
  const password = ref('');
  const confirmPassword = ref('');
  const isRegisterMode = ref(false);
  const router = useRouter();

  const passwordRules = computed(() => [
    { required: true, message: '请填写密码' },
    ...(isRegisterMode.value ? [{ validator: validatePasswordLength, message: '密码长度不能少于6位' }] : [])
  ]);

  const confirmPasswordRules = computed(() => [
    { required: true, message: '请确认密码' },
    { validator: validatePasswordMatch, message: '两次输入的密码不一致' }
  ]);

  function validatePasswordLength(value) {
    return value.length >= 6;
  }

  function validatePasswordMatch(value) {
    return value === password.value;
  }

  const toggleMode = () => {
    isRegisterMode.value = !isRegisterMode.value;
    // Clear fields when switching modes
    userId.value = '';
    userName.value = '';
    password.value = '';
    confirmPassword.value = '';
  };

  const onSubmit = async () => {
    if (isRegisterMode.value) {
      await handleRegister();
    } else {
      await handleLogin();
    }
  };

  const handleLogin = async () => {
    try {
      showLoadingToast({
        message: '登录中...',
        forbidClick: true,
      });
      const response = await http.post('/user/login', {
        userId: userId.value,
        password: password.value
      });
      
      if (response.data.success) {
        closeToast();
        console.log('登录成功');
        const user = {
          userId: userId.value,
          password: password.value,
        };
        state.setUser(user);
        router.push('/');
      } else {
        closeToast();
        showToast(response.data.message || '用户名或密码错误');
      }
    } catch (error) {
      closeToast();
      showToast('登录失败，请稍后重试');
      console.error(error);
    }
  };

  const handleRegister = async () => {
    try {
      showLoadingToast({
        message: '注册中...',
        forbidClick: true,
      });
      
      const response = await http.post('/user/register', {
        userId: userId.value,
        userName: userName.value,
        password: password.value
      });
      
      closeToast();
      
      if (response.data.success) {
        showToast('注册成功！请登录');
        // Switch to login mode after successful registration
        isRegisterMode.value = false;
        // Clear password fields but keep userId
        password.value = '';
        confirmPassword.value = '';
        userName.value = '';
      } else {
        showToast(response.data.message || '注册失败');
      }
    } catch (error) {
      closeToast();
      showToast('注册失败，请稍后重试');
      console.error(error);
    }
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
