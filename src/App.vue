<template>
    <v-app class="v-appl">
        <div id="genDiv">
                <v-app-bar
                    id="appBarNav"
                    class="appBar"
                    style="z-index: 1"
                >
                    <v-tabs>
                        <v-spacer></v-spacer>
                            <v-tab @click="$router.push({name: 'Main'})">Главная</v-tab>
                            <v-tab @click="$router.push({name: 'About'})">Биография</v-tab>
                            <v-tab @click="$router.push({name: 'Info'})">Информация</v-tab>
                        <v-spacer></v-spacer>
                        <v-btn
                                style="height: auto"
                                @click="openLoginDialog"
                        >
                            <v-icon>mdi-account</v-icon>
                            Войти
                        </v-btn>
                    </v-tabs>
                </v-app-bar>
            <div class="router-div">
                <transition name="fade">
                    <router-view class="router-view"/>
                </transition>
            </div>


            <div class="text-center">
                <v-dialog
                        v-model="dialog"
                        width="500"
                >
                    <v-card>
                        <v-card-title
                                style="justify-content: center"
                        >
                            Авторизация
                        </v-card-title>

                        <v-card-text
                        >
                            <v-text-field
                                    label="Логин"
                                    :rules="[required]"
                            />
                            <v-text-field label="Пароль" type="password"/>
                        </v-card-text>

                        <v-divider></v-divider>

                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn
                                    color="primary"
                                    text
                                    @click="dialog = false"
                            >
                                Авторизоваться
                            </v-btn>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
            </div>
        </div>
    </v-app>
</template>

<script>
    import { required } from '@/validations.js'
    export default {
        name: "App",
        setup(){
            return{
                required
            }
        },
        data(){
            return{
                dialog: false,
            }
        },
        methods:{
            openLoginDialog(){
                this.dialog = true;
            }
        }
    }
</script>

<style scoped>
    @import "styles/buttons.css";

    .v-application{
        background: transparent !important;
    }
    .appBar{
        background: transparent !important;
        box-shadow: none !important;
        backdrop-filter: blur(2px);
    }
    .router-div{
    }
    .router-view{
        position: fixed;
        width: 80%;
    }

        /*Реализация плавного перехода*/

    /*Анимация при появлении*/
    .fade-enter-active {
        animation: inPage .3s;
    }

    /*Анимация при исчезании*/
    .fade-leave-active {
        animation: outPage .3s;
    }

    @keyframes inPage {
        from {
            transform: translateY(-100%);
            opacity: 0;
        }
        to {
            transform: translateY(0);
            opacity: 100%;
        }
    }
    @keyframes outPage {
        from {
            transform: translateY(0);
            opacity: 100%;
        }
        to {
            transform: translateY(100%);
            opacity: 0;
        }
    }
</style>
<style>
    body{
        padding: 0 10% 5% 10%;
    }

</style>


