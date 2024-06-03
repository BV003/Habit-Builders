<template>
  <div class="image-grid" :class="`grid-${imgUrls.length}`">
    <div v-for="(url, index) in imgUrls" :key="index" class="grid-item">
      <van-image :src="url" width="100%" height="100%"/>
    </div>
  </div>
</template>

<script setup>

const props = defineProps({
  imgUrls: {
    type: Array,
    required: true,
  },
});
</script>

<style scoped>
.image-grid {
  display: grid;
  gap: 10px;
}
.grid-item {
  background-color: #f0f0f0;
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;
}

.grid-1 {
  grid-template-columns: repeat(1, 1fr);
  grid-template-rows: repeat(1, 1fr);
}

.grid-2 {
  grid-template-columns: repeat(2, 1fr);
  grid-template-rows: repeat(1, 1fr);
}

.grid-3 {
  grid-template-columns: repeat(3, 1fr);
  grid-template-rows: repeat(1, 1fr);
}

.grid-4{
  grid-template-columns: repeat(2, 1fr);
  grid-template-rows: repeat(2, 1fr);
}

.grid-5,
.grid-6 {
  grid-template-columns: repeat(3, 1fr);
  grid-template-rows: repeat(2, 1fr);
}

.grid-7,
.grid-8,
.grid-9 {
  grid-template-columns: repeat(3, 1fr);
  grid-template-rows: repeat(3, 1fr);
}
</style>