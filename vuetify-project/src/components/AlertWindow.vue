<template>
        <v-dialog
                class="mainDiv"
                v-model="show"
                width="500"
        >
            <v-form ref="form"
            >
                <v-card-title>
                    <slot name="title">

                    </slot>
                    <v-spacer/>

                    <v-icon
                            @click="changeVisible(false)"
                    >
                        mdi-close
                    </v-icon>
                </v-card-title>
                <v-divider />

                <v-card-text
                >
                    <slot name="body">

                    </slot>
                </v-card-text
                >
                <v-divider></v-divider>
                <v-card-actions>
                    <slot name="buttons" :changeVisible="changeVisible">

                    </slot>
                    <v-btn >TEST</v-btn>
                </v-card-actions>
            </v-form>
        </v-dialog>
</template>

<script>
    export default {
        name: "alert-window",
        props:{
            show: Boolean,
            func: Function
        },
        data(){
          return{

          }
        },
        beforeDestroy() {
            this.show = false
        },
        methods:{
            changeVisible(val){
                this.show = val
            },
            login(){
                alert('login clicked')
            }
        }
    }
</script>

<style scoped>
    .mainDiv{
        height: auto;
    }
</style>
