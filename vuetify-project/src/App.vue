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
<style type="text/css" scoped>
.background{
  background: url("assets/background.jpg") no-repeat ;
  background-size: cover;
}
</style>
