import {
  createCourseHandler,
  deleteCourseHandler,
  getAllCourses,
  getCourse,
  updateCourseHandler,
} from '../controllers/courses-controller.js'
import { getHealth } from '../controllers/health-controller.js'
import { methodNotAllowed, notFound } from '../utils/http.js'

export const routes = {
  '/health': {
    GET: getHealth,
    POST: methodNotAllowed,
    PUT: methodNotAllowed,
    DELETE: methodNotAllowed,
    PATCH: methodNotAllowed,
  },
  '/api/courses': {
    GET: getAllCourses,
    POST: createCourseHandler,
    PUT: methodNotAllowed,
    DELETE: methodNotAllowed,
    PATCH: methodNotAllowed,
  },
  '/api/courses/:id': {
    GET: getCourse,
    PUT: updateCourseHandler,
    DELETE: deleteCourseHandler,
    POST: methodNotAllowed,
    PATCH: methodNotAllowed,
  },
}

export function fallbackRoute() {
  return notFound()
}
