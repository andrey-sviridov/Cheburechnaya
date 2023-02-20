/**
 * main.js
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Components
import App from './App.vue'

// Composables
import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router';
import ApiService from "@/services/api.service";

// Plugins
import { registerPlugins } from '@/plugins'
import vuetify from './plugins/vuetify'
import routes from "@/plugins/routes";

ApiService.init('https://localhost:7065/')
const app = createApp(App)
const router = createRouter({
  history: createWebHistory(),
  routes: routes,
})

app.config.globalProperties.$vueRouter = router;
app.config.globalProperties.$url = 'https://localhost:7065/';

registerPlugins(app)

app
  .use(vuetify)
  .use(router)
  .mount('#app')
