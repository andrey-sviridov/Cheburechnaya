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

// Plugins
import { registerPlugins } from '@/plugins'
import vuetify from './plugins/vuetify'
import routes from "@/plugins/routes";

const app = createApp(App)
const router = createRouter({
  history: createWebHistory(),
  routes: routes,
})

registerPlugins(app)

app
  .use(vuetify)
  .use(router)
  .mount('#app')
