<template>
  <div class="text-center">
    <v-dialog
      v-model="dialog"
    >
      <v-card style="text-align: center; background-color: rgba(255,255,255, 0.1); color: white; width: 60%; align-self: center; margin: 50px 50px" >
        <v-card-title>
          Информационное окно
        </v-card-title>
        <v-card-text>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
        </v-card-text>
        <v-card-actions style="flex-direction: column">
          <v-btn
            color="primary"
            variant="elevated"
            @click="dialog = false"
          >
            Agree
          </v-btn>
          <v-btn
            @click="dialog = false"
          >
            Disagree
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  name: "AlertWindow",
  data () {
    return {
      dialog: false,
    }
  },
  methods:{
    showAlert(){
      this.dialog = true;
    },
    hideAlert(){
      this.dialog = false
    }
  }
}
</script>

<style scoped>
</style>
