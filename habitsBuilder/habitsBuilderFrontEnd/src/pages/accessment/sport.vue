<template>
    <van-nav-bar title="运动评测" left-text="返回" left-arrow @click-left="onClickLeft"/>

    <van-cell title="新增项目" is-link @click="showPopup" />
    <van-popup v-model:show="show"  position="bottom"
    :style="{ height: '70%' }">
    
        <van-picker
        title="新增运动项目"
        :columns="columns"
        @cancel="onCancel"
        @confirm="onConfirm"
        />

    </van-popup>    

    <van-cell-group inset>
        <van-cell v-for="(item, index) in items" :value="item.time" :title="item.title" 
        icon="close" label="点击删除"@click="deleteItem(index)"/>
    </van-cell-group>
    
    <div class="button-container">
    <van-button plain round type="default" @click=postsport>计算运动分数</van-button>
    </div>

    <div class="show-container">
    <van-cell title="运动得分"  icon="fire-o" inset="true" size="large" >
        <p style="margin:0" :style={color:bindcolor}>{{score}}</p>
    </van-cell>
    </div>

</template>
  
<script>
import { ref,onMounted } from 'vue';
import { http } from '../../http';


  export default {
    setup() {
    const onClickLeft = () => history.back();
    
    const items= ref([
        // { 
        //     title: '项目1',
        //     value:'12'
        //     ,time:'45'
        //  },
        //  { 
        //     title: '项目2',
        //     value:'12'
        //     ,time:'45'
        //  },
        //  { 
        //     title: '项目3',
        //     value:'12'
        //     ,time:'45'
        //  }
      ])

    const columns = [
      // 第一列
      //1为剧烈有氧，2没有那么剧烈，3和散步强度相同
      [
        { text: '篮球', value: '1篮球' },
        { text: '足球', value: '1足球' },
        { text: '网球', value: '2网球' },
        { text: '乒乓球', value: '2乒乓球' },
        { text: '排球', value: '2排球' },
        { text: '棒球', value: '2棒球' },
        { text: '橄榄球', value: '1橄榄球' },
        { text: '曲棍球', value: '2曲棍球' },
        { text: '高尔夫', value: '3高尔夫' },
        { text: '滑雪', value: '2滑雪' },
        { text: '滑板', value: '3滑板' },
        { text: '冲浪', value: '3冲浪' },
        { text: '游泳', value: '2游泳' },
        { text: '长跑', value: '1长跑' },
        { text: '体操', value: '2体操' },
        { text: '拳击', value: '1拳击' },
        { text: '跆拳道', value: '2跆拳道' },
        { text: '自行车', value: '1自行车' },
        { text: '舞蹈', value: '2舞蹈' },
        { text: '瑜伽', value: '3瑜伽' },
        { text: '健身', value: '1健身' },
        { text: '攀岩', value: '1攀岩' },
        { text: '潜水', value: '2潜水' }
      ],
      // 第二列
      [
        { text: '15分钟', value: '15' },
        { text: '30分钟', value: '30' },
        { text: '45分钟', value: '45' },
        { text: '1小时', value: '60' },
        { text: '1小时30分钟', value: '90' },
        { text: '2小时', value: '120' },
        { text: '2小时30分钟', value: '150' },
        { text: '3小时', value: '180' }
      ],
    ];

    const show = ref(false);
    const showPopup = () => {
      show.value = true;
    };
    const onCancel = () =>{
        show.value=false
    };

    const onConfirm = ({ selectedValues }) => {
        const stringValue = selectedValues[0];
        const b = selectedValues[1];
        const numberValue = stringValue.slice(0, 1);
        // 提取运动项目名称部分
        const sportName = stringValue.slice(1);

         let newItem = {
         title: sportName,
         value: numberValue,
         time: b 
         };
         items.value.push(newItem);
        show.value=false;
    }

    const deleteItem = (index) => {
      // 使用value来修改ref的值
      items.value.splice(index, 1);
    };

    const score = ref(null);
    // 从localStorage获取userid，如果不存在则定义一个默认值
    let userid = "1";
    // 定义一个方法来获取今天的分数
    const getsport = async () => {
      const url = '/accessment/getsport';
      try {
        // 调用后端API
        const response = await http.get(url,{ params: { userid } });
        // 将返回的分数设置到score属性中
        score.value = response.data;
        if(score.value==0)
        {
          score.value="请先完成运动评测";
        }
      } catch (error) {
        console.error('获取分数失败:', error);
        console.log(response.data);
        score.value = 0; // 假设0表示获取分数失败
      }
    };

    // 页面挂载时调用getTodayScore方法
    onMounted(getsport);

    const time1 = ref(0);
    const time2 = ref(0);
    const time3 = ref(0);
    const postsport = async () => {
      const url = '/accessment/postsport';

      items.value.forEach(item => {
      const value = parseInt(item.value, 10); // 将value从字符串转换为数字
      const time = parseInt(item.time, 10);  // 将gram从字符串转换为数字

      // 使用if-else语句更新对应的gram变量
      if (value === 1) {
        time1.value += time;
      } else if (value === 2) {
        time2.value += time;
      } else if (value === 3) {
        time3.value += time;
      } 
    });
      try {
        // 调用后端API
        const response = await http.post(url,{ 
          userid: '2',
          time1:time1.value,
          time2:time2.value,
          time3:time3.value,
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

    return{
        onClickLeft,
        show,
        showPopup,
        columns,
        onCancel,
        onConfirm,
        items,
        deleteItem,
        score,
        getsport,
        postsport
    }
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