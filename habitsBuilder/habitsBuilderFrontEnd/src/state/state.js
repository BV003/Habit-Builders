import { reactive } from 'vue';

export const state = reactive({
  user: null,
  setUser(user) {
    this.user = user;
  },
  clearUser() {
    this.user = null;
  },
  get isAuthenticated() {
    return !!this.user;
  },
});
