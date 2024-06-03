
<script setup>
import tabbar from "../../components/tabbar.vue";
import { ref, computed, onBeforeUpdate } from 'vue';
import CardItem from './cardItem.vue';

const cards = ref([
  { id: 1, description: '今日跑步5公里', creator: 'User1', isCheckedInToday: false, isFriendCheckedInToday: true, createdAt: '2024-05-30' },
  { id: 2, description: '今日阅读1小时', creator: 'User2', isCheckedInToday: false, isFriendCheckedInToday: false, createdAt: '2024-05-29' },
  { id: 3, description: '今日冥想30分钟', creator: 'User3', isCheckedInToday: true, isFriendCheckedInToday: false, createdAt: '2024-05-28' },
  { id: 4, description: '今日喝水8杯', creator: 'User4', isCheckedInToday: true, isFriendCheckedInToday: true, createdAt: '2024-05-27' },
  // 添加更多卡片信息
]);

const sortedCards = computed(() => {
  return cards.value.sort((a, b) => {
    if (a.isFriendCheckedInToday && !a.isCheckedInToday) return -1;
    if (!a.isFriendCheckedInToday && !a.isCheckedInToday) return -1;
    if (a.isCheckedInToday && !a.isFriendCheckedInToday) return 1;
    if (a.isCheckedInToday && a.isFriendCheckedInToday) return 1;
    return new Date(b.createdAt) - new Date(a.createdAt);
  });
});
</script>


<template>
  <van-nav-bar title="打卡记录" />
  <div class="card-list">
    <CardItem
      v-for="card in sortedCards"
      :key="card.id"
      :description="card.description"
      :creator="card.creator"
      :isCheckedInToday="card.isCheckedInToday"
      :isFriendCheckedInToday="card.isFriendCheckedInToday"
    />
  </div>
  <tabbar/>
</template>

<style scoped>
.card-list {
  padding: 16px;
  display: flex;
  flex-direction: column;
  gap: 12px;
}
</style>