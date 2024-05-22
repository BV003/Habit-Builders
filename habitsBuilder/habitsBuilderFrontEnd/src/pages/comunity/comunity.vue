<script setup>
import tabbar from "../../components/tabbar.vue";
import listcell from "../../components/listcell.vue";
import { ref ,computed } from 'vue';


    const loading = ref(false);
    const finished = ref(false);
    const refreshing = ref(false);

    const onLoad = () => {
      console.log("onload");
      loading.value = false;
      refreshing.value=false;
      finished.value = true;

    };

    const onRefresh = () => {
      finished.value = false;
      loading.value = true;
      onLoad();
    };
    const posts = ref([
      {
        imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        time: '5/18 17:50',
        name: 'Chen',
        text: '如果你正在使用一个前端框架或库，确保你遵循该框架或库的指导原则来正确使用 ARIA 角色。有时，框架或库会自动处理 ARIA 角色和属性，因此手动添加可能会引起冲突。',
        like: 2
      },
      {
        imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        time: '5/19 14:30',
        name: 'Li',
        text: '这是一段示例文本，用于展示如何使用Vue 3和Vant库。',
        like: 3
      },
    ])
    const show = ref(false);
    const onClick = () => {
      console.log("onClick");
      show.value=true;
    };

const message=ref('')
const crossClick=()=>{
  
  show.value = false;
  message.value='';
}

const isMessageEmpty = computed(() => {
  return message.value.trim() === '';
});

const onPublish = () => {
  const formatDate = (date) => {
    const options = { month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit', hour12: false };
    return new Date(date).toLocaleString('zh-CN', options);
  };
  const newPost = {
    imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg', // 默认图片
    time: formatDate(new Date()), // 当前时间
    name: 'User', // 可以根据实际需求修改
    text: message.value,
    like: 0
  };
  
  posts.value.push(newPost);
  message.value = ''; // 清空输入框
  show.value = false; // 关闭 Action Sheet
};
</script>

<template>
  
  <van-nav-bar title="发现" fixed safe-area-inset-top>
    <template #right>
      <van-icon name="plus" size="18" color="black" @click="onClick" />
    </template>
  </van-nav-bar>
  <van-action-sheet v-model:show="show" :close-on-click-overlay="false">
    <template #default>
        <div class="custom-header">
          <div class="licon"><van-button :disabled="isMessageEmpty" color="#41b69c" size="small" @click="onPublish">发表</van-button></div>
          <div class="custom-header-title"></div>
          <div class="ricon"><van-icon name="cross" color="#646464" size="25" @click="crossClick"/></div>
        </div>
        <div class="content">
          <van-cell-group>
  <van-field
    v-model="message"
    rows="10"
    autosize
    type="textarea"
    placeholder="这一刻的想法....."
    class="custom-field"
  />
</van-cell-group>
        </div>
      </template>
  </van-action-sheet>
  <div class="comunity_list">
  <van-pull-refresh class="listrefresh" v-model="refreshing" success-text="刷新成功" @refresh="onRefresh">
  <van-list
    v-model:loading="loading"
    :finished="finished"
    finished-text="没有更多了"
    @load="onLoad"
    
  >
    <listcell v-for="(post, index) in posts" v-model="posts[index]">
    </listcell>
  </van-list>
</van-pull-refresh>
</div>
  <tabbar/>
</template>

<style scoped>
.content {
  padding: 15px;
  height: 250px;
}
.custom-header{
  display: flex;
  align-items: center;
  justify-content: center;
}
.licon{
  margin-left: 5px;
  text-align: center;
  align-items: center;
  width: 20%;
}
.custom-header-title{
  width: 65%;
}
.ricon{
  text-align: center;
  align-items: center;
  width: 15%;
}
.comunity_list{
  margin-top: 48px;
  min-height: 600px;
}
.listrefresh{
  min-height: 600px;
}
.van-list{
  min-height: 600px;
}
</style>