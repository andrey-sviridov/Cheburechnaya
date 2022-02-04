import Vue from 'vue'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'

Vue.use(Vuetify)

const opts = {
    theme:{
        options: {
            customProperties: true
        },
        dark: true,
        themes:{
            dark:{

            }
        }
    }
}

export default new Vuetify(opts)