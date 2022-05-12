<template>
  <div class="about">
    <div class="pa-3">
     <v-btn outlined @click="dialog.visible = true">
       Добавить пост
     </v-btn>
    </div>
    <div v-for="item in items" :key="item.id">
      <v-card class="card-post pa-1">
        <v-row>
          <v-card-title>
            {{item.title}}
          </v-card-title>
          <v-spacer/>
          <v-menu

                  offset-y
          >
            <template v-slot:activator="{ attrs, on }">
              <v-btn
                      small
                      class="white--text ma-5"
                      v-bind="attrs"
                      v-on="on"
              >
                <v-icon>mdi-dots-vertical</v-icon>
              </v-btn>
            </template>

            <v-list class="menu">
              <v-list-item link>
                <v-list-item-title>Изменить</v-list-item-title>
              </v-list-item>
              <v-list-item link
                           @click="confirmDelete(item)">
                <v-list-item-title
                >
                  Удалить
                </v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </v-row>
        <v-card-text
                v-html="item.text"
        ></v-card-text>
        <v-card-actions>
          <v-btn
                  @click="like(item)"
                  v-bind:class="{likedClass: item.youLiked}"
          >
            <p style="top: 7px; position:relative;">{{getCountLiked(item)}}</p>
            <v-icon
                    right
                    dark
            >
              mdi-heart
            </v-icon>
            Нравится
          </v-btn>
        </v-card-actions>
      </v-card>
    </div>
    <v-dialog
            max-width="1000px"
            v-model="dialog.visible"
            persistent
    >
      <v-form ref="form"
      >
        <v-card-title
        >
          Добавление поста
        </v-card-title>
        <v-divider />

        <v-card-text
        >
          <v-text-field
                  label="Заголовок"
                  required
                  outlined
                  v-model="dialog.content.title"
          ></v-text-field>
          <v-textarea
                  label="Текст"
                  outlined
                  v-model="dialog.content.text"
          ></v-textarea>
        </v-card-text>

        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer/>
          <v-btn
                  color="primary"
                  text
                  @click="addPost"
          >
            Сохранить
          </v-btn>
          <v-btn
                  color="primary"
                  text
                  @click="clearDialog"
          >
            Закрыть
          </v-btn>
        </v-card-actions>
      </v-form>
    </v-dialog>

    <confirmation-dialog ref="confirmation"/>
  </div>
</template>

<script>
  import ApiService from "../services/api.service";
  import ConfirmationDialog from "../components/ConfirmationDialog";
  export default {
    name: 'About',
    components: {ConfirmationDialog},
    mounted() {
      this.getList();
    },
    data(){
      return{
        items: [],
        dialog:{
          content:{
            title: '',
            text: ''
          },
          visible: false
        }
      }
    },
    methods:{
      addPost(){
        ApiService.post('NewPost', this.dialog.content).then(()=>{
          this.clearDialog()
          this.getList()
        }).catch((error)=>{
          switch (error.response.status) {
            case 401:
              alert('Для совершения данного действия нужно авторизоваться')
              break;
          }
        })
      },
      clearDialog(){
        this.dialog.visible = false
        this.dialog.content.title = ''
        this.dialog.content.text = ''
      },
      getCountLiked(val){
        let likes = this.items.find(x=>x.id === val.id)?.likeCount
        return  likes === 0 ? '' : likes
      },
      like(item){
        ApiService.put(`LikePost/${item.id}`).then(()=>{
          this.getList()
        })
      },
      unlike(item){
        ApiService.put(`UnlikePost/${item.id}`).then(()=>{
          this.getList()
        })
      },
      confirmDelete(item){
        this.$refs.confirmation.show(item)
      },
      getList(){
        ApiService.get(`GetPosts`).then((response)=>{
          this.items = response.data
        })
      }
    }
  }
</script>

<style scoped>
  .v-icon{
    color: white !important;
  }
  .likedClass .v-icon{
    color: #ff4d4d !important;
    animation: anim .5s;
  }
  @keyframes anim {
   0% {

   }
   50% {
     font-size: 30px;
   }
   100% {
   }
  }
</style>
