<script setup>
import { ref,onMounted,computed } from "vue";
import tabbar from "../../components/tabbar.vue";
import listcell from "../../components/listcell.vue";
import { state } from '../../state/state.js';
import axios from 'axios';
import { showConfirmDialog } from 'vant';

const onEditProfile=()=> {
  showConfirmDialog({
    title: '个人介绍',
    message: `<input id="custom-input" type="text" style="border: none; width: 100%; height: 100%; box-sizing: border-box; text-align: center;" placeholder="点击输入内容" />` ,
    allowHtml: true,
  }).then(() => {
    var inputValue=document.getElementById('custom-input').value;
    console.log(inputValue);
    if (inputValue) {
      introduce.value=inputValue;
      state.setIntroduce(inputValue);
    } else {
    }
    document.getElementById('custom-input').value='';
  }).catch(() => {
    document.getElementById('custom-input').value='';
  });
    };
const introduce=ref('');
const activeNames = ref(['1']);
const activeTab=ref(0);
const postsme = ref([]);

const loadingMe = ref(false);
const finishedMe = ref(false);
const onLoadMe = () => {
  console.log("onload");
  loadingMe.value = false;
  finishedMe.value = true;
};
const loadingLike = ref(false);
const finishedLike = ref(false);
const onLoadLike = () => {
  console.log("onload");
  loadingLike.value = false;
  finishedLike.value = true;
};

const postslike = ref([]);

