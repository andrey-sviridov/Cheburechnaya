<template>
  <div class="grid">
<div>
  <v-sheet rounded="lg" style="background-color: transparent;">
    <v-list style="background-color: transparent;">
      <div class="pa-3">
        <v-btn variant="outlined" >
          Добавить пост
        </v-btn>
      </div>
      <v-divider class="my-2"></v-divider>
      <v-switch label="Стихи" />
      <v-switch label="Рассказы" />
      <v-switch label="Прочее" />
    </v-list>
  </v-sheet>
</div>
    <v-divider vertical/>
    <div>1111</div>
    <v-divider vertical/>
    <div>1111</div>
  </div>
</template>

<script>
export default {
  name: "Posts"
}
</script>

<style scoped>
.grid {
  display: grid;
  grid-template-columns: 20% 1px 58% 1px 20%;
  gap: 10px;
  height: 700px;
}
.switches .v-switch{

}
</style>
