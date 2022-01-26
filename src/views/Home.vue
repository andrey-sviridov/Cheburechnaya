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
    background: url("../assets/nebulaBack.jpg") no-repeat;
    background-size: 100%;
  }
</style>
