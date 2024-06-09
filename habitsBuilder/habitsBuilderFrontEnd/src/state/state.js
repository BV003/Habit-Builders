import { reactive } from 'vue';

export const state = reactive({
  user: null,
  introduce: "这个人很懒，没有介绍......",
  setUser(user) {
    this.user = user;
  },
  setIntroduce(introduce) {
    this.introduce = introduce;
  },
  clearUser() {
    this.user = null;
  },
  get isAuthenticated() {
    return !!this.user;
  },
});
