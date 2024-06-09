<template>
  <van-nav-bar title="AI辅助锻炼" left-text="返回" left-arrow @click-left="onClickLeft"/>
    <div class="exercise-plan-generator">
    <h1>锻炼计划生成器</h1>
    <p>根据你的健身目标生成个性化的锻炼计划</p>
    </div>

<van-form @submit="onSubmit">
  <van-cell-group inset>

    <van-field
      v-model="goal"
      name="目标"
      label="目标"
      placeholder="目标"
      :rules="[{ required: true, message: '你的健身目标是什么' }]"
    />



    <van-field name="radio" label="性别">
  <template #input>
    <van-radio-group v-model="gender" direction="horizontal">
      <van-radio name="1">男</van-radio>
      <van-radio name="2">女</van-radio>
      <van-radio name="3">其他</van-radio>
    </van-radio-group>
  </template>
</van-field>

<van-field name="radio" label="训练方法">
  <template #input>
    <van-radio-group v-model="mentold" direction="horizontal">
      <van-radio name="1">抗阻训练 + 心肺训练</van-radio>
      <van-radio name="2">抗阻训练</van-radio>
      <van-radio name="3">仅饮食计划</van-radio>
      <van-radio name="4">个性化例程</van-radio>
    </van-radio-group>
  </template>
</van-field>

<van-field name="radio" label="锻炼类型">
  <template #input>
    <van-radio-group v-model="kind" direction="horizontal">
      <van-radio name="1">器械</van-radio>
      <van-radio name="2">自重</van-radio>
      <van-radio name="3">无装备</van-radio>
    </van-radio-group>
  </template>
</van-field>

<van-field name="radio" label="力量水平">
  <template #input>
    <van-radio-group v-model="level" direction="horizontal">
      <van-radio name="1">菜鸟</van-radio>
      <van-radio name="2">进阶</van-radio>
      <van-radio name="3">高手</van-radio>
    </van-radio-group>
  </template>
</van-field>

  </van-cell-group>
  <div style="margin: 16px;">
    <van-button round block type="primary" native-type="submit" @click=postbaidu>
      提交
    </van-button>
  </div>
</van-form>

<div class="exercise-plan-generator">
    <p style="margin:0; white-space:pre-wrap">{{ans}}</p>
</div>


</template>

<style>
    .exercise-plan-generator {
        text-align: center; /* 文本居中对齐 */
        margin: 20px; /* 外边距 */
        padding: 20px; /* 内边距 */
        border: 1px solid #ddd; /* 边框 */
        border-radius: 8px; /* 边框圆角 */
        background-color: #f9f9f9; /* 背景颜色 */
    }

    .exercise-plan-generator h1 {
        color: #333; /* 字体颜色 */
        font-size: 24px; /* 字体大小 */
    }

    .exercise-plan-generator p {
        color: #666; /* 字体颜色 */
        font-size: 16px; /* 字体大小 */
    }
</style>
<script>
import { ref,onMounted } from 'vue';
import axios from 'axios';

export default {
  setup() {
    const goal=ref(null);
    const gender = ref('1');
    const mentold = ref('1');
    const kind = ref('1');
    const level = ref('1');
    const query=ref(null);
    const ans = ref(null);
    const onClickLeft = () => history.back();
    const updateQuery = () => {

      query.value = ''; // 重置消息
      query.value += '我的目标是';
      query.value += goal.value;
      query.value += ',';
      if (gender.value === '1') {
        query.value+= '我是男性';
      } else if (gender.value === '2') {
        query.value+= '我是女性';
      }
      query.value += ',';
      query.value += '我的训练方法是';
      if (mentold.value === '1') {
        query.value+= '抗阻训练和心肺训练';
      } else if (mentold.value === '2') {
        query.value+= '抗阻训练';
      }else if (mentold.value === '3') {
        query.value+= '仅饮食计划';
      }
      query.value += ',';
      query.value += '我的锻炼类型是';
      if (kind.value === '1') {
        query.value+= '器械训练';
      } else if (kind.value === '2') {
        query.value+= '自重训练';
      }else if (kind.value === '3') {
        query.value+= '';
      }
      query.value += ',';
      query.value += '我目前的水平是';
      if (level.value === '1') {
        query.value+= '菜鸟';
      } else if (level.value === '2') {
        query.value+= '中等';
      }else if (level.value === '3') {
        query.value+= '高手';
      }
      query.value +='。请你帮我生成一个锻炼计划。'; 


    };

    const postbaidu = () => {
        console.log('开始发送数据到百度...');
        ans.value='正在生成中，请耐心等候';
        updateQuery();
        const url = 'https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/chat/completions';
    axios.post(url, { messages:[ {"role": "user","content": query.value}]}, {
        params: { access_token:'24.ad2520a282f8289fc05f4e0d343b5c95.2592000.1719998817.282335-77974807' }
  })
  .then(response => {
    ans.value=response.data.result;
    console.log('请求成功:', response.data.result);
  })
  .catch(error => {
    
    console.error('请求失败:', error);
  });

    };


    return { onClickLeft,goal,gender,mentold,kind,level,ans ,postbaidu};
  },
};


</script>