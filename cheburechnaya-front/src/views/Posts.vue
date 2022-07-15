<template>
    <v-app id="inspire">
        <v-main class="grey lighten-3" style="background-color: transparent !important; padding-top: 0;">
            <v-container>
                <v-row>
                    <v-col cols="2">
                        <v-sheet rounded="lg" style="background-color: transparent; position:fixed;">
                            <v-list color="transparent">
                                <div class="pa-3">
                                    <v-btn outlined @click="dialog.visible = true">
                                        Добавить пост
                                    </v-btn>
                                </div>
                                <v-divider class="my-2"></v-divider>
                                <div v-for="item in categories" :key="item.id">
                                    <v-switch :label="item.name" style="margin-bottom: -30px;"/>
                                </div>
                            </v-list>
                        </v-sheet>
                    </v-col>
                    <v-col style="overflow: auto">
                        <v-sheet
                                min-height="70vh"
                                rounded="lg"
                                style="background-color: transparent"
                        >
                            <div class="about ma-1">
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
                                                            class="white--text mt-8 mr-5"
                                                            v-bind="attrs"
                                                            v-on="on"
                                                    >
                                                        <v-icon>mdi-dots-vertical</v-icon>
                                                    </v-btn>
                                                </template>

                                                <v-list class="menu  text-center">
                                                    <v-list-item link>
                                                        <v-list-item-title class="primary--text"
                                                                           @click="editPost(item)"
                                                        >
                                                            Изменить
                                                        </v-list-item-title>
                                                    </v-list-item>
                                                    <v-list-item link @click="confirmDelete(item)">
                                                        <v-list-item-title class="red--text"
                                                        >
                                                            Удалить
                                                        </v-list-item-title>
                                                    </v-list-item>
                                                </v-list>
                                            </v-menu>
                                        </v-row>
                                        <v-card-text
                                                class="card-status-text grey--text"
                                        >
                                            {{getAuthorInfo(item)}}
                                        </v-card-text>
                                        <v-divider/>
                                        <v-card-text
                                                class="card-text"
                                                v-html="item.text"
                                        ></v-card-text>
                                        <v-divider/>
                                        <v-card-actions>
                                            <v-btn
                                                    small
                                                    @click="likeUnlike(item)"
                                                    v-bind:class="{likedClass: item.youLiked}"
                                                    class="light-blue--text transparent"
                                            >
                                                <p style="top: 7px; position:relative;">{{getCountLiked(item)}}</p>
                                                <v-icon
                                                        right
                                                        dark
                                                        class="light-blue--text"
                                                >
                                                    mdi-heart
                                                </v-icon>
                                                Нравится
                                            </v-btn>
                                        </v-card-actions>
                                    </v-card>
                                </div>
                                <v-dialog
                                        max-width="60%"
                                        v-model="dialog.visible"
                                        persistent
                                >
                                    <v-form ref="form"
                                            v-model="dialog.valid"
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
                                                :rules="[this.$valid.valRequired]"
                                                outlined
                                                v-model="editedItem.title"
                                            ></v-text-field>
                                            <v-textarea
                                                label="Текст"
                                                :rules="[this.$valid.valRequired]"
                                                outlined
                                                v-model="editedItem.text"
                                            >
                                            </v-textarea>
                                            <v-combobox :items="categories" v-model="editedItem.type" item-value="id" item-text="name"
                                                        :rules="[this.$valid.valRequired]"></v-combobox>
                                        </v-card-text>

                                        <v-divider></v-divider>
                                        <v-card-actions>
                                            <v-spacer/>
                                            <v-btn
                                                :disabled="!dialog.valid"
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
                                <progress-linear ref="progressLinear"/>
                                <snack-bar-info ref="infoSnack" />
                            </div>
                        </v-sheet>
                    </v-col>
                </v-row>
            </v-container>
        </v-main>
    </v-app>
</template>

<script>
    import ApiService from "../services/api.service";
    import ConfirmationDialog from "../components/ConfirmationDialog";
    import ProgressLinear from "../components/ProgressLinear";
    import moment from 'moment-with-locales-es6'
    import SnackBarInfo from "../components/SnackBarInfo";

    export default {
        name: 'About',
        components: {SnackBarInfo, ProgressLinear, ConfirmationDialog},
        mounted() {
            this.getList();
        },
        data(){
            return{
                categories:[
                    {
                        id:1,
                        name: 'Стихи'
                    },
                    {
                        id:2,
                        name: 'Рассказы'
                    },
                    {
                        id:3,
                        name: 'Прочее'
                    },
                ],
                editedItem:{
                    title: '',
                    text: '',
                    type: ''
                },
                items: [],
                dialog:{
                    content:{
                        title: '',
                        text: ''
                    },
                    visible: false,
                    valid: false
                }
            }
        },
        methods:{
            getAuthorInfo(item){
                return `${item.author != null ? `${item.author.firstName} ${item.author.lastName} (${item.author.userName})` : '(Пользователь не найден)'} ${moment(item.createdDate).format('DD MMMM yyyy в HH:mm')} ${item.isEdited ? '(изменено)' : ''}`
            },
            editPost(item){
                this.editedItem = Object.assign({}, item)
                this.editedItem.text = item.text.replaceAll('<br>','\n')
                this.dialog.visible = true
            },
            addPost(){
                if(!this.editedItem.id){
                    ApiService.post('NewPost', this.editedItem).then(()=>{
                        this.$refs.infoSnack.showSnackbar('Пост успешно добавлен','success', 'top')
                        this.clearDialog()
                        this.getList()
                    }).catch((error)=>{
                        switch (error.response.status) {
                            case 401:
                                alert('Для совершения данного действия нужно авторизоваться')
                                break;
                        }
                    })
                }else{
                    ApiService.put('EditPost', {
                        id: this.editedItem.id,
                        title: this.editedItem.title,
                        text: this.editedItem.text,
                        isEdited: true
                    }).then(()=>{
                        this.$refs.infoSnack.showSnackbar('Пост успешно изменён','success', 'top')
                        this.clearDialog()
                        this.getList()
                    }).catch((error)=>{
                        switch (error.response.status) {
                            case 401:
                                alert('Для совершения данного действия нужно авторизоваться')
                                break;
                        }
                    })
                }

            },
            clearDialog(){
                this.dialog.visible = false
                this.editedItem.title = ''
                this.editedItem.text = ''
            },
            getCountLiked(val){
                let likes = this.items.find(x=>x.id === val.id)?.likeCount
                return  likes === 0 ? '' : likes
            },
            likeUnlike(item){
                ApiService.put(`${item.youLiked ? 'UnlikePost' : 'LikePost'}/${item.id}`).then(()=>{
                    this.getList()
                })
            },
            confirmDelete(item){
                this.$refs.confirmation.showConfirm(() => {
                        ApiService.delete(`DeletePost/${item.id}`).then(()=>{
                            this.$refs.infoSnack.showSnackbar('Пост успешно удалён','success', 'top')
                            this.getList()
                        })
                    },item
                )
            },
            getList(){
                this.$refs.progressLinear.showLinearProgress('Загрузка списка...')
                ApiService.get(`GetPosts`).then((response)=>{
                    this.items = response.data
                    this.$refs.progressLinear.hideLinearProgress()
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