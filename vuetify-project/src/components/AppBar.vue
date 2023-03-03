<template>
  <v-container style="text-align: -webkit-center !important;">
    <v-responsive class="d-flex align-center fill-height">
      <v-app-bar
        :elevation="2"
        :height="50"
        class="bg-transparent"
        style="backdrop-filter: blur(3px)"
      >
        <v-avatar class="ma-lg-2"
        >
          <v-img src="../src/assets/logo.png"/>
        </v-avatar>
        <span style="margin: 0 15px 0 5px">
          Чебуречная на закате
        </span>

        <template
          v-for="el in this.appBarItems"
        >
          <v-divider vertical inset/>
          <v-btn
            :height="47"
            :prepend-icon=el.icon
            :to="el.route"
          >
            {{ el.name }}
          </v-btn>
        </template>

        <v-spacer/>
        <Authorization />
      </v-app-bar>
      <transition name="fade">
        <router-view v-slot="{ Component, route }">
          <transition name="fade">
            <component :is="Component" :key="route.path" />
          </transition>
        </router-view>

      </transition>
    </v-responsive>
  </v-container>

  <!-- ОКНА -->
  <confirmation-dialog ref="confirmation"/>
  <form-dialog ref="formDialog" size="30%" height="50%">
    <template v-slot:title>
      Авторизация
    </template>
    <template v-slot:body>
      <v-form ref="loginForm" v-model="fmLoginValidated">
        <v-text-field label="Логин*" variant="underlined" :rules="checkRules" validate-on="input"/>
        <v-text-field label="Пароль*" type="password" variant="underlined" :rules="checkRules" validate-on="input"/>
      </v-form>
    </template>
    <template v-slot:actions>
      <v-btn
        @click="this.isLogged = true; this.$refs.formDialog.hideConfirm()"
        variant="outlined"
        color="success"
        :disabled="!fmLoginValidated"
      >
        Авторизоваться
      </v-btn>
      <v-spacer/>
      <v-btn
        variant="outlined"
        @click="this.$refs.formDialog.hideForm()"
      >
        Закрыть
      </v-btn>
    </template>
  </form-dialog>
</template>

<script>
import AlertWindow from "@/components/AlertWindow";
import ConfirmationDialog from "@/components/ConfirmationDialog";
import FormDialog from "@/components/FormDialog";
import {requiredField} from "@/plugins/rules";
import Authorization from "@/components/Authorization";

export default {
  name: 'main-page',
  components: {Authorization, FormDialog, ConfirmationDialog, AlertWindow},
  data() {
    return {
      isLogged: false,
      fmLoginValidated: false,
      showWin: false,
      appBarItems: [
        {
          icon: 'mdi-home',
          name: 'Главная',
          route: '/'
        },
        {
          icon: 'mdi-text',
          name: 'Лента',
          route: '/posts'
        },
      ]
    };
  },
  methods: {
    showWindow() {
      this.$refs.confirmation.showConfirm(null, {title: 'Тест'})
    }
  },
  computed: {
    checkRules() {
      return [
        value => requiredField(value)
      ]
    }
  }
}
</script>
<style scoped type="text/css">
@import "@/styles/app-bar.css";

.loginButtons > * {
  margin: 3px;
  vertical-align: middle;
}
.fill-height{
  width: 90%;
  text-align: -webkit-center;
}
</style>
