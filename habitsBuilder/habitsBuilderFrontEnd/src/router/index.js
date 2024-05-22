import { createRouter,createWebHashHistory } from "vue-router";

const routes=[
    {
        path:"/",
        name:"accessment",
        component: () => import("../pages/accessment/accessment.vue"),

    },
    {
        path:"/accessment/main",
        name:"main",
        component: () => import("../pages/accessment/main.vue"),
    },
    {
        path:"/accessment/diet",
        name:"diet",
        component: () => import("../pages/accessment/diet.vue"),
    },
    {
        path:"/accessment/sleep",
        name:"sleep",
        component: () => import("../pages/accessment/sleep.vue"),
    },
    {
        path:"/accessment/sport",
        name:"sport",
        component: () => import("../pages/accessment/sport.vue"),
    },
    {
        path:"/accessment/history",
        name:"history",
        component: () => import("../pages/accessment/history.vue"),
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