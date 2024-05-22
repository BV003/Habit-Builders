<template>

    <van-nav-bar title="饮食评测" left-text="返回" left-arrow @click-left="onClickLeft"/>

    <div>
    <van-cell title="饮食" is-link @click="showPopup" />
    <van-popup v-model:show="show"  position="bottom"
    :style="{ height: '70%' }">
    
        <van-picker
        title="新增饮食项目"
        :columns="brecolumns"
        @cancel="onCancel"
        @confirm="onConfirm"
        />

    </van-popup>    

    <van-cell-group inset>
        <van-cell v-for="(item, index) in breitems" :value="item.gram" :title="item.title" 
        icon="close" label="点击删除"@click="deleteItem(index)"/>
    </van-cell-group>
    </div>

    <div class="button-container">
    <van-button plain round type="default" @click=postdiet>计算饮食分数</van-button>
    </div>

    <div class="show-container">
    <van-cell title="饮食得分"  icon="flower-o" inset="true" size="large" >
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
    
    const breitems= ref([
        // { 
        //     title: '项目1',
        //     value:'12'
        //     ,gram:'45'
        //  },
        //  { 
        //     title: '项目2',
        //     value:'12'
        //     ,gram:'45'
        //  },
        //  { 
        //     title: '项目3',
        //     value:'12'
        //     ,gram:'45'
        //  }
      ])

    const brecolumns = [
      // 第一列
      //1为碳水，2为肉类，3为蔬菜，4为水果，5为零食，6为水
      {
  text: "碳水",
  value: "1",
  children: [
    {
      text: "米饭",
      value: "米饭", // 第二层的 value 修改为与 text 相同
      children: [
        { text: "0碗", value: "0" },
        { text: "0.5碗", value: "125" },
        { text: "1碗", value: "250" },
        { text: "1.5碗", value: "375" },
        { text: "2碗", value: "500" },
      ],
    },
    {
      text: "面条",
      value: "面条", // 第二层的 value 修改为与 text 相同
      children: [
        { text: "0碗", value: "0" },
        { text: "0.5碗", value: "125" },
        { text: "1碗", value: "250" },
        { text: "1.5碗", value: "375" },
        { text: "2碗", value: "500" },
      ],
    },
    {
      text: "包子或馒头",
      value: "包子或馒头", // 第二层的 value 修改为与 text 相同
      children: [
        { text: "0", value: "0" },
        { text: "1个", value: "80" },
        { text: "2个", value: "160" },
        { text: "3个", value: "240" },
      ],
    },
  ],
},
{
  text: "肉类",
  value: "2",
  children: [
    {
      text: "猪肉",
      value: "猪肉", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "50克", value: "50" },
        { text: "100克", value: "100" },
        { text: "150克", value: "150" },
        { text: "200克", value: "200" }
      ]
    },
    {
      text: "牛肉",
      value: "牛肉", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "50克", value: "50" },
        { text: "100克", value: "100" },
        { text: "150克", value: "150" },
        { text: "200克", value: "200" }
      ]
    },
    {
      text: "鸡肉",
      value: "鸡肉", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "50克", value: "50" },
        { text: "100克", value: "100" },
        { text: "150克", value: "150" },
        { text: "200克", value: "200" }
      ]
    },
    {
      text: "鱼肉",
      value: "鱼肉", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "50克", value: "50" },
        { text: "100克", value: "100" },
        { text: "150克", value: "150" },
        { text: "200克", value: "200" }
      ]
    }
  ]
},
{
  text: "蔬菜类",
  value: "3",
  children: [
    {
      text: "菠菜",
      value: "菠菜", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "50克", value: "50" },
        { text: "100克", value: "100" },
        { text: "150克", value: "150" },
        { text: "200克", value: "200" }
      ]
    },
    {
      text: "胡萝卜",
      value: "胡萝卜", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "50克", value: "50" },
        { text: "100克", value: "100" },
        { text: "150克", value: "150" },
        { text: "200克", value: "200" }
      ]
    },
    {
      text: "西红柿",
      value: "西红柿", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "50克", value: "50" },
        { text: "100克", value: "100" },
        { text: "150克", value: "150" },
        { text: "200克", value: "200" }
      ]
    },
    {
      text: "黄瓜",
      value: "黄瓜", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "50克", value: "50" },
        { text: "100克", value: "100" },
        { text: "150克", value: "150" },
        { text: "200克", value: "200" }
      ]
    },
    {
      text: "西兰花",
      value: "西兰花", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "50克", value: "50" },
        { text: "100克", value: "100" },
        { text: "150克", value: "150" },
        { text: "200克", value: "200" }
      ]
    },
    {
      text: "生菜",
      value: "生菜", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "50克", value: "50" },
        { text: "100克", value: "100" },
        { text: "150克", value: "150" },
        { text: "200克", value: "200" }
      ]
    }
  ]
},
{
  text: "水果类",
  value: "4",
  children: [
    {
      text: "苹果",
      value: "苹果", // 修改这里
      children: [
        { text: "0个", value: "0" },
        { text: "1个", value: "150" },
        { text: "2个", value: "300" },
        { text: "3个", value: "450" },
        { text: "4个", value: "600" }
      ]
    },
    {
      text: "香蕉",
      value: "香蕉", // 修改这里
      children: [
        { text: "0个", value: "0" },
        { text: "1个", value: "100" },
        { text: "2个", value: "200" },
        { text: "3个", value: "300" },
        { text: "4个", value: "400" }
      ]
    },
    {
      text: "橙子",
      value: "橙子", // 修改这里
      children: [
        { text: "0个", value: "0" },
        { text: "1个", value: "120" },
        { text: "2个", value: "240" },
        { text: "3个", value: "360" },
        { text: "4个", value: "480" }
      ]
    },
    {
      text: "葡萄",
      value: "葡萄", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "50克", value: "50" },
        { text: "100克", value: "100" },
        { text: "150克", value: "150" },
        { text: "200克", value: "200" }
      ]
    },
    {
      text: "猕猴桃",
      value: "猕猴桃", // 修改这里
      children: [
        { text: "0个", value: "0" },
        { text: "1个", value: "80" },
        { text: "2个", value: "160" },
        { text: "3个", value: "240" },
        { text: "4个", value: "320" }
      ]
    }
  ]
},
{
  text: "零食类",
  value: "5",
  children: [
    {
      text: "薯片",
      value: "薯片", // 修改这里
      children: [
        { text: "0包", value: "0" },
        { text: "1小包", value: "20" },
        { text: "1中包", value: "50" },
        { text: "1大包", value: "100" }
      ]
    },
    {
      text: "巧克力",
      value: "巧克力", // 修改这里
      children: [
        { text: "0块", value: "0" },
        { text: "1块", value: "10" },
        { text: "2块", value: "20" },
        { text: "3块", value: "30" }
      ]
    },
    {
      text: "坚果",
      value: "坚果", // 修改这里
      children: [
        { text: "0克", value: "0" },
        { text: "25克", value: "25" },
        { text: "50克", value: "50" },
        { text: "75克", value: "75" }
      ]
    },
    {
      text: "饼干",
      value: "饼干", // 修改这里
      children: [
        { text: "0片", value: "0" },
        { text: "3片", value: "15" },
        { text: "6片", value: "30" },
        { text: "9片", value: "45" }
      ]
    },
    {
      text: "糖果",
      value: "糖果", // 修改这里
      children: [
        { text: "0颗", value: "0" },
        { text: "3颗", value: "10" },
        { text: "6颗", value: "20" },
        { text: "9颗", value: "30" }
      ]
    }
  ]
},
{
  text: "水",
  value: "6", // 修改这里
  children: [
    {
      text: "水",
      value: "水", // 修改这里
      children: [
        { text: "0杯", value: "0" },
        { text: "1杯", value: "200" },
        { text: "2杯", value: "400" },
        { text: "3杯", value: "600" },
        { text: "4杯", value: "800" },
        { text: "5杯", value: "1000" },
        { text: "6杯", value: "1200" }
      ]
    }
  ]
}
    ];

    const show = ref(false);
    const showPopup = () => {
      show.value = true;
    };
    const onCancel = () =>{
        show.value=false
    };

    const onConfirm = ({ selectedValues }) => {
        const stitle=selectedValues[1];
        const sgram=selectedValues[2];
        const svalue=selectedValues[0];
         let newItem = {
         title: stitle,
         value: svalue,
         gram: sgram 
         };
        breitems.value.push(newItem);
        show.value=false;
    }

    const deleteItem = (index) => {
      // 使用value来修改ref的值
      breitems.value.splice(index, 1);
    };

    const score = ref(null);
    // 从localStorage获取userid，如果不存在则定义一个默认值
    let userid = "1";
    // 定义一个方法来获取今天的分数
    const getdiet = async () => {
      const url = '/accessment/getdiet';
      try {
        // 调用后端API
        const response = await http.get(url,{ params: { userid } });
        // 将返回的分数设置到score属性中
        score.value = response.data;
        if(score.value==0)
        {
          score.value="请先完成饮食评测";
        }
      } catch (error) {
        console.error('获取分数失败:', error);
        console.log(response.data);
        score.value = 0; // 假设0表示获取分数失败
      }
    };

    // 页面挂载时调用getTodayScore方法
    onMounted(getdiet);

    
    const gram1 = ref(0);
    const gram2 = ref(0);
    const gram3 = ref(0);
    const gram4 = ref(0);
    const gram5 = ref(0);
    const gram6 = ref(0);

    const postdiet = async () => {
      const url = '/accessment/postdiet';

      breitems.value.forEach(item => {
      const value = parseInt(item.value, 10); // 将value从字符串转换为数字
      const gram = parseInt(item.gram, 10);  // 将gram从字符串转换为数字

      // 使用if-else语句更新对应的gram变量
      if (value === 1) {
        gram1.value += gram;
      } else if (value === 2) {
        gram2.value += gram;
      } else if (value === 3) {
        gram3.value += gram;
      } else if (value === 4) {
        gram4.value += gram;
      } else if (value === 5) {
        gram5.value += gram;
      } else if (value === 6) {
        gram6.value += gram;
      }
    });
      try {
        // 调用后端API
        const response = await http.post(url,{ 
          userid: '2',
          gram1:gram1.value,
          gram2:gram2.value,
          gram3:gram3.value,
          gram4:gram4.value,
          gram5:gram5.value,
          gram6:gram6.value,
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
        brecolumns,
        onCancel,
        onConfirm,
        breitems,
        deleteItem,
        score,
        getdiet,
        postdiet
    }
  },
    data() {
      return {
      }
    },
    methods: {
      
    }
  }
  </script>
  
  <style>
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