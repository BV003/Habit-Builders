<script setup>

import { ref } from 'vue';
const active = ref(0);

</script>



<template>
  
<van-tabbar v-model="active" active-color="#41b69c" inactive-color="#646464" route safe-area-inset-bottom>
  <van-tabbar-item to="/" icon="home-o">评分</van-tabbar-item>
  <van-tabbar-item to="/habit" icon="search">标签2</van-tabbar-item>
  <van-tabbar-item to="/comunity" icon="comment-o">社区</van-tabbar-item>
  <van-tabbar-item to="/user" icon="contact-o">我</van-tabbar-item>
</van-tabbar>

</template>

<style scoped>
</style>