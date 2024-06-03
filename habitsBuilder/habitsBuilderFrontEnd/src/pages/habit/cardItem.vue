<template>
    <van-card class="card" :class="cardClass">
      <template #title>
        <div class="card-title">{{ description }}</div>
      </template>
      <template #desc>
        <div class="card-desc">
          <span class="card-creator">创建人: {{ creator }}</span>
          <div class="checkin-status-container">
            <span :class="['checkin-status', isCheckedInToday ? 'checked-in' : 'not-checked-in']">
              {{ isCheckedInToday ? '今日已打卡' : '今日未打卡' }}
            </span>
            <span :class="['checkin-status', isFriendCheckedInToday ? 'friend-checked-in' : 'friend-not-checked-in']">
              {{ isFriendCheckedInToday ? '好友已打卡' : '好友未打卡' }}
            </span>
          </div>
        </div>
      </template>
    </van-card>
  </template>
  
  <script setup>
  import { defineProps, computed } from 'vue';
  
  const props = defineProps({
    description: {
      type: String,
      required: true
    },
    creator: {
      type: String,
      required: true
    },
    isCheckedInToday: {
      type: Boolean,
      required: true
    },
    isFriendCheckedInToday: {
      type: Boolean,
      required: true
    }
  });
  
  const cardClass = computed(() => {
    if (props.isFriendCheckedInToday && !props.isCheckedInToday) {
      return 'friend-checked-in-self-not';
    }
    if (!props.isFriendCheckedInToday && !props.isCheckedInToday) {
      return 'both-not-checked-in';
    }
    if (props.isCheckedInToday && !props.isFriendCheckedInToday) {
      return 'self-checked-in-friend-not';
    }
    if (props.isCheckedInToday && props.isFriendCheckedInToday) {
      return 'both-checked-in';
    }
  });
  </script>
  
  <style scoped>
  .card {
    border: 1px solid #ebeef5;
    border-radius: 20px;
    padding: 12px;
    background-color: white;
  }
  
  .card-title {
    font-size: 16px;
    font-weight: bold;
  }
  
  .card-desc {
    display: flex;
    flex-direction: column;
    gap: 8px;
    margin-top: 8px;
  }
  
  .card-creator {
    color: #606266;
  }
  
  .checkin-status-container {
    display: flex;
    gap: 8px;
  }
  
  .checkin-status {
    padding: 2px 8px;
    border-radius: 12px;
    font-size: 12px;
  }
  
  .checked-in {
    color: #67c23a;
    background-color: #f0f9eb;
  }
  
  .not-checked-in {
    color: #f56c6c;
    background-color: #fef0f0;
  }
  
  .friend-checked-in {
    color: #409eff;
    background-color: #ecf5ff;
  }
  
  .friend-not-checked-in {
    color: #e6a23c;
    background-color: #fdf6ec;
  }
  
  .friend-checked-in-self-not {
    border: 2px solid #409eff;
    background-color: #ecf5ff;
  }
  
  .both-not-checked-in {
    border: 2px solid #e6a23c;
    background-color: #fdf6ec;
  }
  
  .self-checked-in-friend-not {
    border: 2px solid #67c23a;
    background-color: #f0f9eb;
  }
  
  .both-checked-in {
    border: 2px solid #909399;
    background-color: #f0f0f0;
  }
  </style>
  