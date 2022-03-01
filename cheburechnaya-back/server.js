import express from 'express'
import router from './routes/todo.routes.js'
import fs from 'fs'
import swaggerUI from 'swagger-ui-express'
import cors from 'cors'

const swaggerFile = JSON.parse(fs.readFileSync('./swagger/output.json'))

const app = express()
const corsOptions = {
    origin: 'http://localhost:8080',
    optionsSuccessStatus: 200,
    methods: "GET, POST, PUT, DELETE"
}

app.use(express.json())
app.use('/api-doc', swaggerUI.serve, swaggerUI.setup(swaggerFile))
app.use(cors(corsOptions))

app.use('/todos', router)
app.get('*',(req,res) => {
    res.send('Only /todos endpoint is available.')
})

app.use((err,req,res,next) => {
    console.log(err)
    const status = err.status || 500

    const message = err.message || 'Something went wrong. Try again later'
    res.status(status).json({message})
})

app.listen(3000,() => {
    console.log('server ready')
})