<template>
    <van-nav-bar title="睡眠评测" left-text="返回" left-arrow @click-left="onClickLeft"/>

    <div>
        <van-picker-group
        cancel-button-text=""
        confirm-button-text=""
        title=""
        :tabs="['入睡时间', '起床时间']"
        
        >
            <van-time-picker v-model="startTime" />
            <van-time-picker v-model="endTime" />
        </van-picker-group>
    </div>

    <div class="button-container">
    <van-button plain round type="default" @click=postsleep>计算睡眠分数</van-button>
  </div>

  <div class="show-container">
    <van-cell title="睡眠得分"  icon="closed-eye" inset="true" size="large" >
        <p style="margin:0" :style={color:bindcolor}>{{score}}</p>
    </van-cell>
  </div>

</template>
  
  <script>

import { ref,onMounted } from 'vue';
import { showToast } from 'vant';
import { http } from '../../http';

  export default {
    setup() {
    const startTime = ref(['12', '00']);
    const endTime = ref(['12', '00']);

    const onConfirm = () => {
      showToast(`${startTime.value.join(':')} ${endTime.value.join(':')},添加成功`);
    };

    

    const onClickLeft = () => history.back();

    const score = ref(null);
    // 从localStorage获取userid，如果不存在则定义一个默认值
    let userid = "1";
    // 定义一个方法来获取今天的分数
    const getsleep = async () => {
      const url = '/accessment/getsleep';
      try {
        // 调用后端API
        const response = await http.get(url,{ params: { userid } });
        // 将返回的分数设置到score属性中
        score.value = response.data;
        if(score.value==0)
        {
          score.value="请先完成睡眠评测";
        }
      } catch (error) {
        console.error('获取分数失败:', error);
        console.log(response.data);
        score.value = 0; // 假设0表示获取分数失败
      }
    };

    const postsleep = async () => {
      const url = '/accessment/postsleep';
      try {
        // 调用后端API
        const response = await http.post(url,{ 
          userid: '2',
          StartHour:startTime.value[0],
          StartMinute:startTime.value[1],
          EndHour:endTime.value[0],
          EndMinute:endTime.value[1]
        }
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

    // 页面挂载时调用getTodayScore方法
    onMounted(getsleep);


    return {
      onClickLeft,
      endTime,
      startTime,
      onConfirm,
      score,
      getsleep,
      postsleep
    };
  },
  
    data() {
      return {
      };
    },
    methods: {
       
    }
  };
  </script>
  
  <style scoped>
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
  </style>