<template>
  <v-dialog v-model="dialog" style="text-align: center; color: white;"  transition="slide-y-reverse-transition" persistent>
    <v-card-title class="fontSettings" style=" position:relative;">
      Подтверждение действия
    </v-card-title>
    <div style="width: 60%; align-self: center">
      <v-card-text
        style="padding: 30px 0 30px; "
      >Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</v-card-text>

    </div>
    <v-card-actions style="justify-content: center">
      <v-btn
        variant="flat"
        color="primary"
        @click="hideConfirm"
      >
        Принять
      </v-btn>
      <v-btn
        @click="hideConfirm"
      >
        Отклонить
      </v-btn>
    </v-card-actions>
  </v-dialog>
</template>

<script>
export default {
  name: "ConfirmationDialog",
  data() {
    return {
      dialog: false,
    }
  },
  methods: {
    showConfirm() {
      this.dialog = true;
    },
    hideConfirm() {
      this.dialog = false;
    }
  }
}
</script>

<style scoped>
.fontSettings{
  text-align: center;
  font-family: Impact;
  color: black;
  -webkit-text-stroke: 1px white;
  letter-spacing: 5px;
  font-size: 40px;
}
</style>
