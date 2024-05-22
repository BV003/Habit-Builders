<template>

    <van-nav-bar title="健康评测" />

   <div class="icon-container">
    <router-link to="/accessment/sleep">
    <van-icon name="closed-eye" class="icon" />
    </router-link>
    <div class="text">
        睡眠数据评估
    </div>
  </div>

  <div class="icon-container">
    <router-link to="/accessment/sport">
    <van-icon name="fire-o" class="icon" />
    </router-link>
    <div class="text">
        运动数据评估
    </div>
  </div>

  <div class="icon-container">
    <router-link to="/accessment/diet">
    <van-icon name="flower-o" class="icon" />
    </router-link>
    <div class="text">
        饮食数据评估
    </div>
  </div>

  
  <div class="show-container">
    <van-cell title="健康评测总分"  icon="smile-o" :inset="true" size="large" >
        <p style="margin:0" >{{score}}</p>
    </van-cell>
  </div>

  <div class="button-container">
    <van-button plain round type="default" @click=posttoday>获取今日结果</van-button>
  </div>

  <div class="button-container">
    <van-button plain round type="default" to="/accessment/history">查看历史记录</van-button>
  </div>
<tabbar/>
</template>
  
  <script setup>
import tabbar from "../../components/tabbar.vue";
import { http } from '../../http/index.js';
import { ref,onMounted } from 'vue';

    // 创建一个响应式属性来存储分数
    const score = ref(null);
    // 从localStorage获取userid，如果不存在则定义一个默认值
    let userid = "1";
    // 定义一个方法来获取今天的分数
    const gettoday = async () => {
      const url = '/accessment/gettoday';
      try {
        // 调用后端API
        const response = await http.get(url,{ params: { userid } });
        // 将返回的分数设置到score属性中
        score.value = response.data;
        if(score.value==0)
        {
          score.value="请先完成睡眠，运动，饮食评测";
        }
      } catch (error) {
        console.error('获取分数失败:', error);
        console.log(response.data);
        score.value = 0; // 假设0表示获取分数失败
      }
    };

    // 页面挂载时调用getTodayScore方法
    onMounted(gettoday);

    const posttoday = async () => {
      const url = '/accessment/posttoday';
      try {
        // 调用后端API
        const response = await http.post(url,{ userid: '2' }
      );
        // 将返回的分数设置到score属性中
        score.value = response.data;
        if(score.value==0)
        {
          score.value="请先完成睡眠，运动，饮食评测";
        }
      } catch (error) {
        console.error('获取分数失败:', error);
        console.log(error.response.data);
        score.value = 0; // 假设0表示获取分数失败
      }
    };
  </script>


  <style scoped>

.icon-container {
border: 3px solid #000; /* 1像素宽的实心黑色边框 */
margin-bottom: 10px; /* 在div下方添加20px的间隔 */
display: flex;
flex-direction: column;
align-items: center;
  /* 如果需要可以指定高度 */
height: 100px; /* 设置更大的高度 */
width: 80%; /* 或者固定像素值 */
  margin-left: auto;
  margin-right: auto;
justify-content: center; /* 垂直居中 */
}

.show-container {
margin-bottom: 10px; /* 在div下方添加20px的间隔 */
display: flex;
flex-direction: column;
align-items: center;
  /* 如果需要可以指定高度 */
height: 100px; /* 设置更大的高度 */
width: 80%; /* 或者固定像素值 */
  margin-left: auto;
  margin-right: auto;
justify-content: center; /* 垂直居中 */
}

.button-container {
margin-bottom: 10px; /* 在div下方添加20px的间隔 */
display: flex;
flex-direction: column;
align-items: center;
  /* 如果需要可以指定高度 */
height: 100px; /* 设置更大的高度 */
width: 80%; /* 或者固定像素值 */
  margin-left: auto;
  margin-right: auto;
justify-content: center; /* 垂直居中 */
}

.icon {
font-size: 50px; /* 根据需要调整图标大小 */
margin-bottom: 8px; /* 根据需要调整图标与文字的间距 */
}

.text {
  margin-top: 1px; /* 调整文字与图标的间距 */
  text-align: center; /* 文本居中对齐 */
  /* 可以根据需要添加更多样式，比如字体大小、颜色等 */
  justify-content: flex-start; /* 垂直居中 */
  font-size: 14px; /* 调整文本的字体大小 */
}
  </style>