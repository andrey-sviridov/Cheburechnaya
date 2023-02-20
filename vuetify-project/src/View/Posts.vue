<template>
  <div class="grid">
<div>
  <v-sheet rounded="lg" style="background-color: transparent;">
    <div class="pa-3">
      <v-btn variant="outlined" >
        Добавить пост
      </v-btn>
    </div>
    <v-divider class="my-2"></v-divider>
    <div class="mb-2 switches">
      <v-switch label="Стихи" color="primary" />
    </div>
    <div class="mb-2 switches">
      <v-switch label="Рассказы" color="primary" />
    </div>
    <div class="mb-2 switches">
      <v-switch label="Прочее" color="primary" />
    </div>
  </v-sheet>
</div>
    <v-divider vertical/>
    <div>
      <v-card v-for="el in posts" :key="el.id" class="ma-5" style="text-align-last: start">
        <v-card-title>
          {{el.title}}
          <v-divider vertical/>

        </v-card-title>
        <v-card-text style="text-align-last: left; padding: 0 20px 0 20px;">{{ el.text }}</v-card-text>
        <v-divider style="margin-top: 10px"/>
        <v-card-actions>
          <div>
            <v-btn @click="likeUnlike(el)">
              <v-icon style="margin-top: -2px" :color="el.youLiked ? 'red' : 'black'">
                mdi-heart
              </v-icon>
              {{el.likeCount > 0 ? el.likeCount : ''}}
            </v-btn>
          </div>
        </v-card-actions>
      </v-card>

    </div>
    <v-divider vertical/>
    <div>1111</div>
  </div>
</template>

<script>
import ApiService from "@/services/api.service";
import axios from "axios";

export default {
  name: "Posts",
  data(){
    return{
      posts:[
        {
          id: 1,
          title: 'Заголовок 1',
          text: 'Текст первого поста',
          likeCount: 20,
          youLiked: true
        },
        {
          id: 2,
          title: 'Заголовок 2',
          text: 'Текст второго поста',
          likeCount: 15,
          youLiked: false
        },
        {
          id: 3,
          title: 'Заголовок 3',
          text: 'Текст третьего поста',
          likeCount: 28,
          youLiked: true
        },
      ]
    }
  },
  mounted() {
    this.getList();
  },
  methods: {
    getList(){
      ApiService.get(`GetPosts`).then((response)=>{
        console.log(response.data)
        this.posts = response.data
      })
    },
    likeUnlike(item){
      ApiService.put(`${item.youLiked ? 'UnlikePost' : 'LikePost'}/${item.id}`).then(()=>{
        this.getList()
      })
    },
  }
}
</script>

<style scoped>
.grid {
  display: grid;
  grid-template-columns: 20% 1px 58% 1px 20%;
  gap: 10px;
  height: 700px;
}
.switches{
  background: rgba(255,255,255,0.3);
  border: rgba(128,128,128,0.1) 1px solid;
  border-radius: 10px;
  height: 55px;
  padding: 0 0 0 15px
}
</style>
