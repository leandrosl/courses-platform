const port = Number.parseInt(process.env.PORT ?? '8080', 10)

if (Number.isNaN(port) || port <= 0) {
  throw new Error('PORT must be a positive number.')
}

const databaseUrl = process.env.DATABASE_URL

if (!databaseUrl) {
  throw new Error('DATABASE_URL is not configured.')
}

export const env = {
  port,
  databaseUrl,
}
