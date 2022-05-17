<template>
    <v-dialog
            v-model="visible"
            hide-overlay
            persistent
            width="300"
    >
        <v-card-text style="text-align: center">
            {{this.text}}
            <v-progress-linear
                    indeterminate
                    color="white"
                    class="mb-0"
            ></v-progress-linear>
        </v-card-text>
    </v-dialog>
</template>

<script>
    export default {
        name: "ProgressLinear",
        data(){
            return{
                visible: false,
                text: 'Пожалуйста, подождите...'
            }
        },
        methods:{
            show(msg){
                this.visible = true
                this.text = msg ?? this.text
            },
            hide(){
                this.visible = false
                this.text = 'Пожалуйста, подождите...'
            }
        }
    }
</script>

<style scoped>

</style>