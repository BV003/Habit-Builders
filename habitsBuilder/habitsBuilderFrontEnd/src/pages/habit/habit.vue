<script setup>
import tabbar from "../../components/tabbar.vue";
import { ref, computed, onBeforeUpdate, watch } from 'vue';
import { showConfirmDialog } from 'vant';
import { onMounted } from 'vue';
import { state } from '../../state/state.js';
import { useRouter } from 'vue-router';
import axios from 'axios';

const router = useRouter();
const onClickLeft = async() =>{
  router.push('/habit/ai');
};
onMounted(() => {
  getCardData();
});
const show = ref(false);
const cards = ref([]);

const uncheckedCounts = computed(() => {
  return cards.value.map(card => card.checklistStatus.filter(status => !status).length);
});
const maxCheckedDays = computed(() => {
      return cards.value.map(card => {
        return card.checkDays.length > 0 ? Math.max(...card.checkDays) : 0;
      });
    });

const removeCard = async (index) => {
  try {
    const deleteCardData ={
      cardId: cards.value[index].id,
      userId: state.user.userId
    };
    const response = await axios.delete('/api/card', {
      headers: {
        'Content-Type': 'application/json'
      },
    data: deleteCardData
    });
  } catch (error) {
    console.error('删除失败',error);
  }
  getCardData();
};
const addItem = async (cardIndex) => {
  showConfirmDialog({
    title: '添加打卡',
    message: `<input id="custom-input" type="text" style="border: none; width: 100%; height: 100%; box-sizing: border-box; text-align: center;" placeholder="点击输入内容" />` ,
    allowHtml: true,
  }).then(async () => {
    var inputValue=document.getElementById('custom-input').value;
    if (inputValue) {
      try {
    const addItemData ={
      cardId: cards.value[cardIndex].id,
      userId: state.user.userId,
      checklistItem:inputValue
    };
    const response = await axios.post('/api/card/items',addItemData, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
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

const removeItem = async (cardIndex, itemIndex) => {
  try{
    const deleteItemData ={
      cardId: cards.value[cardIndex].id,
      userId: state.user.userId,
      checklistItem:cards.value[cardIndex].checklist[itemIndex]
    };
    const response = await axios.delete('/api/card/items', {
      headers: {
        'Content-Type': 'application/json'
      },
    data: deleteItemData
    });
  } catch (error) {
    console.error('删除失败',error);
  }
  getCardData();
};


const onClick = () => {
      show.value=true;
    };
const crossClick=()=>{
  
  show.value = false;
  create_category.value = '';
  create_description.value = '';
}

const create_category = ref('');
const create_description = ref('');
const onSubmit = async () => {
  try {
    const createCardData ={
      id: 0,
      description: create_description.value,
      category: create_category.value,
      userId: state.user.userId
    };
    const response = await axios.post('/api/card',createCardData, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  } catch (error) {
    console.error('添加失败',error);
  }
  getCardData();
  show.value = false;
  create_category.value = '';
  create_description.value = '';
};

const checkCard=async (cardIndex,index)=> {
  try {
    const checkData ={
      cardId: cards.value[cardIndex].id,
      userId: state.user.userId,
      checklistItem: cards.value[cardIndex].checklist[index]
    };
    const response = await axios.post('/api/card/check',checkData, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  } catch (error) {
    console.error('添加失败',error);
  }
  getCardData();
};


const getCardData = async () => {
  try {
    const response = await axios.get(`/api/card/${state.user.userId}`);
    cards.value=response.data;
  } catch (error) {
    console.error('获取失败',error);
  }
};

</script>

<template>
  <van-nav-bar title="打卡记录"   left-text="AI辅助"  @click-left="onClickLeft" >
    <template #right>
      <van-icon name="plus" size="18" color="black" @click="onClick" />
    </template>
  </van-nav-bar>
  <van-action-sheet v-model:show="show" :close-on-click-overlay="false">
    <template #default>
        <div class="custom-header">
          <div class="custom-header-title"></div>
          <div class="ricon"><van-icon name="cross" color="#646464" size="25" @click="crossClick"/></div>
        </div>
        <div class="content">
          <van-form @submit="onSubmit">
  <van-cell-group inset>
    <van-field
      v-model="create_category"
      label="类型"
      placeholder="类型"
      :rules="[{ required: true, message: '请填写类型' }]"
    />
    <van-field
      v-model="create_description"
      label="备注"
      placeholder="备注"
      :rules="[{ required: true, message: '备注' }]"
    />
  </van-cell-group>
  <div style="margin: 16px;">
    <van-button round block type="primary" native-type="submit">
      提交
    </van-button>
  </div>
</van-form>
        </div>
      </template>
  </van-action-sheet>
  <div class="card-list">
    <div v-for="(card, cardIndex) in cards" :key="card.id" class="card">
      <div class="card-left">
        <div class="card-left-button">
          <van-icon name="cross" size="15" @click="removeCard(cardIndex)"/>
          <van-icon name="plus" size="15" @click="addItem(cardIndex)"/>
        </div>
        <div class="card-left-user">{{ card.category }}</div>
        
        <div class="card-left-num">{{ uncheckedCounts[cardIndex] }}</div>
        <div class="card-left-day">打卡{{  maxCheckedDays[cardIndex] }}天</div>
        <div class="card-left-remark">{{ card.description }}</div>
      </div>
      <div class="card-right">
        <div v-for="(item, index) in card.checklist" :key="index" class="checkbox-item">
          <van-swipe-cell>
          <van-checkbox v-model="cards[cardIndex].checklistStatus[index]" @click="checkCard(cardIndex,index)" checked-color="#ff9800" class="checkbox-item-card">{{ item }}</van-checkbox>
          <template #right >
    <van-button round type="danger" text="删除" size="small" @click="removeItem(cardIndex, index)"/>
  </template>
        </van-swipe-cell>
        </div>
      </div>
    </div>
  </div>
  <tabbar/>
</template>

<style scoped>
.card-list {
  position: relative;
  top: 40px;
  margin-bottom: 100px;
  padding: 16px;
  display: flex;
  flex-direction: column;
  gap: 12px;
}
.card {
  display: flex;
  margin: 10px;
  padding: 10px;
  border: 1px solid #eaeaea;
  border-radius: 20px;
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
}

.card-left {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-right: 10px;
  margin-left: 5px;
  width: 30%;
}

.card-left-button {
  width: 100%;
  height: 30px;
  display: flex;
  gap: 10px;
}
.card-left-user {
  margin-top: 10px;
  height: 45%;
  text-align: left;
  align-self: start;
  align-items: start;
  font-size: 25px;
  font-weight: 300;
  width: 100%;
}
.card-left-day {
  font-size: 14px;
  font-weight: 400;
  margin: 5px 0;
  height: 15%;
  text-align: left;
  align-self: start;
  align-items: start;
  width: 100%;

}
.card-left-num {
  font-size: 25px;
  font-weight: 900;
  margin: 10px 0;
  height: 25%;
  text-align: left;
  align-self: start;
  align-items: start;
  width: 100%;

}

.card-left-remark {
  font-size: 14px;
  color: #ff9800;
  height: 15%;
  text-align: left;
  align-items: start;
  align-self: start;
  width: 100%;
}

.card-right {
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  margin-left: 10px;
  width: 70%;
  font-size: 13px;
}

.checkbox-item {
  padding: 10px 0;
  border-bottom: 1px dashed #dcdcdc;
}
.checkbox-item-card{
  height:  33px;
}

.checkbox-item:last-child {
  border-bottom: none;
}

.custom-header{
  display: flex;
  align-items: center;
  justify-content: center;
}
.custom-field .van-field__control {
  min-height: 100px; 
}
.custom-header-title{
  width: 85%;
}
.ricon{
  text-align: center;
  align-items: center;
  width: 15%;
}
.content{
  height: 500px;
}
</style>