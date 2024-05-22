<script >

import { http } from '../../http/index.js';
import { ref } from 'vue';


export default {
  setup(){
    const users = ref([]);

    const show = () => {
      console.log(users.value);
    };

    const test = async () => {
      const url = '/user'; // 替换为您的后端接口路径
      try {
        const response = await http.get(url);
        users.value = response.data; // 更新响应式数据
        console.log(response.data);
      } catch (error) {
        console.log("error");
      }
    };

    return {
      users,
      test,
      show
    };
  },

  data() {
    return {
      //users: []
    }},

  methods: {

  //   test() {
  //   const url = '/user'; // 替换为您的后端接口路径
  //   http.get(url, "")
  //     .then(function (response) { // 使用普通函数，这样这里的 this 就指向 Vue 实例
  //       const data = response.data;
  //       this.users = data; // 正确更新 this.users
  //       console.log(data);
  //     }.bind(this)) // 使用 .bind(this) 确保 this 指向 Vue 实例
  //     .catch(function (error) {
  //       console.log("error");
  //     })
  // },

    // show()
    // {
    //   console.log(this.users);
    // }


  }


};
</script>

<template>
  <van-button @click="test()">接收后端数据</van-button>
  <van-button @click="show()">查看user数组内容</van-button>

  <ul>
      <li v-for="user in users" :key="user.id">
        ID: {{ user.id }}, Name: {{ user.name }},Password:{{user.password}}
    </li>
  </ul>

</template>

<style scoped>


</style>