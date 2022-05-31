import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Info from "@/views/Info";
import About from "@/views/About";
import Posts from "@/views/Posts";


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
            component: About
        },
        {
            path: '/info',
            name: 'Info',
            component: Info
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
