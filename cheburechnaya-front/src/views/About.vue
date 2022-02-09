<template>
  <div class="about">
    <v-btn @click="getList">axios</v-btn>
    <div v-for="item in items" :key="item.title">
      <v-card>
        <v-card-title>
          {{item.title}}
        </v-card-title>
        <v-card-text>
          {{item.text}}
        </v-card-text>
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
  import axios from 'axios'
  export default {
    name: 'About',
    mounted() {
      this.getList();
    },
    data(){
      return{
        items: [],
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
      },
      getList(){
        axios.get(`${this.$url}`).then((response)=>{
          this.items = response.data
          console.log(response.data)
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