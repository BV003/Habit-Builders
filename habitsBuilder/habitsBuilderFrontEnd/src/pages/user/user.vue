<script setup>
import { ref } from "vue";
import tabbar from "../../components/tabbar.vue";
import listcell from "../../components/listcell.vue";
const onEditProfile=()=> {
      // 编辑资料点击事件
      console.log('编辑资料');
    };
const activeNames = ref(['1']);
const activeTab=ref(0);
const postsme = ref([
      {
        imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        time: '5/18 17:50',
        name: 'Chen',
        text: '如果你正在使用一个前端框架或库，确保你遵循该框架或库的指导原则来正确使用 ARIA 角色。有时，框架或库会自动处理 ARIA 角色和属性，因此手动添加可能会引起冲突。',
        like: 2,
        imgUrls:[
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        ]
      },
      {
        imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        time: '5/19 14:30',
        name: 'Li',
        text: '这是一段示例文本，用于展示如何使用Vue 3和Vant库。',
        like: 3,
        imgUrls:[
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        ]
      },
    ])

    const loading = ref(false);
    const finished = ref(false);
    const refreshing = ref(false);
    const onLoad = () => {
      console.log("onload");
      loading.value = false;
      refreshing.value=false;
      finished.value = true;

    };


    const postslike = ref([
      {
        imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        time: '5/18 17:50',
        name: 'Chen',
        text: '如果你正在使用一个前端框架或库，确保你遵循该框架或库的指导原则来正确使用 ARIA 角色。有时，框架或库会自动处理 ARIA 角色和属性，因此手动添加可能会引起冲突。',
        like: 2,
        imgUrls:[
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        ]
      },
      {
        imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        time: '5/19 14:30',
        name: 'Li',
        text: '这是一段示例文本，用于展示如何使用Vue 3和Vant库。',
        like: 3,
        imgUrls:[
          'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        ]
      },
    ])
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
          Chen
        </div>
        <div class="id">账号：1134100956</div>
      </div>
    </div>
    <van-cell-group class="van-cell-group1">
      <van-cell class="van-cell1" title="1" label="获赞"/>
      <van-cell class="van-cell1" title="27" label="发布"/>
      <van-cell class="van-cell1" title="144" label="奖牌"/>
      <van-cell class="van-cell1" title="28" label="朋友"/>
    </van-cell-group>
    <van-collapse v-model="activeNames">
      <van-collapse-item title="个人介绍" name="1">这个人很懒，没有介绍......</van-collapse-item>
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

