<template>
    <div>
        <v-dialog
                max-width="500px"
                v-model="visible"
                persistent
        >
            <v-form ref="form"
            >
                <v-card-title
                >
                    Подтверждение действия
                </v-card-title>
                <v-divider />
                <v-card-text>
                    Вы действительно хотите удалить запись "{{item.title}}"?
                </v-card-text>
                <v-divider></v-divider>
                <v-card-actions>
                    <v-spacer/>
                    <v-btn
                            color="primary"
                            text
                            @click="confirm"
                    >
                        Да
                    </v-btn>
                    <v-btn
                            color="primary"
                            text
                            @click="clearDialog"
                    >
                        Нет
                    </v-btn>
                </v-card-actions>
            </v-form>
        </v-dialog>
    </div>
</template>

<script>
    export default {
        name: "ConfirmationDialog",
        data(){
            return{
                visible: false,
                item:[],
                callback : null
            }
        },
        methods:{
            showConfirm(callback, item){
                this.callback = callback
                this.item = item
                this.visible = true
            },
            clearDialog(){
                this.visible = false
                this.item = []
            },
            confirm(){
                this.visible = false
                this.callback('ok')
            }
        }
    }
</script>

<style scoped>

</style>
