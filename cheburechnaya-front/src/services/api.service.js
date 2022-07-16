import axios from "axios";
import AuthService from "./auth.service";
//import WorkingContext from "../store/WorkingContext";

const ApiService = {

    init(baseURL) {
        axios.defaults.baseURL = baseURL;
    },

    setHeaders() {
        const jwtToken = AuthService.getAccessToken()?.replaceAll('"','')
        axios.defaults.headers.common["UserId"] = 0;
        axios.defaults.headers.common["Authorization"] = `Bearer ${jwtToken}`;

        if (AuthService.isLoggedOn()) {
            axios.defaults.headers.common["UserId"] = JSON.parse(localStorage.getItem("authorizedUser"))?.id
            console.log(axios.defaults.headers.common["UserId"])
        }
    },

    get(resource, config) {
        axios.interceptors.request.use(function(config) {
            // change the url scheme from http to https
            config.url = config.url.replace('https://', 'http://')

            return config
        })
        this.setHeaders()
        return axios.get(`/${resource}` , config);
    },

    post(resource, data) {
        this.setHeaders()
        return axios.post(`/${resource}`, data);
    },

    put(resource, data) {
        this.setHeaders()
        return axios.put(`/${resource}`, data);
    },

    delete(resource) {
        this.setHeaders()
        return axios.delete(`/${resource}`);
    },

    customRequest(data) {
        return axios(data);
    },

}

export default ApiService;
