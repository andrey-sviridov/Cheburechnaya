import {join, dirname} from 'path'
import {fileURLToPath} from 'url'
import swaggerAutogen from 'swagger-autogen'

const _dirname = dirname(fileURLToPath(import.meta.url))

const doc = {
    info:{
        title: 'CRUD',
        description: 'CRUD операции с объектом'
    },
    definitions:{
        List:{
            id: 1,
            title: 'exampleTitle',
            text: 'exampleText',
            liked: false,
            likedCount: 0
        },
        CreateObject:{
            title: 'exampleTitle',
            text: 'exampleText',
            liked: false,
            likedCount: 0
        },
        Changes:{
            title: 'exampleTitle',
            text: 'exampleText',
            liked: false,
            likedCount: 0
        },
        SpecificlyObject:{
            id: 1
        }
    },
    host: 'localhost:3000',
    schemes: ['http']
}

const outputFile = join(_dirname, 'output.json')

const endpointsFiles = [join(_dirname, '../server.js')]

swaggerAutogen()(outputFile, endpointsFiles, doc).then(({ success }) =>{
    console.log(`Generated: ${success}`)
})