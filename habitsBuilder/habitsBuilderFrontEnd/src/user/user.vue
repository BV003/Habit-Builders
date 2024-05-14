<script setup>
  import axios from "axios";



  const http = axios.create({
    baseURL: "http://127.0.0.1:8888",
    timeout: 3000,
  });
  const get = () => {
  http.get('/api/WeatherForecast')
    .then(function (response) {
      const weatherData = response.data;
      console.log(weatherData);
    })
    .catch(function (error) {
      console.log("error");
    })
    .finally(function () {
      // 总是会执行
    });
};
</script>

<template>
  <p>2</p>
  <button @click="get">123</button>

</template>

<style scoped>
</style>