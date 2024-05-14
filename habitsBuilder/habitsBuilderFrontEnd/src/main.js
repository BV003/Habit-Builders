import { createApp } from 'vue'
// import './style.css'
import App from './App.vue'
import { Button,Field, CellGroup } from 'vant';
import 'vant/lib/index.css';

import router from "./router/index.js"

const app =createApp(App)
app.use(Button);
app.use(Field);
app.use(CellGroup);
app.use(router);
app.mount('#app')
