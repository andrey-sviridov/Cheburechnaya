<template>
  <v-container class="fill-height">
    <v-responsive class="d-flex align-center text-center fill-height">
      <v-app-bar
        :elevation="2"
        :height="50"
        style="background-color: rgb(248,250,255, 0.5)"
      >
        <v-avatar
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
        <div v-if="!isLogged" class="loginButtons">
          <v-btn
            :height="47"
            prepend-icon="mdi-login"
            @click="this.$refs.confirmation.showConfirm()"
          >
            Уведомление
          </v-btn>
          <v-btn
            :height="47"
            prepend-icon="mdi-login"
            @click="this.$refs.formDialog.showConfirm()"
          >
            Войти
          </v-btn>
        </div>
        <div v-else-if="isLogged" class="loginButtons" style="display: contents;">

          <v-menu open-on-hover>
            <template v-slot:activator="{ props }">

              <v-btn v-bind="props" :height="47">
            <span style=" padding-right: 10px">
                  Привет, Андрей!
                </span>
                <v-avatar
                  color="primary"
                  :size="30"
                >
                  A
                </v-avatar>
              </v-btn>


            </template>

            <v-list class="mt-1">
              <v-list-item class="mt-n2 mb-n1">
                <span>Свиридов Андрей Алексеевич</span>
              </v-list-item>
              <v-divider/>
              <v-list-item class="mt-1">
                <v-btn
                  :height="40"
                  width="100%"
                  variant="outlined"
                  prepend-icon="mdi-cog"
                  @click="this.isLogged = false"
                >
                  Параметры
                </v-btn>
              </v-list-item>
              <v-list-item>
                <v-btn
                  :height="40"
                  width="100%"
                  variant="outlined"
                  prepend-icon="mdi-login"
                  @click="this.isLogged = false"
                >
                  Выйти
                </v-btn>
              </v-list-item>
            </v-list>
          </v-menu>
        </div>
      </v-app-bar>
      <transition name="fade">
        <router-view class="router-view"/>
      </transition>
    </v-responsive>
  </v-container>

  <!-- ОКНА -->
  <confirmation-dialog ref="confirmation"/>
  <form-dialog ref="formDialog" size="30%">
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
        variant="flat"
        color="primary"
        :disabled="!fmLoginValidated"
      >
        Авторизоваться
      </v-btn>
      <v-spacer/>
      <v-btn
        variant="outlined"
        @click="this.$refs.formDialog.hideConfirm()"
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

export default {
  name: 'main-page',
  components: {FormDialog, ConfirmationDialog, AlertWindow},
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
.loginButtons > * {
  margin: 3px;
  vertical-align: middle;
}
</style>
