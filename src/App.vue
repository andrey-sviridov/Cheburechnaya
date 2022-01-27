<template>
    <v-app class="v-appl">
        <div id="genDiv">
                <v-app-bar
                    id="appBarNav"
                    class="appBar"
                    style="z-index: 1"
                >
                    <div
                            style=" position:absolute; width: auto; cursor: pointer; z-index: 1"
                    >
                        <v-img src="../src/assets/logo.png" style="width: 70px;" />
                        <span style="position:absolute; top: 33%; color: white; margin-left: 80px; width: 200px">
                            ЧЕБУРЕЧНАЯ НА ЗАКАТЕ
                        </span>
                    </div>
                    <v-tabs>
                        <v-spacer>
                        </v-spacer>
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
                <!--Окно регистрации-->
                <v-dialog
                        v-model="regDialog"
                        width="900"
                >
                    <v-form ref="form"
                            v-model="valid"
                    >
                        <v-card-title
                        >
                            Регистрация
                            <v-spacer/>
                            <v-btn
                                    outlined
                                    @click="hideRegistrationDialog"
                            >
                                X
                            </v-btn>
                        </v-card-title>
                        <v-stepper v-model="e1">
                            <v-stepper-header>
                                <v-stepper-step
                                        :complete="e1 > 1"
                                        step="1"
                                >
                                    Шаг 1: Данные авторизации
                                </v-stepper-step>

                                <v-divider></v-divider>

                                <v-stepper-step
                                        :complete="e1 > 2"
                                        step="2"
                                >
                                    Шаг 2: Контактные данные
                                </v-stepper-step>

                                <v-divider></v-divider>

                                <v-stepper-step
                                        :complete="e1 > 2"
                                        step="3"
                                >
                                    Шаг 3: Подтверждение регистрации
                                </v-stepper-step>
                            </v-stepper-header>

                            <v-stepper-items>
                                <v-stepper-content step="1">
                                    <v-form
                                            v-model="regValid"
                                            class="mb-12"
                                            color="grey lighten-1"
                                            height="200px"
                                    >
                                        <v-text-field
                                                type="login"
                                                v-model="registration.login"
                                                label="Логин"
                                                :rules="[valRequired]"
                                        />
                                        <v-text-field
                                                type="password"
                                                v-model="registration.password"
                                                label="Пароль"
                                                :rules="[valRequired]"
                                        />
                                    </v-form>

                                    <v-btn
                                            color="primary"
                                            @click="e1 = 2, regValid = !regValid, validate"
                                            :disabled="!regValid"
                                    >
                                        Далее
                                    </v-btn>

                                    <v-btn
                                        :disabled="e1 === 1"
                                        @click="--e1"
                                    >
                                        Назад
                                    </v-btn>
                                </v-stepper-content>

                                <v-stepper-content step="2">
                                    <v-form
                                            v-model="regValid"
                                            class="mb-12"
                                            color="grey lighten-1"
                                            height="200px"
                                    >
                                        <v-text-field
                                                v-model="registration.firstName"
                                                label="Имя"
                                                :rules="[valRequired]"
                                        />
                                        <v-text-field
                                                v-model="registration.lastName"
                                                label="Фамилия"
                                                :rules="[valRequired]"
                                        />
                                        <v-text-field
                                                v-model="registration.email"
                                                label="Электронная почта"
                                                :rules="[valRequired, emailValidator]"
                                        />
                                    </v-form>


                                    <v-btn
                                            color="primary"
                                            @click="e1 = 3"
                                            :disabled="!regValid"
                                    >
                                        Далее
                                    </v-btn>

                                    <v-btn
                                            :disabled="e1 === 1"
                                            @click="--e1, regValid = true"
                                    >
                                        Назад
                                    </v-btn>
                                </v-stepper-content>
                                <v-stepper-content step="3">
                                    <v-form
                                            class="mb-12"
                                            color="grey lighten-1"
                                            height="250px"
                                    >
                                            Логин: {{registration.login}}<br/>
                                            Имя: {{registration.firstName}}<br/>
                                            Фамилия: {{registration.lastName}}<br/>
                                            Электронная почта: {{registration.email}}<br/>
                                    </v-form>


                                    <v-btn
                                            color="success"
                                    >
                                        Зарегистрироваться
                                    </v-btn>

                                    <v-btn
                                            :disabled="e1 === 1"
                                            @click="--e1, regValid = true"
                                    >
                                        Назад
                                    </v-btn>
                                </v-stepper-content>
                            </v-stepper-items>
                        </v-stepper>
                    </v-form>
                </v-dialog>
                <!--Окно авторизации-->
                <v-dialog
                        v-model="dialog"
                        width="500"
                >
                    <v-form ref="form"
                            v-model="valid"
                    >
                        <v-card-title
                        >
                            Авторизация
                            <v-spacer/>
                            <v-btn
                                    outlined
                                    @click="hideAuthorizationDialog"
                            >
                                X
                            </v-btn>
                        </v-card-title>
                        <v-divider />

                        <v-card-text
                        >
                            <v-text-field
                                    v-model="authorization.login"
                                    :rules="[valRequired]"
                                    label="Логин"
                                    required
                            ></v-text-field>
                            <v-text-field
                                    v-model="authorization.password"
                                    label="Пароль"
                                    type="password"
                                    :rules="[valRequired]"
                                    required

                            />
                        </v-card-text>

                        <v-divider></v-divider>
                        <v-card-actions>
                            <v-btn
                                    color="primary"
                                    text
                                    @click="showRegistrationDialog"
                            >
                                Регистрация
                            </v-btn>
                            <v-spacer></v-spacer>
                            <v-btn
                                    :disabled="!valid"
                                    color="primary"
                                    text
                                    @click="hideLoginDialog"
                            >
                                Авторизоваться
                            </v-btn>
                        </v-card-actions>
                    </v-form>
                </v-dialog>
            </div>
        </div>
    </v-app>
