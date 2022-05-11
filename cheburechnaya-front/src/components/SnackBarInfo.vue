<template>
    <v-snackbar style="padding-top: 60px"
                v-model="isVisible"
                :top="top"
                :bottom="bottom"
                :left="left"
                :right="right"
                :color="type"

                :timeout="3000">
        {{ message }}
    </v-snackbar>
</template>

<script>
    export default {
        name: 'SnackBarInfo',
        data() {
            return {
                type: 'primary',
                message : 'Текст сообщения не указан',
                isVisible : false,
                left: false,
                right: false,
                top: false,
                bottom: false
            }
        },
        methods: {
            show(message, type, position) {
                this.position(position)
                this.type = type ? type : 'primary'
                this.message = message
                this.isVisible = true
            },
            hide() {
                this.isVisible = false
            },
            position(pos){
                this.resetPositions()
                switch (pos) {
                    case 'left':
                        this.left = true
                        break
                    case 'right':
                        this.right = true
                        break
                    case 'top':
                    case undefined:
                        this.top = true
                        break
                    case 'bottom':
                        this.bottom = true
                        break
                    case 'topleft':
                        this.top = true
                        this.left = true
                        break
                    case 'topright':
                        this.top = true
                        this.right = true
                        break
                    case 'bottomleft':
                        this.bottom = true
                        this.left = true
                        break
                    case 'bottomright':
                        this.bottom = true
                        this.right = true
                        break
                }
            },
            resetPositions(){
                this.top = false
                this.bottom = false
                this.left = false
                this.right = false
            }
        }
    }
</script>

<style scoped>

</style>
