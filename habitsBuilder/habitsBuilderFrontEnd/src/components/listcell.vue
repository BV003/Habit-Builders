<script setup>
import { ref, watch, toRefs } from 'vue'


const props = defineProps({
  modelValue: {
    type: Object,
    required: true
  }
})
const emit = defineEmits(['update:modelValue'])

const posts = ref({ ...props.modelValue })

watch(() => props.modelValue, (newValue) => {
  posts.value = { ...newValue }
})

watch(posts, (newValue) => {
  emit('update:modelValue', newValue)
})

const showPopover = ref(false)


const actions =  ref([
      { text: '喜欢', icon: 'like-o' },
      { text: '分享', icon: 'share-o' },
    ]);

    const onSelect = (action) => {
        if (action.text==='喜欢') {
            if (action.icon==='like-o') {
                action.icon='like'
                posts.value.like++;
            }else{
                action.icon='like-o'
                posts.value.like--;
            }
            
        }
    };
</script>

<template>
    <div class="cell-container">
        <div class="container1">
            <div class="image">
                <van-image
                    round
                    width="1rem"
                    height="1rem"
                    :src= "posts.imgsrc"
                />
            </div>
            <div class="content">
                <div class="usern"><div class="usern1">{{posts.name}}</div><div class="usern2"><van-icon name="fire-o" size="18px" color="#898989" />   {{posts.like}}</div></div>
                <div class="user">{{posts.text}}</div>
                <div class="details-container">
                    <div class="time">{{posts.time}}</div>
                    <div class="commit">
                        <van-popover v-model:show="showPopover" :actions="actions" @select="onSelect" placement="left-start">
                            <template #reference>
                                <van-button icon="ellipsis"></van-button>
                            </template>
                        </van-popover>
                    </div>
                </div>
                
            </div>
        </div>
        
    </div>
</template>

<style scoped>
.cell-container {
    padding: 10px;
    border-bottom: 1px solid #e5e5e5;
    display: block;
    align-items: flex-start; 
}
  .container1{
    display: flex;
  }
  .image {
    margin-left: 15px;
    margin-right: 10px;
  }
  
  .van-image {
    width: 50px;
    height: 50px;
    border-radius: 50%; /* 更圆滑的圆形 */
  }
  
  .content{
    margin-right: 20px;
  }

  .usern {
    display: flex;
  }

  .usern1 {
    font-size: 16px;
    color: #47327f;
    font-weight:600;
  }

  .usern2 {
    margin-left:10px ;
    font-size: 16px;
    color: #898989;
    font-weight:600;
  }

  .user {
    margin-top: 10px;
    font-size: 16px;
    color: #000000;
    font-weight:400;
  }


  .details-container{
    display: flex;
  }

  .time{
    margin-top: 10px;
    font-size: 12px;
    color: #919191;
    font-weight:750;
    width: 65px;
  }
  
  
  .commit {
    width: 210px;
    text-align: right;
    /* 可以添加更多样式来显示评论或交互元素 */
  }
  .van-button--default{
    --van-button-default-border-color:0;
  }
  .van-divider {
    margin: 10px 0;
  }

</style>