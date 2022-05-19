import axios from "axios";
import ApiService from '@/services/api.service'
// import store from '@/store/index'
import router from '@/router/index'
import qs from 'qs';

const AuthService = {

  init(baseURL) {
    axios.defaults.baseURL = baseURL;

    axios.defaults.paramsSerializer = params => {
      return qs.stringify(params, {arrayFormat: 'repeat'});
    };
  },


  signIn(login, password){

    return ApiService.post(`Login`, { username: login, password: password }).then(response => {

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


  _wipeData() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('userData');
  },

  signOut() {
    this._wipeData()

    //store.dispatch('clearWorkingContext')
    router.push( { name: "auth-login" } )
  },

  getUserId() {
    return  localStorage.getItem('userId');
  },

  getAccessToken() {
    return  localStorage.getItem('accessToken');
  },

  getServerTime() {
    return  localStorage.getItem('serverTime');
  },

  isLoggedOn() {
    return localStorage.getItem('accessToken') && localStorage.getItem('authorizedUser')
  }

}

export default AuthService;
