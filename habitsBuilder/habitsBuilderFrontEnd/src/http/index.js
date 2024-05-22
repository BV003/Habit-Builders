import axios from "axios";
let baseURL = '/api'        //关键代码

export const http = axios.create({
    baseURL ,
    timeout : 5000,
    headers: {
    'Accept': 'application/json',
    'Content-Type': 'application/json'
    },
  });

  
export const post = (url,data) => {
  http.post(url,data)
    .then(function (response) {
      const data = response.data;
      console.log(data);
    })
    .catch(function (error) {
      console.log("error");
    })
    .finally(function () {
      
    });
};

export const get = (url,data) => {
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