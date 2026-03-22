import { SQL } from 'bun'
import { env } from '../config/env.js'

export const db = new SQL(env.databaseUrl)
