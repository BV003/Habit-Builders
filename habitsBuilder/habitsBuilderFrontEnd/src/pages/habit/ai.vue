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
        text-align: center;
        margin: 20px;
        padding: 20px;
        border: 1px solid #ddd;
        border-radius: 8px;
        background-color: #f9f9f9;
    }

    .exercise-plan-generator h1 {
        color: #333;
        font-size: 24px;
    }

    .exercise-plan-generator p {
        color: #666;
        font-size: 16px;
    }
</style>
<script>
import { ref } from 'vue';
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

      query.value = '';
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
        ans.value='正在生成中，请耐心等候';
        updateQuery();
        // Note: To use this feature, you need to set VITE_BAIDU_API_TOKEN in your .env file
        const token = import.meta.env.VITE_BAIDU_API_TOKEN || '';
        if (!token) {
          ans.value = '请配置百度API Token环境变量 (VITE_BAIDU_API_TOKEN)';
          return;
        }
        const url = 'https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/chat/completions';
        axios.post(url, { messages:[ {"role": "user","content": query.value}]}, {
            params: { access_token: token }
        })
        .then(response => {
          ans.value=response.data.result;
        })
        .catch(() => {
          ans.value = '请求失败，请检查API Token是否有效';
        });

    };


    return { onClickLeft,goal,gender,mentold,kind,level,ans ,postbaidu};
  },
};


</script>
