import axios from "axios";

const http = axios.create({
    baseURL : "http://127.0.0.1:8888",
    timeout : 5000,
    headers: {
    'Accept': 'application/json',
    'Content-Type': 'application/json'
    },
  });
  const get = (url,data) => {
  http.get(url,data)
    .then(function (response) {
      const data = response.data;
      console.log(data);
    })
    .catch(function (error) {
      console.log("error");
    })
    .finally(function () {
      // 总是会执行
    });
};
const post = (url,data) => {
  http.post(url,data)
    .then(function (response) {
      const data = response.data;
      console.log(data);
    })
    .catch(function (error) {
      console.log("error");
    })
    .finally(function () {
      // 总是会执行
    });
};
