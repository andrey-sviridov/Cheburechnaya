import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Info from "@/views/Info";
import About from "@/views/About";
import Posts from "@/views/Posts";
import Test from "../views/Test";


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
            path: '/test',
            name: 'Test',
            component: Test
        },
        {
            path: '/info',
            name: 'Info',
            component: Info
        },
        {
            path: '/about',
            name: 'About',
            component: About
        },
        {
            path: '/posts',
            name: 'Posts',
            component: Posts,
            meta: {
                layout: 'blank'
            },
        }
    ]
},);
