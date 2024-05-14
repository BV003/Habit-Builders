<script setup>
import { ref } from 'vue';

    const tel = ref('');
    const text = ref('');
    const digit = ref('');
    const number = ref('');
    const password = ref('');

</script>

<template>
  <van-button type="primary" size="large">主要按钮</van-button>
<van-button type="success" size="large">成功按钮</van-button>
<van-button type="default" size="large">默认按钮</van-button>
<van-button type="danger" size="large">危险按钮</van-button>
<van-button type="warning" size="large">警告按钮</van-button>
<van-cell-group inset>
  <!-- 输入任意文本 -->
  <van-field v-model="text" label="文本" />
  <!-- 输入手机号，调起手机号键盘 -->
  <van-field v-model="tel" type="tel" label="手机号" />
  <!-- 允许输入正整数，调起纯数字键盘 -->
  <van-field v-model="digit" type="digit" label="整数" />
  <!-- 允许输入数字，调起带符号的纯数字键盘 -->
  <van-field v-model="number" type="number" label="数字" />
  <!-- 输入密码 -->
  <van-field v-model="password" type="password" label="密码" />
</van-cell-group>

</template>

<style scoped>
</style>
