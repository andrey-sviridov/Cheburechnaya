<template>
  <v-app class="background">
    <v-main>
      <HelloWorld />
    </v-main>
  </v-app>
</template>

<script setup>
  import HelloWorld from '@/components/HelloWorld.vue'
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
