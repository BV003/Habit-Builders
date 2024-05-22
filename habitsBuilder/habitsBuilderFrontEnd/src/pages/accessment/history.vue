<template>
      <van-nav-bar title="" left-text="返回" left-arrow @click-left="onClickLeft"/>
    <van-calendar
  title="运动记录"
  :poppable="false"
  :show-confirm="false"
  :style="{ height: '700px' }"
  :min-date="minDate"
  :max-date="maxDate"
  :formatter="formatter"
/>

</template>
  
<script>
import { http } from '../../http/index.js';
import { ref,onMounted } from 'vue';

  export default {
    setup()
    {
      const onClickLeft = () => history.back();

      const scores = ref([]);
      const scoresMap=ref(new Map());
      
      let userid = "1";
      const gethistory = async () => {
      const url = '/accessment/gethistory';
      try {
        // 调用后端API
        const response = await http.get(url,{ params: { userid } });        
        response.data.forEach(item => {
      const dateObject = new Date(item.date);
      // 使用Date对象的getFullYear、getMonth、getDate方法来格式化日期
      const year = dateObject.getFullYear();
      const month = String(dateObject.getMonth() + 1).padStart(2, '0');
      const day = String(dateObject.getDate()).padStart(2, '0');
      // 创建格式化的日期字符串
      const formattedDate = `${year}-${month}-${day}`;
      // 将格式化的日期字符串和分数作为键值对添加到Map中
      scoresMap.value.set(formattedDate, item.score);
       });
      } catch (error) {
        console.error('获取分数失败:', error);
        scores.value = [{ date: new Date(), score: -1, message: '获取分数失败' }];
      }
    };

    // formatter函数，用于设置day对象的bottomInfo属性
    const formatter = (day) => {
        // 将day对象的date属性转换为"YYYY-MM-DD"格式的字符串
        const month = day.date.getMonth() + 1;
        const date = day.date.getDate();
        const year = day.date.getFullYear();
        const dateStr = `${year}-${month.toString().padStart(2, '0')}-${date.toString().padStart(2, '0')}`;
        // 使用转换后的日期字符串从scoresMap中获取分数
        const score = scoresMap.value.get(dateStr);
          // 如果找到了对应的分数，设置bottomInfo
          if (score !== undefined) {
            day.bottomInfo = `分数：${score}`;
          } else {
            day.bottomInfo = '未打卡'; // 如果没有找到分数，设置默认值
          }
          return day;
        };

    onMounted(gethistory);

    return {
      onClickLeft,
      gethistory,
      formatter,
      minDate: new Date(2024, 4, 1),
      maxDate: new Date(),
      scores,
    };    

    },

    data() {
      return {
      };
    }
  };
</script>
  
<style scoped>

</style>