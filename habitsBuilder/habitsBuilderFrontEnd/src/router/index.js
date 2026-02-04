import { createRouter,createWebHashHistory } from "vue-router";
import { state } from '../state/state.js';

const routes=[
    {
        path:"/",
        name:"main",
        component: () => import("../pages/accessment/main.vue"),
        meta: { requiresAuth: true },
    },
    {
        path:"/accessment/diet",
        name:"diet",
        component: () => import("../pages/accessment/diet.vue"),
        meta: { requiresAuth: true },
    },
    {
        path:"/accessment/sleep",
        name:"sleep",
        component: () => import("../pages/accessment/sleep.vue"),
        meta: { requiresAuth: true },
    },
    {
        path:"/accessment/sport",
        name:"sport",
        component: () => import("../pages/accessment/sport.vue"),
        meta: { requiresAuth: true },
    },
    {
        path:"/accessment/history",
        name:"history",
        component: () => import("../pages/accessment/history.vue"),
        meta: { requiresAuth: true },
    },
    
    {
        path:"/community",
        name:"community",
        component: () => import("../pages/community/community.vue"),
        meta: { requiresAuth: true },

    },
    {
        path:"/habit",
        name:"habit",
        component: () => import("../pages/habit/habit.vue"),
        meta: { requiresAuth: true },

    },
    {
        path:"/user",
        name:"user",
        component: () => import("../pages/user/user.vue"),
        meta: { requiresAuth: true },

    },
    {
        path:"/login",
        name:"login",
        component: () => import("../pages/login/login.vue"),

    }, 
    {
        path:"/habit/ai",
        name:"ai",
        component: () => import("../pages/habit/ai.vue"),

    },
];

const router= createRouter(
    {
        history:createWebHashHistory(),
        routes,
    },
);

router.beforeEach((to, from, next) => {
    if (to.meta.requiresAuth && !state.isAuthenticated) {
      next('/login');
    } else {
      next();
    }
  });
export default router;