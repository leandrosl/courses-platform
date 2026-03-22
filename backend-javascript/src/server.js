import { env } from './config/env.js'
import { ensureCoursesTable } from './repositories/courses-repository.js'
import { fallbackRoute, routes } from './routes/index.js'

await ensureCoursesTable()

const server = Bun.serve({
  hostname: '0.0.0.0',
  port: env.port,
  routes,
  fetch: fallbackRoute,
  error(error) {
    console.error(error)
    return Response.json({ message: 'Internal Server Error' }, { status: 500 })
  },
})

console.log(`Bun API listening on ${server.url}`)
