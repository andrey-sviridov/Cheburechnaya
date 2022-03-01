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
                        size="32"
                >
                    <v-img src="../assets/logo.png" style="width: 70px;" />
                </v-avatar>
                <span id="logoName" class="headerName">
                    Чебуречная на закате
                </span>
                <v-divider vertical class="ml-10"/>
                <v-tab @click="$router.push({name: 'Main'})">Главная</v-tab>
                <v-tab @click="$router.push({name: 'About'})">Биография</v-tab>
                <v-tab @click="$router.push({name: 'Info'})">Информация</v-tab>

                <v-spacer></v-spacer>

                <v-responsive max-width="260">
                    <v-text-field
                            dense
                            flat
                            hide-details
                            rounded
                            solo-inverted
                    ></v-text-field>
                </v-responsive>
                <v-btn
                        style="height: auto"
                        @click="openLoginDialog"
                >
                    <v-icon>mdi-account</v-icon>
                    Войти
                </v-btn>
            </v-container>
        </v-app-bar>

        <v-main class="grey lighten-3">
            <v-container>
                <v-row>
                    <v-col cols="2">
                        <v-sheet rounded="lg">
                            <v-list color="transparent">
                                <v-list-item
                                        v-for="n in 5"
                                        :key="n"
                                        link
                                >
                                    <v-list-item-content>
                                        <v-list-item-title>
                                            List Item {{ n }}
                                        </v-list-item-title>
                                    </v-list-item-content>
                                </v-list-item>

                                <v-divider class="my-2"></v-divider>

                                <v-list-item
                                        link
                                        color="grey lighten-4"
                                >
                                    <v-list-item-content>
                                        <v-list-item-title>
                                            Refresh
                                        </v-list-item-title>
                                    </v-list-item-content>
                                </v-list-item>
                            </v-list>
                        </v-sheet>
                    </v-col>

                    <v-col>
                        <v-sheet
                                min-height="70vh"
                                rounded="lg"
                        >
                            <transition name="fade">
                                <router-view class="router-view"/>
                            </transition>
                        </v-sheet>
                    </v-col>
                </v-row>
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



        </div>
    </v-app>
</template>

<script>
    export default {
        name: "Test",
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