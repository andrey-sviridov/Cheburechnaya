<template>
  <v-dialog v-model="dialog" :style="computedWidthForm" style="text-align: center;" transition="slide-y-reverse-transition" persistent>
    <v-card-title class="fontSettings" style="position:relative;">
      <slot name="title" />
    </v-card-title>
    <div style="align-self: center; width: 100%" >
      <slot name="body" />
    </div>
    <v-card-actions style="justify-content: center; color: black">
      <slot name="actions" />
    </v-card-actions>
  </v-dialog>
</template>

<script>
export default {
  name: "FormDialog",
  props:{
    size: {
      type: String,
      default: '300px'
    },
    height:{
      type: String,
      default: '300px'
    }
  },
  data() {
    return {
      dialog: false,
    }
  },
  created() {
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
    computedWidthForm(){
      return{
        "width": this.size,
        "height": this.height
      };
    }
  }
}
</script>

<style scoped>
.fontSettings{
  text-align: center;
  font-family: Impact,serif;
  color: black;
  -webkit-text-stroke: 1px white;
  letter-spacing: 5px;
  font-size: 40px;
}
.v-dialog > *{
  background: transparent !important;
}

</style>
