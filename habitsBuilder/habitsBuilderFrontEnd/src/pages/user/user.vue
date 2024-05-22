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
        like: 2
      },
      {
        imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        time: '5/19 14:30',
        name: 'Li',
        text: '发布。',
        like: 3
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
        like: 2
      },
      {
        imgsrc: 'https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg',
        time: '5/19 14:30',
        name: 'Li',
        text: '喜欢。',
        like: 3
      },
    ])
</script>

<template>
  <van-nav-bar
      title="个人主页"
      right-text="编辑资料"
      @click-right="onEditProfile"
      fixed
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

    <van-tabs v-model:active="activeTab">
      <van-tab title="发布">
        <van-list
    v-model:loading="loading"
    :finished="finished"
    finished-text="没有更多了"
    @load="onLoad"
    class="listcontainer"
  >
    <listcell v-for="(post, index) in postsme" v-model="postsme[index]">
    </listcell>
  </van-list>
      </van-tab>
      <van-tab title="奖牌" />
      <van-tab title="打卡" />
      <van-tab title="喜欢">
        <van-list
    v-model:loading="loading"
    :finished="finished"
    finished-text="没有更多了"
    @load="onLoad"
    class="listcontainer"
  >
    <listcell v-for="(post, index) in postslike" v-model="postslike[index]">
    </listcell>
  </van-list>
      </van-tab>
      
    </van-tabs>
  </div>
  <tabbar/>

</template>



<style scoped>
.home {
  padding: 0;
  position: relative;
  top:45px;
  background-color: #f2f2f2;
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
.van-cell-group1{
  display: flex;
  text-align: center;
  border-radius: 15px 15px 0 0;
}
.van-cell1{
  margin-left: 15px;
  margin-top: 5px;
  margin-bottom: 5px;
  padding: 0;
  width: 60px;
  font-size:16px;
}
.listcontainer{
  background-color: #fff;
}
</style>

