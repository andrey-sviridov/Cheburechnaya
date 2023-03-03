<template>
  <div v-if="!currentUser" class="loginButtons">
    <v-btn
      :height="47"
      prepend-icon="mdi-login"
      @click="this.$refs.formDialog.showForm()"
    >
      Авторизация
    </v-btn>
  </div>
  <div v-else-if="currentUser" class="loginButtons" style="display: contents;">

    <v-menu open-on-hover >
      <template v-slot:activator="{ props }">

        <v-btn v-bind="props" :height="47">
            <span style=" padding-right: 10px">
                  {{`Привет, ${this.currentUser.firstName}!`}}
                </span>
          <v-avatar
            color="primary"
            :size="30"
          >
            A
          </v-avatar>
        </v-btn>


      </template>

      <v-list class="mt-1">
        <v-list-item class="mt-1">
          <v-btn
            :height="40"
            width="100%"
            variant="outlined"
            prepend-icon="mdi-cog"
            @click="this.isLogged = false"
            color="primary"
          >
            Параметры
          </v-btn>
        </v-list-item>
        <v-list-item>
          <v-btn
            :height="40"
            width="100%"
            variant="outlined"
            prepend-icon="mdi-login"
            @click="this.clearAuthorizedUser"
            color="red"
          >
            Выйти
          </v-btn>
        </v-list-item>
      </v-list>
    </v-menu>
  </div>

  <form-dialog ref="formDialog" size="30%" height="50%">
    <template v-slot:title>
      Авторизация
    </template>
    <template v-slot:body>
      <v-form ref="loginForm" v-model="fmLoginValidated">
        <v-text-field v-model="this.auth.login" label="Логин*" variant="underlined" :rules="checkRules" validate-on="input"/>
        <v-text-field v-model="this.auth.password" label="Пароль*" type="password" variant="underlined" :rules="checkRules" validate-on="input"/>
      </v-form>
    </template>
    <template v-slot:actions>
      <v-btn
        @click="this.login"
        variant="outlined"
        color="success"
        :disabled="!fmLoginValidated"
      >
        Авторизоваться
      </v-btn>
      <v-spacer/>
      <v-btn
        variant="outlined"
        @click="this.$refs.formDialog.hideForm()"
      >
        Закрыть
      </v-btn>
    </template>
  </form-dialog>
  <snack-bar-info ref="snackbar" />
</template>

<script>
import FormDialog from "@/components/FormDialog";
import {requiredField} from "@/plugins/rules";
import ConfirmationDialog from "@/components/ConfirmationDialog";
import ApiService from "@/services/api.service";
import SnackBarInfo from "@/components/SnackBarInfo";
import CryptoJS from 'crypto-js'

export default {
  name: "Authorization",
  components: {SnackBarInfo, FormDialog, ConfirmationDialog},
  data(){
    return{
      isLogged: false,
      fmLoginValidated: false,
      showWin: false,
      auth:{
        login: '',
        password: ''
      },
      currentUser: {
        id: null,
        userName: '',
        firstName: '',
        lastName: '',
      }
    }
  },

  mounted() {
    this.refreshAuthorizedUser()

  },
  methods:{
    encryptValue(value){
      const key = CryptoJS.enc.Utf8.parse(ApiService.encryptedKey());
      const encrypted = CryptoJS.AES.encrypt(value, key, {
        keySize: 128 / 8,
        iv: key,
        mode: CryptoJS.mode.CBC,
        padding: CryptoJS.pad.Pkcs7
      });
      console.log(encrypted);
      return encrypted.toString();
    },
    login(){
      let author = {login: this.encryptValue(this.auth.login), password: this.encryptValue(this.auth.password)};

      ApiService.post('Login', author).then((response)=>{
        if(!response.data) return alert('Данный пользователь не зарегистрирован')
        localStorage.setItem("authorizedUser", JSON.stringify(response.data))
        localStorage.setItem("accessToken", JSON.stringify(response.data.token))
        this.refreshAuthorizedUser(true)
        this.$refs.formDialog.hideForm()
        window.location.reload()
      })

      this.showWin = false;
      this.auth = this.clearObject(this.auth)
    },
    clearAuthorizedUser(){
      localStorage.removeItem('authorizedUser')
      window.location.reload()
    },
    refreshAuthorizedUser(isLogged){
      this.currentUser = JSON.parse(localStorage.getItem('authorizedUser'))
      if(this.currentUser === null) localStorage.removeItem('accessToken')

      if(isLogged === undefined)
        this.checkJwt()

      setInterval(this.checkJwt, 300000) //Интервал каждые 5 минут

    },
    checkJwt(){
      if(this.currentUser !== null) {
        ApiService.post('UpdateValidJwt').then((response) => {
          if (response.data.status === 1) {
            localStorage.setItem('accessToken', JSON.stringify(response.data.token))
            this.$refs.snackbar.showSnackbar(`токен обновлён до: ${response.data.validTo}`, 'success', 'topleft')
          } else if (response.data.status === 0) {
            localStorage.removeItem('accessToken')
            localStorage.removeItem('authorizedUser')
            this.currentUser = null
            this.$refs.snackbar.showSnackbar('токен очищен', 'success', 'topleft')
          }
        })
      }
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
    }
  },
  computed: {
    checkRules() {
      return [
        value => requiredField(value)
      ]
    }
  }
}
</script>

<style scoped>

</style>
