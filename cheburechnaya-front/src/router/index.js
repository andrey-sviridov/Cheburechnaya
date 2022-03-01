import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Info from "@/views/Info";
import About from "@/views/About";


export default new VueRouter({
    mode: 'history',
    routes:[{
        path: '/',
        name: 'Home',
        component: Home,
        children: [
            ]
        },
        {
            path: '/about',
            name: 'About',
            component: About
        },
        {
            path: '/info',
            name: 'Info',
            component: Info
        },
        {
            path: '/test',
            name: 'Test',
            component: () => import('@/views/Test.vue'),
            meta: {
                layout: 'blank'
            },
        }
    ]
},);
