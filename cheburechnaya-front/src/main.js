import Vue from 'vue'
import App from './App.vue'
import router from './router/index'
import vuetify from "@/plugins/vuetify";
import VueRouter from "vue-router";
import * as validations from "./validations.js"
import axios from "axios"
import VueAxios from "vue-axios"
import ApiService from "./services/api.service";

Vue.config.productionTip = false
Vue.prototype.$valid = validations;
Vue.prototype.$url = 'https://localhost:7065/';

ApiService.init('https://localhost:7065/')

Vue.use(VueRouter);
Vue.use(VueAxios,axios)

new Vue({
  router,
  vuetify,
  render: h => h(App)
}).$mount('#app')
