import { json } from '../utils/http.js'

export function getHealth() {
  return json({ status: 'ok' })
}
