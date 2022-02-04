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
                        <span id="logoName" class="headerName">
                            ЧЕБУРЕЧНАЯ НА ЗАКАТЕ
                        </span>
                    </div>
                    <v-tabs id="navTabs">
                        <v-spacer>
                        </v-spacer>
                            <v-tab @click="$router.push({name: 'Main'})">Главная</v-tab>
                            <v-tab @click="$router.push({name: 'About'})">Биография</v-tab>
                            <v-tab @click="$router.push({name: 'Info'})">Информация</v-tab>

                        <v-spacer id="spacerRight"></v-spacer>
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
                            <v-icon
                                    @click="hideRegistrationDialog"
                            >
                                mdi-close
                            </v-icon>
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
                                                v-model.trim="registration.login"
                                                label="Логин"
                                                :rules="[this.$valid.valRequired]"
                                        />
                                        <v-text-field
                                                type="password"
                                                v-model.trim="registration.password"
                                                label="Пароль"
                                                :rules="[this.$valid.valRequired]"
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
                                                v-model.trim="registration.firstName"
                                                label="Имя"
                                                :rules="[this.$valid.valRequired]"
                                        />
                                        <v-text-field
                                                v-model.trim="registration.lastName"
                                                label="Фамилия"
                                                :rules="[this.$valid.valRequired]"
                                        />
                                        <v-text-field
                                                v-model.trim="registration.email"
                                                label="Электронная почта"
                                                :rules="[this.$valid.valRequired, this.$valid.emailValidator]"
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
                                            @click="register"
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

                            <v-icon
                                    @click="hideAuthorizationDialog"
                            >
                                mdi-close
                            </v-icon>
                        </v-card-title>
                        <v-divider />

                        <v-card-text
                        >
                            <v-text-field
                                    v-model="authorization.login"
                                    :rules="[this.$valid.valRequired]"
                                    label="Логин"
                                    required
                            ></v-text-field>
                            <v-text-field
                                    v-model="authorization.password"
                                    label="Пароль"
                                    type="password"
                                    :rules="[this.$valid.valRequired]"
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
                                    @click="login"
                            >
                                Авторизоваться
                            </v-btn>
                        </v-card-actions>
                    </v-form>
                </v-dialog>

                <alert-window
                >
                    <div slot="title">
                        <p>SLOT TITLE</p>
                    </div>
                    <div slot="body">
                        <v-text-field>slot TF</v-text-field>
                    </div>
                    <div slot="buttons" slot-scope="changeVisible">
                        <v-btn @click="changeVisible(false)">slot button</v-btn>
                    </div>
                </alert-window>

            </div>
        </div>
        <v-snackbar
                v-model="alert.show"
                top
                right
                color="success"
                transition="fade-transition"
        >
            {{alert.text}}
        </v-snackbar>
    </v-app>
</template>

<script>
    import AlertWindow from "./components/AlertWindow";
    export default {
        name: "App",
        components: {AlertWindow},
        setup(){
        },
        data(){
            return{
                isSnackbarTopRightVisible: false,
                alert: {
                  color: 'success',
                  show: false,
                  text: 'sads'
                },
                e1: 1,
                logged: false,
                currentUser: {
                    currentLogin: '',
                    currentUserName: '',
                },
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
                users:[
                ]
            }
        },
        methods:{
            validate () {
                this.$refs.form.validate();
            },
            openLoginDialog(){
                this.dialog = true;
            },
            login(){
                let login = this.users.find(x=>x.login === this.authorization.login);
                if(login?.password === this.authorization.password){
                    alert('OK')
                }else alert('BAD')

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
                this.registration.login = '';
                this.registration.password = '';
                this.registration.firstName = '';
                this.registration.lastName = '';
                this.registration.email = '';
            },
            hideAuthorizationDialog(){
                this.dialog = false;
                this.authorization.login = '';
                this.authorization.password = '';
            },
            register(){
                let newUser = Object.assign({},this.registration);
                this.users.push(newUser);

                this.alert.color = 'success';
                this.alert.show = true;
                this.alert.text = 'Регистрация успешно завершена';
                this.hideRegistrationDialog();
            },
        }
    }
</script>

<style scoped>
    @import "styles/buttons.css";
    @import "styles/app-bar.css";

    .headerName{
        position:absolute;
        top: 40%;
        color: white;
        margin-left: 80px;
        width: 200px;
        box-shadow: black;
    }
    .v-application{
        background: transparent !important;

        box-shadow: 0 0 50px 20px #ffffff24, 0 0 100px 59px #ffffff1c inset !important;
    }
    .theme--dark.v-sheet{
        background: transparent;
    }
    .appBar{
        background: transparent !important;
        /*backdrop-filter: blur(1px);*/
    }
    .v-divider{
        background: white;
    }
    .v-application--wrap{
    }

    @media(max-width: 1308px){
        #logoName{
            display: none;
        }
    }
    @media (max-width: 834px) {

        #spacerRight{
            display: none;
        }
    }
    @media (max-width: 738px) {

        #navTabs{
            flex-direction: column;
            align-items: center;
        }
    }
    .router-view{
        margin-top: 20px;
    }
</style>
<style>
    @import "styles/customVariables.css";

    body{
        background: url("../src/assets/wall.jpg") no-repeat fixed;
        background-size: 100%;
        padding: 0 10% 5% 10%;
    }
</style>


