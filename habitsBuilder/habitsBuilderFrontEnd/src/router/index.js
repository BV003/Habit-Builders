import { createRouter,createWebHashHistory } from "vue-router";

const routes=[
    {
        path:"/",
        name:"accessment",
        component: () => import("../pages/accessment/accessment.vue"),

    },
    {
        path:"/comunity",
        name:"comunity",
        component: () => import("../pages/comunity/comunity.vue"),

    },
    {
        path:"/habit",
        name:"habit",
        component: () => import("../pages/habit/habit.vue"),

    },
    {
        path:"/user",
        name:"user",
        component: () => import("../pages/user/user.vue"),

    },
];

const router= createRouter(
    {
        history:createWebHashHistory(),
        routes,
    },
);

export default router;