<template>
  <v-app class="background">
    <v-main>
      <AppBar />
    </v-main>
  </v-app>
</template>

<script setup>
  import AppBar from '@/components/AppBar.vue'
</script>
<style>
html{
  overflow: auto !important;
}
body{
  min-height: 100vh;
  min-width: 100vw;
}
</style>
<style scoped>
/*.background{*/
/*  background: url("assets/light-background.jpg") no-repeat fixed;*/
/*  background-size: cover;*/
/*}*/
.background{
  background-color: aliceblue;
}
</style>
<style>
@import "@/styles/global.scss";
</style>
