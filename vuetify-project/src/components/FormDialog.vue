<template>
  <v-dialog :style="widthForm" v-model="dialog" style="text-align: center; color: white;"  transition="slide-y-reverse-transition" persistent>
    <v-card-title class="fontSettings" style=" position:relative;">
      <slot name="title" />
    </v-card-title>
    <div style="align-self: center; width: 100%" >
      <slot name="body" />
    </div>
    <v-card-actions style="justify-content: center">
      <slot name="actions" />
    </v-card-actions>
  </v-dialog>
</template>

<script>
export default {
  name: "FormDialog",
  props:{
    WidthForm: {
      type: Number,
      default: 20
    }
  },
  data() {
    return {
      dialog: false,
    }
  },
  created() {
    console.log(this.WidthForm)
  },
  methods: {
    showConfirm() {
      this.dialog = true;
    },
    hideConfirm() {
      this.dialog = false;
    }
  },
  computed:{
    widthForm(){
      return{
        "width": `${this.WidthForm}%`
      }
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
