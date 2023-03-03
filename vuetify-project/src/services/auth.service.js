import axios from "axios";
import ApiService from "@/services/api.service";


const AuthService = {
  init(baseURL) {
    axios.defaults.baseURL = baseURL;
  },
  signIn(login, password) {

    return ApiService.post(`Login`, {username: login, password: password}).then(response => {

      localStorage.setItem('accessToken', response.data.token)
      localStorage.setItem('userId', response.data.id)
      //localStorage.setItem('serverTime', response.data.serverTime)


      return new Promise((resolve, reject) => {
        if (response.data.success) {
          resolve(response.data)
        } else {
          reject(response.data)
        }
      })

    })
  },
  _clearData() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('userData');
  },

  signOut() {
    this._clearData()

    //store.dispatch('clearWorkingContext')
    this.$vueRouter.to({name: "auth-login"})
  },

  getUserId() {
    return localStorage.getItem('userId');
  },

  getAccessToken() {
    return localStorage.getItem('accessToken');
  },
  isLoggedOn() {
    return localStorage.getItem('accessToken') && localStorage.getItem('authorizedUser')
  }
}

export default AuthService;
