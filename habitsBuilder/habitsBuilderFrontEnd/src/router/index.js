import { createRouter,createWebHashHistory } from "vue-router";

const routes=[
    {
        path:"/",
        name:"index",
        component: () => import("../index/index.vue"),

    },
    {
        path:"/user",
        name:"user",
        component: () => import("../user/user.vue"),

    },
];

const router= createRouter(
    {
        history:createWebHashHistory(),
        routes,
    },
);

export default router;