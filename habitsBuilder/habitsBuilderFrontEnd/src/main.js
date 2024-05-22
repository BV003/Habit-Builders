import { createApp } from 'vue'
// import './style.css'
import App from './App.vue'
import { Button,Field, CellGroup,Grid, GridItem,Cell,Card, Icon,NavBar,PickerGroup,
    TimePicker,
    Popup,
    Picker,
    Calendar
} from 'vant';
import 'vant/lib/index.css';

import router from "./router/index.js"




const app =createApp(App)
app.use(Button);
app.use(TimePicker);
app.use(NavBar);
app.use(Card);
app.use(Field);
app.use(CellGroup);
app.use(Grid);
app.use(GridItem);
app.use(PickerGroup);
app.use(Cell);
app.use(Icon);
app.use(Popup);
app.use(Picker);
app.use(Calendar);
app.use(router);
app.mount('#app')