onMounted(() => {
  getUser();
  getPostsme();
  getCardData();
  introduce.value=state.introduce;
  getPostslike();
});
const userId=state.user.userId;
const userName=ref(0);
const likeNum=computed(() => {
      return postsme.value.reduce((sum, post) => sum + post.like, 0);
});
const postNum=computed(() => {
      return postsme.value.length;
});
const dayNum=computed(() => {
      return cards.value.reduce((sum, card) => sum + card.checkDays.reduce((cardSum, day) => cardSum + day, 0), 0);
});
const cards = ref([]);
const friendNum=ref(0);
const getUser = async ()=>{
  try {
    const response = await axios.get(`/api/user/${userId}`);
    userName.value=response.data.userName;
    friendNum.value=response.data.friends.length;
  } catch (error) {
    console.error('获取失败',error);
  }
};
const getPostsme = async () => {
      try {
    const response = await axios.get(`/api/posts/${userId}/posts`);
    if (response.data.hasOwnProperty('message')) {
    }else{
      console.log(response.data);
      const fetchedPosts = response.data.map(post => ({
      postId: post.postId,
      imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg', // 默认图片
      time: new Date(post.postedAt).toLocaleString('zh-CN', { month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit', hour12: false }),
      name: post.userName, // 根据实际需求修改
      text: post.content,
      like: post.likes,
      imgUrls: post.photos.map(photo => `http://127.0.0.1:8888${photo}`)
    }));
    postsme.value = fetchedPosts;
    }
  } catch (error) {
    console.log('Failed to fetch posts:');
  }
};
const getPostslike = async () => {
      try {
    const response = await axios.get(`/api/posts/${userId}/likedPosts`);
    if (response.data.hasOwnProperty('message')) {
    }else{
      console.log(response.data);
      const fetchedPosts = response.data.map(post => ({
      postId: post.postId,
      imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg', // 默认图片
      time: new Date(post.postedAt).toLocaleString('zh-CN', { month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit', hour12: false }),
      name: post.userName, // 根据实际需求修改
      text: post.content,
      like: post.likes,
      imgUrls: post.photos.map(photo => `http://127.0.0.1:8888${photo}`)
    }));
    postslike.value = fetchedPosts;
    }
  } catch (error) {
    console.log('Failed to fetch posts:');
  }
};
//获取数据
const getCardData = async () => {
  try {
    const response = await axios.get(`/api/card/${state.user.userId}`);
    console.log(response.data);
    cards.value=response.data;
  } catch (error) {
    console.error('获取失败',error);
  }
};

const addFriend=async () => {
  showConfirmDialog({
    title: '添加好友',
    message: `<input id="custom-input" type="text" style="border: none; width: 100%; height: 100%; box-sizing: border-box; text-align: center;" placeholder="输入好友userId" />` ,
    allowHtml: true,
  }).then(async () => {
    var inputValue=document.getElementById('custom-input').value;
    if (inputValue) {
      try {
    const friendId=inputValue;
    const response = await axios.post(`/api/user/${userId}/friends/${friendId}`,{
      headers: {
        'Content-Type': 'application/json'
      }
    });
    console.log(response.data.message);
    getUser();
  } catch (error) {
    console.error('添加失败',error);
  }
  getCardData();
    } else {
    }
    document.getElementById('custom-input').value='';
  }).catch(() => {
    document.getElementById('custom-input').value='';
  });

};
</script>

<template>
  <van-nav-bar
    title="个人主页"
    right-text="编辑资料"
    @click-right="onEditProfile"
    fixed
    class="bar"
  />
  <div class="home">
    <div class="profile-header">
      <van-image class="avatar" round width="80px" height="80px" src="https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg" />
      <div class="profile-info">
        <div class="username">
          {{ userName }}
        </div>
        <div class="id">账号：{{ userId }}</div>
      </div>
    </div>
    <van-cell-group class="van-cell-group1">
      <van-cell class="van-cell1" :title=likeNum label="获赞"/>
      <van-cell class="van-cell1" :title=postNum label="发布"/>
      <van-cell class="van-cell1" :title=dayNum label="打卡"/>
      <van-cell class="van-cell1" :title=friendNum label="朋友" @click="addFriend"/>
    </van-cell-group>
    <van-collapse v-model="activeNames">
      <van-collapse-item title="个人介绍" name="1">{{introduce}}</van-collapse-item>
    </van-collapse>
    <van-tabs v-model:active="activeTab" class="tabs">
      <van-tab title="发布">
        <div class="tab-content">
          <van-list
            v-model:loading="loadingMe"
            :finished="finishedMe"
            finished-text="没有更多了"
            @load="onLoadMe"
            class="listcontainer"
          >
            <listcell v-for="(post, index) in postsme" :key="post.postId" v-model="postsme[index]" />
          </van-list>
        </div>
      </van-tab>
      <van-tab title="奖牌">
        <div class="tab-content">
          <!-- 奖牌内容 -->
        </div>
      </van-tab>
      <van-tab title="打卡">
        <div class="tab-content">
          <!-- 打卡内容 -->
        </div>
      </van-tab>
      <van-tab title="喜欢">
        <div class="tab-content">
          <van-list
            v-model:loading="loadingLike"
            :finished="finishedLike"
            finished-text="没有更多了"
            @load="onLoadLike"
            class="listcontainer"
          >
            <listcell v-for="(post, index) in postslike" :key="post.postId" v-model="postslike[index]" />
          </van-list>
        </div>
      </van-tab>
    </van-tabs>
  </div>
  <tabbar />
</template>


<style scoped>
.bar{
  z-index: 999;
}
.home {
  padding: 0;
  position: relative;
  top: 45px;
  background-color: #f2f2f2;
  height: calc(100vh - 45px); /* 计算高度 */
  display: flex;
  flex-direction: column;
}

.profile-header {
  margin-left: 20px;
  margin-right: 20px;
  display: flex;
  align-items: center;
  padding: 16px 0;
}

.avatar {
  margin-right: 16px;
}

.profile-info {
  flex: 1;
}

.username {
  display: flex;
  align-items: center;
  font-size: 16px;
  font-weight: bold;
}

.username .van-icon {
  margin-left: 4px;
}

.id {
  color: #818181;
  font-size: 14px;
}

.van-cell-group1 {
  display: flex;
  text-align: center;
  border-radius: 15px 15px 0 0;
}

.van-cell1 {
  margin-left: 15px;
  margin-top: 5px;
  margin-bottom: 5px;
  padding: 0;
  width: 60px;
  font-size: 16px;
}

.tabs {
  flex: 1;
  display: flex;
  flex-direction: column;
}

.tab-content {
  flex: 1;
  overflow-y: auto;
  padding-bottom: 50px; /* 确保底部空间充足 */
}

.listcontainer {
  background-color: #fff;
}
</style>

