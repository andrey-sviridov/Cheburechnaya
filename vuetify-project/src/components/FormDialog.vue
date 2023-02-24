<template>
<v-dialog v-model="dialog" style="text-align: center;" transition="slide-x-transition">
  <VCard :style="computedHeight" style="padding: 30px; background: linear-gradient(0.75turn, transparent, aliceblue, aliceblue, transparent); backdrop-filter: blur(1px); box-shadow:0 0 0 5px transparent, 0 0 10px 5px transparent;">
    <v-card-title class="fontSettings align-self-center" style="position:relative;" :style="computedWidth">
      <slot name="title" />
    </v-card-title>
    <div class="align-self-center" :style="computedWidth" >
      <slot name="body" />
    </div>
    <v-card-actions class="align-self-center" :style="computedWidth">
      <slot name="actions" />
    </v-card-actions>
  </VCard>
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
    computedForm(){
      return{
        "width": this.size,
        "height": this.height
      };
    },
    computedWidth(){
      return{
        "width": this.size
      }
    },
    computedHeight(){
      return{
        "height": this.height
      }
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
</style>
