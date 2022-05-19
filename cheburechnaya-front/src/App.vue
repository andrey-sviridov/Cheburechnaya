<template>
    <v-app id="inspire">
        <v-app-bar
                app
                color="transparent"
                style="backdrop-filter: blur(2px)"
                flat
        >
            <v-container class="py-0 fill-height">
                <v-avatar
                        class="mr-10"
                        color="grey darken-1"
                        size="44"
                >
                    <v-img src="../src/assets/logo.png" />
                </v-avatar>
                <span
                    class="mr-5"
                >
                    Чебуречная на закате
                </span>
                <v-tab class="nav-btn" @click="$router.push({name: 'Main'})">
                    Главная
                </v-tab>
                <v-tab class="nav-btn" @click="$router.push({name: 'About'})">
                    Биография
                </v-tab>
                <v-tab class="nav-btn" @click="$router.push({name: 'Info'})">
                    Информация
                </v-tab>

                <v-spacer></v-spacer>
                <v-btn
                       style="height: auto"
                       @click="this.test"
                >
                    TEST
                </v-btn>
                <v-btn v-if="this.currentUser == null"
                       class="nav-btn"
                       @click="openLoginDialog"
                >
                    Войти
                </v-btn>
                <div v-if="this.currentUser != null">
                    <span>
                        Привет, {{this.currentUser.firstName}}!
                    </span>
                    <v-avatar
                            style="cursor: default;"
                            color="primary"
                            class="ml-5 mr-5"
                    >
                        {{this.currentUser.firstName !== undefined ? (this.currentUser.firstName.slice(0,1)).toUpperCase() : ''}}
                    </v-avatar>
                    <v-btn
                            @click="clearAuthorizedUser"
                            class="mr-3"
                            outlined
                    >
                        Выйти
                        <v-icon>mdi-logout</v-icon>
                    </v-btn>
                </div>
            </v-container>
        </v-app-bar>

        <v-main class="grey lighten-3">
            <v-container>
                <v-sheet
                        min-height="70vh"
                        rounded="lg"
                >
                    <transition name="fade">
                        <router-view class="router-view"/>
                    </transition>
                </v-sheet>
            </v-container>
        </v-main>
        <v-snackbar
                v-model="alert.show"
                top
                right
                color="success"
                transition="fade-transition"
        >
            {{alert.text}}
        </v-snackbar>
        <div class="text-center">
            <!--Окно регистрации-->
            <v-dialog
                    v-model="regDialog"
                    width="900"
                    persistent
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
                                            v-model.trim="registration.userName"
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
                                    Логин: {{registration.userName}}<br/>
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
                    persistent
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

            <snack-bar-info ref="infoSnack" />
        </div>
    </v-app>
</template>

<script>
    import ApiService from "./services/api.service";
    import SnackBarInfo from "./components/SnackBarInfo";
    export default {
        name: "Test",
        components: {SnackBarInfo},
        mounted() {
            this.refreshAuthorizedUser()
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
                    id: null,
                    userName: '',
                    firstName: '',
                    lastName: '',
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
                    userName: '',
                    password: '',
                },
                users:[
                    {
                        login: 'asd',
                        password: 'asd'
                    },
                    {
                        login: 'qwert',
                        password: 'q'
                    }
                ]
            }
        },
        methods:{
            test(){
                this.$refs.infoSnack.showSnackbar('asda','success','topright')

            },
            refreshAuthorizedUser(){
                this.currentUser = JSON.parse(localStorage.getItem('authorizedUser'))
                if(this.currentUser === null) localStorage.removeItem('accessToken')
            },
            clearAuthorizedUser(){
                localStorage.removeItem('authorizedUser')
                this.currentUser = null
            },
            validate () {
                this.$refs.form.validate();
            },
            openLoginDialog(){
                this.dialog = true;
            },
            login(){
                ApiService.post('Login', this.authorization).then((response)=>{
                    if(!response.data) return alert('Данный пользователь не зарегистрирован')
                    localStorage.setItem("authorizedUser", JSON.stringify(response.data))
                    localStorage.setItem("accessToken", JSON.stringify(response.data.token))
                    this.refreshAuthorizedUser()
                })

                this.dialog = false;
                this.authorization = this.clearObject(this.authorization)
            },
            showRegistrationDialog(){
                this.hideAuthorizationDialog()
                this.regDialog = true;
            },
            hideRegistrationDialog(){
                this.regDialog = false;
                this.registration = this.clearObject(this.registration)
            },
            hideAuthorizationDialog(){
                this.dialog = false;
                this.authorization = this.clearObject(this.authorization)
            },
            register(){
                ApiService.post('Register', this.registration).then(()=>{
                    this.$refs.infoSnack.showSnackbar('Регистрация успешно завершена','success','topright')
                    this.hideRegistrationDialog();
                })
            },
            clearObject(obj){
                for(let [key, value] of Object.entries(obj)){
                    switch (typeof value) {
                        case 'string':
                            obj[key] = ''
                            break;
                        case 'number':
                            obj[key] = null
                            break;
                        case ('object'):
                            if(value instanceof Date)
                                obj[key] = null
                            else
                                obj[key] = {}
                            break;
                        case 'boolean':
                            obj[key] = false
                            break;
                    }
                }
                return obj;
            },
        },
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
    .v-application .grey.lighten-3{
        background: transparent !important;

        /*Свечение вокруг компонента-страницы*/
        /*box-shadow: 0 0 50px 20px #ffffff24, 0 0 100px 59px #ffffff1c inset !important;*/
    }
    .theme--dark.v-sheet{
        background-color: transparent;
    }
    .theme--dark.v-application{
        background: transparent;
    }
    .appBar{
        background: transparent !important;
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


