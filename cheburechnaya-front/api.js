import axios from 'axios'

const client = axios.create({
    baseURL: 'http://localhost:3000/',
    json: true
})

export default {
    async execute (method, resource, data) {
        return client({
            method,
            url: resource,
            data,
        }).then(req => {
            return req.data
        })
    },
    getPosts () {
        return this.execute('get', '/')
    },
    getPost (id) {
        return this.execute('get', `/${id}`)
    },
    createPost (data) {
        return this.execute('post', '/', data)
    },
    updatePost (id, data) {
        return this.execute('put', `/${id}`, data)
    },
    deletePost (id) {
        return this.execute('delete', `/${id}`)
    }
}