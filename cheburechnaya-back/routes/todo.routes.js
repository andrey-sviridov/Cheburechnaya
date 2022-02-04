import {Router} from 'express'
import db from '../db/index.js'

const router = Router()


router.get('/', async (req,res,next) => {
    // описание роута
    // #swagger.description = 'Get all todos'
    // возвращаемый ответ
    /* #swagger.responses[200] = {
        // описание ответа
        description: 'Array of all todos',
        // схема ответа - ссылка на модель
        schema: { $ref: '#/definitions/Todos' }
    } */

    try {
        await db.read()

        if (db.data.length) {
            res.status(200).json(db.data)
        } else {
            res.status(200).json({message: 'There are no todos'})
        }
    } catch (e) {
        console.log('*** Get all todos exception')
        next(e)
    }
})

router.get('/:id',async (req,res,next) => {
    // #swagger.description = 'Get object by ID'
    // параметр запроса
    /* #swagger.parameters['id'] = {
      // описание параметра
      description: 'Existing object ID',
      // тип параметра
      type: 'string',
      // является ли параметр обязательным?
      required: true
    } */
    /* #swagger.responses[200] = {
        description: 'Object with provided ID',
        schema: { $ref: '#/definitions/List' }
    } */

    const id = parseInt(req.params.id)

    try {
        await db.read()
        if (!db.data.length) {
            return res.status(400).json({message: 'There are not todos'})
        }

        const todo = db.data.find((t)=>t.id === id)

        if (!todo) {
            return res.status(400).json({message: 'There is no todo with prodided ID'})
        }

        res.status(200).json(todo)
    } catch (e) {
        console.log('*** Get todo by ID exception')
        next(e)
    }
})

router.post('/',async (req,res,next) => {
    // #swagger.description = 'Create new object'
    // тело запроса
    /* #swagger.parameters['new object'] = {
      in: 'body',
      description: 'Create a new object',
      type: 'object',
      required: true,
      schema: { $ref: '#/definitions/CreateObject' }
    } */
    /* #swagger.responses[201] = {
        description: 'Array of new todos',
        schema: { $ref: '#/definitions/CreateObject' }
    } */

    const reqBody = req.body
    console.log(reqBody)

    if (!reqBody) {
        return res.status(400).json({message: 'New todo text must be provided'})
    }

    try {
        await db.read()

        const newTodo = {
            id: db.data.length + 1,
            title: reqBody.title,
            text: reqBody.text,
            liked: reqBody.liked,
            likedCount: reqBody.likedCount
        }

        db.data.push(newTodo)
        
        await db.write()

        res.status(201).json(db.data)
    } catch (e) {
        console.log('*** Create Todo exception')
        next(e)
    }
})

router.put('/:id',async (req,res,next) => {
    // #swagger.description = 'Update existing object'
    /* #swagger.parameters['id'] = {
      description: 'Existing object ID',
      type: 'string',
      required: true
    } */
    /* #swagger.parameters['Changes'] = {
      in: 'body',
      description: 'Existing object changes',
      type: 'object',
      required: true,
      schema: { $ref: '#/definitions/Changes' }
    } */
    /* #swagger.responses[201] = {
      description: 'Array of existing object',
      schema: { $ref: '#/definitions/Changes' }
    } */

    const id = parseInt(req.params.id)

    if (!id) {
        return res.status(400).json({message: 'Existing todo ID must be provided'})
    }

    const changes = req.body
    console.log('changes', req.body)

    if (!changes) {
        return res.status(400).json({message: 'Changes must be provided'})
    }

    try {
        await db.read()

        const todo = db.data.find((t) => t.id === id)

        if (!todo) {
            return res.status(400).json({message: 'There is no todo with provided ID'})
        }

        const updatedTodo = {...todo, ...changes}

        const newTodos = db.data.map((t)=>(t.id === id ? updatedTodo : t))

        db.data = newTodos

        await db.write()

        res.status(201).json(db.data)
    } catch (e) {
        console.log('*** update todo exception')
        next(e)
    }
})

router.delete('/:id',async (req,res,next) => {
    // #swagger.description = 'Remove existing object'
    /* #swagger.parameters['id'] = {
      description: 'Existing object ID',
      type: 'string',
      required: true
    } */
    /* #swagger.responses[201] = {
      description: 'Delete the specificly existing object',
      schema: { $ref: '#/definitions/SpecificlyObject' }
    } */

    const id = parseInt(req.params.id)

    if (!id) {
        return res.status(400).json({message: 'Existing todo ID must be provided'})
    }

    try {
        await db.read()

        const todo = db.data.find((t)=> t.id === id)
        
        if (!todo) {
            return res.status(400).json({message: 'There is no todo with provided ID'})
        }

        const newTodos = db.data.filter((t)=> t.id !== id)

        db.data = newTodos

        await db.write()

        res.status(201).json(db.data)
    } catch (e) {
        console.log('*** remove todo exception')
        next(e)
    }
})


export default router