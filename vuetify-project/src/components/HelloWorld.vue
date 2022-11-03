<template>
  <v-container class="fill-height">
    <v-responsive class="d-flex align-center text-center fill-height">
      <v-app-bar
        :elevation="2"
        :height="50"
      >
        <v-avatar
        >
          <v-img src="../src/assets/logo.png" />
        </v-avatar>
        <span style="margin: 0 15px 0 5px">
          Чебуречная на закате
        </span>
        <v-divider vertical inset/>

        <v-btn
          :height="40"
          prepend-icon="mdi-home"
        >
          Главная
        </v-btn>
        <v-divider vertical inset/>
        <v-btn
          :height="40"
          prepend-icon="mdi-home"
        >
          Посты
        </v-btn>
        <v-divider vertical inset/>

        <v-spacer />
        <v-btn
          :height="40"
          variant="outlined"
          prepend-icon="mdi-login"
        >
          Авторизация
        </v-btn>
      </v-app-bar>
      <transition name="fade">
        <router-view class="router-view"/>
      </transition>
    </v-responsive>
  </v-container>
</template>

<script setup>
  //
</script>
<style type="text/css" scoped>

</style>
