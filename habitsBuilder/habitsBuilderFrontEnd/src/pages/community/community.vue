<script setup>
import tabbar from "../../components/tabbar.vue";
import listcell from "../../components/listcell.vue";
import { ref ,computed,onMounted } from 'vue';
import axios from 'axios';
import { state } from '../../state/state.js';

    const loading = ref(false);
    const finished = ref(false);
    const refreshing = ref(false);
    const show = ref(false);
    const message=ref('')
    const fileList = ref([]);
    const userId=state.user.userId;
    const posts = ref([]);

    const onLoad = () => {
      if (refreshing.value) {
          posts.value = [];
          refreshing.value = false;
        }
      getPosts();
      loading.value = false;
      finished.value = true;
    };

    const onRefresh = () => {
      finished.value = false;
      loading.value = true;
      onLoad();
    };
    const getPosts = async () => {
      try {
    const response = await axios.get(`/api/posts/${userId}/allposts`);
    if (response.data.hasOwnProperty('message')) {
    }else{
      const fetchedPosts = response.data.map(post => ({
      postId: post.postId,
      imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg', // 默认图片
      time: new Date(post.postedAt).toLocaleString('zh-CN', { month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit', hour12: false }),
      name: post.userName, // 根据实际需求修改
      text: post.content,
      like: post.likes,
      imgUrls: post.photos.map(photo => `http://127.0.0.1:8888${photo}`)
    }));
    posts.value = fetchedPosts;
    }
  } catch {
    // Error handled silently
  }
};
    const onClick = () => {
      show.value=true;
    };


const crossClick=()=>{
  
  show.value = false;
  message.value='';
  fileList.value=[];
}

const isMessageEmpty = computed(() => {
  return message.value.trim() === '';
});

const onPublish = async () => {
  // 构建新帖数据，假设后端需要的格式如下：
  const newPostData = {
    content: message.value,
    photos: fileList.value.map(file => file.file)
  };

  try {
    const formData = new FormData();
    formData.append('content', newPostData.content);
    newPostData.photos.forEach((photo) => {
      formData.append(`files`, photo);
    });
    const response = await axios.post(`/api/posts/${userId}/posts`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
  
  message.value = ''; // 清空输入框
  fileList.value=[];
  show.value = false; // 关闭 Action Sheet
  onLoad();
}catch{
  // Error handled silently
}
}

    // const posts = ref([
    //   {
    //     imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
    //     time: '5/18 17:50',
    //     name: 'Chen',
    //     text: '如果你正在使用一个前端框架或库，确保你遵循该框架或库的指导原则来正确使用 ARIA 角色。有时，框架或库会自动处理 ARIA 角色和属性，因此手动添加可能会引起冲突。',
    //     like: 2,
    //     imgUrls:[
    //       'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
    //       'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
    //       'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
    //       'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
    //     ]
    //   },
    //   {
    //     imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
    //     time: '5/19 14:30',
    //     name: 'Li',
    //     text: '这是一段示例文本，用于展示如何使用Vue 3和Vant库。',
    //     like: 3,
    //     imgUrls:[
    //       'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
    //     ]
    //   },
    // ])
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
    rows="3"
    autosize
    type="textarea"
    placeholder="这一刻的想法....."
    class="custom-field"
  />
  <div class="userimg"><van-uploader v-model="fileList" multiple :max-count="9" /></div>
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
  <div v-if="posts.length > 0"><listcell v-for="(post, index) in posts" v-model="posts[index]">
    </listcell></div>
    
  </van-list>
</van-pull-refresh>
</div>
  <tabbar/>
</template>

<style scoped>
.content {
  padding: 15px;
  height: 500px;
}
.custom-header{
  display: flex;
  align-items: center;
  justify-content: center;
}
.custom-field .van-field__control {
  min-height: 100px; 
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