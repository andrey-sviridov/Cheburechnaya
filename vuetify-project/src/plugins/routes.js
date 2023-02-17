import App from "@/App.vue";
import Posts from "@/View/Posts";

export default [
  {
    path: '/',
    name: 'Home',
  },
  {
    path: '/posts',
    name: 'Posts',
    component: Posts
  }
]