</template>

<script>
    import { valRequired, emailValidator } from '@/validations'
    export default {
        name: "App",
        setup(){
            return{
            }
        },
        data(){
            return{
                e1: 1,
                regValid: true,
                valid: true,
                regDialog: false,
                dialog: false,
                authorization:{
                    login: '',
                    password: '',
                },
                registration:{
                    firstName: '',
                    lastName: '',
                    email: '',
                    login: '',
                    password: '',
                },

                valRequired,
                emailValidator
            }
        },
        methods:{
            validate () {
                this.$refs.form.validate()
            },
            openLoginDialog(){
                this.dialog = true;
            },
            hideLoginDialog(){
                this.dialog = false;
                this.authorization.login = '';
                this.authorization.password = '';
            },
            showRegistrationDialog(){
                this.hideAuthorizationDialog()
                this.regDialog = true;
            },
            hideRegistrationDialog(){
                this.regDialog = false;
                this.registration.login = ''
                this.registration.password = ''
                this.registration.firstName = ''
                this.registration.lastName = ''
                this.registration.email = ''
            },
            hideAuthorizationDialog(){
                this.dialog = false;
                this.authorization.login = ''
                this.authorization.password = ''
            },
        }
    }
</script>

<style scoped>
    @import "styles/buttons.css";
    @import "styles/customVariables.css";

    .v-application{
        background: transparent !important;
    }
    .appBar{
        background: transparent !important;
        box-shadow: none !important;
        backdrop-filter: blur(2px);
    }

        /*Реализация плавного перехода*/

    /*Анимация при появлении*/
    .fade-enter-active {
        animation: inPage .6s;
    }

    /*Анимация при исчезании*/
    .fade-leave-active {
        animation: outPage .2s;
    }

    @keyframes inPage {
        from {
            transform: translateY(-1%);
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
            transform: translateY(1%);
            opacity: 0;
        }
    }

</style>
<style>
    body{
        padding: 0 10% 5% 10%;
    }

</style>


