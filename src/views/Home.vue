<template>
  <div class="home">
    <router-view></router-view>
  </div>
</template>

<script>
// @ is an alias to /src

export default {
  name: 'Home',
  components: {
  }
}
</script>

<style lang="css">
  body{
    background: url("../assets/wall.jpg") no-repeat fixed;
    background-size: 100%;
  }
</style>
