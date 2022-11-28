<template>
  <v-container class="fill-height">
    <v-responsive class="d-flex align-center text-center fill-height">
      <v-app-bar
        :elevation="2"
        :height="50"
        style="background-color: rgba(255,255,255, 0.5)"
      >
        <v-avatar
        >
          <v-img src="../src/assets/logo.png" />
        </v-avatar>
        <span style="margin: 0 15px 0 5px">
          Чебуречная на закате
        </span>

        <template
          v-for="el in this.appBarItems"
        >
          <v-divider vertical inset/>
          <v-btn
          :height="40"
          :prepend-icon=el.icon
          >
            {{el.name}}
          </v-btn>
        </template>

        <v-spacer />
        <v-btn
          :height="40"
          variant="outlined"
          prepend-icon="mdi-login"
          @click="this.$refs.alert.showAlert()"
        >
          Авторизация
        </v-btn>
      </v-app-bar>
      <transition name="fade">
        <router-view class="router-view"/>
      </transition>
    </v-responsive>
  </v-container>
  <alert-window ref="alert" />
</template>

<script>
  import AlertWindow from "@/components/AlertWindow";
  export default{
    name: 'main-page',
    components: {AlertWindow},
    data(){
      return{
        showWin: false,
        appBarItems:[
          {
            icon: 'mdi-home',
            name: 'Главная',
            route: '/'
          },
          {
            icon: 'mdi-text',
            name: 'Посты',
            route: '/posts'
          },
        ]
    };
    },
    methods:{
      showWindow(){
        this.$refs.confirmation.showConfirm(null, {title: 'Тест'})
      }
    }
  }
</script>
