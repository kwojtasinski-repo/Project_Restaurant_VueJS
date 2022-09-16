import { createRouter, createWebHistory } from 'vue-router'
import HomePage from './pages/Home'
import SettingsPage from './pages/Settings'
import MyOrderPage from './pages/MyOrder'
import NotFoundPage from './pages/NotFound'

const routes = [
    {
        path: '/',
        name: 'home',
        component: HomePage
    },
    {
        path: '/settings',
        name: 'settings',
        component: SettingsPage
    },
    {
        path: '/my-order',
        name: 'my-order',
        component: MyOrderPage
    },
    // and finally the default route, when none of the above matches:
    { 
        path: "/:catchAll(.*)",
        name: 'not-found',
        component: NotFoundPage
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;