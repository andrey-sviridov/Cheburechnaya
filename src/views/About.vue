<template>
  <div class="about">
    <div v-for="item in items" :key="item.title">
      <v-card>
        <v-card-title>
          {{item.title}}
        </v-card-title>
        <v-card-text>
          {{item.text}}
        </v-card-text>
        <v-card-subtitle>asdsadasd</v-card-subtitle>
        <v-card-actions>
          <v-btn
                  @click="like(item)"
                  v-bind:class="{likedClass: item.liked}"
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
  </div>
</template>

<script>
  export default {
    name: 'About',
    data(){
      return{
        items: [
          {
            title: 'Стишок о бомже',
            text: 'Обосрался вдруг бомжара, оказался это шкипер',
            liked: true,
            likedCount: 558
          },
          {
            title: 'Стишок про говно',
            text: 'Выпустили близы аддон, а оказался он говном',
            liked: false,
            likedCount: 0
          },
          {
            title: 'История о крабе',
            text: 'Пришёл краб в бар а там хабар',
            liked: false,
            likedCount: 0
          },
          {
            title: 'Черубек из Кахастана',
            text: 'жил-был и не тужил',
            liked: false,
            likedCount: 0
          },
        ],
      }
    },
    methods:{
      getCountLiked(val){
        return val.likedCount === 0 ? '' : val.likedCount
      },
      like(item){
        item.liked = !item.liked;
        if(item.liked){
          item.likedCount = ++item.likedCount
        }else{
          item.likedCount = --item.likedCount
        }
      }
    }
  }
</script>

<style scoped>
  .v-icon{
    color: white !important;
  }
  .likedClass .v-icon{
    color: red !important;
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
