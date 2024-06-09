import { createApp } from 'vue'
import App from './App.vue'

import 'vant/lib/index.css';

import router from "./router/index.js"

import { Button,Field,Form,Cell, CellGroup,Tabbar, TabbarItem,NavBar,List, Icon,Space } from 'vant';
import { Col, Row } from 'vant';
import { PullRefresh } from 'vant';
import { Image as VanImage } from 'vant';
import { Divider } from 'vant';
import { Popover } from 'vant';
import { Tab, Tabs } from 'vant';
import { Collapse, CollapseItem } from 'vant';
import { ActionSheet } from 'vant';
import { TimePicker,Card,Grid,GridItem, PickerGroup,Picker,
    Popup,Calendar
} from 'vant';
import { Toast } from 'vant';
import { Uploader } from 'vant';
import { Checkbox, CheckboxGroup } from 'vant';
import { Dialog } from 'vant';
import { SwipeCell,Radio,RadioGroup } from 'vant';


const app =createApp(App)
app.use(Button);
app.use(SwipeCell);
app.use(Radio);
app.use(RadioGroup);
app.use(Dialog);
app.use(Checkbox);
app.use(CheckboxGroup);
app.use(Uploader);
app.use(Toast);
app.use(Form);
app.use(Field);
app.use(Cell);
app.use(CellGroup);
app.use(Tabbar);
app.use(TabbarItem);
app.use(NavBar);
app.use(List);
app.use(Col);
app.use(Row);
app.use(PullRefresh);
app.use(VanImage);
app.use(Divider);
app.use(Popover);
app.use(Icon);
app.use(Tab);
app.use(Tabs);
app.use(Collapse);
app.use(CollapseItem);
app.use(ActionSheet);
app.use(Space);
app.use(TimePicker);
app.use(Card);
app.use(Grid);
app.use(GridItem);
app.use(PickerGroup);
app.use(Picker);
app.use(Popup);
app.use(Calendar);
app.use(router);
app.mount('#app')
