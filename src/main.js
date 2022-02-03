import Vue from 'vue'
import App from './App.vue'
import router from './router/index'
import vuetify from "@/plugins/vuetify";
import VueRouter from "vue-router";
import * as validations from "./validations.js"

Vue.config.productionTip = false
Vue.prototype.$valid = validations;

Vue.use(VueRouter);

new Vue({
  router,
  vuetify,
  render: h => h(App)
}).$mount('#app')
