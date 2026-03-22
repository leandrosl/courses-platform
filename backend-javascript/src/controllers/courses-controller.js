import {
  createCourse,
  deleteCourse,
  getCourseById,
  listCourses,
  updateCourse,
} from '../repositories/courses-repository.js'
import {
  badRequest,
  json,
  noContent,
  notFound,
  validationProblem,
} from '../utils/http.js'
import { parseJsonBody, validateSaveCourseRequest } from '../utils/validation.js'

function parseCourseId(request) {
  const id = Number.parseInt(request.params.id, 10)
  return Number.isNaN(id) ? null : id
}

export async function getAllCourses() {
  const courses = await listCourses()
  return json(courses)
}

export async function getCourse(request) {
  const id = parseCourseId(request)
  if (id === null) {
    return notFound()
  }

  const course = await getCourseById(id)
  return course ? json(course) : notFound()
}

export async function createCourseHandler(request) {
  const payload = await parseJsonBody(request)
  if (payload === null) {
    return badRequest('A valid JSON body is required.')
  }

  const validation = validateSaveCourseRequest(payload)
  if (!validation.valid) {
    return validationProblem(validation.errors)
  }

  const course = await createCourse(validation.value)
  return json(course, {
    status: 201,
    headers: {
      Location: `/api/courses/${course.id}`,
    },
  })
}

export async function updateCourseHandler(request) {
  const id = parseCourseId(request)
  if (id === null) {
    return notFound()
  }

  const payload = await parseJsonBody(request)
  if (payload === null) {
    return badRequest('A valid JSON body is required.')
  }

  const validation = validateSaveCourseRequest(payload)
  if (!validation.valid) {
    return validationProblem(validation.errors)
  }

  const course = await updateCourse(id, validation.value)
  return course ? json(course) : notFound()
}

export async function deleteCourseHandler(request) {
  const id = parseCourseId(request)
  if (id === null) {
    return notFound()
  }

  const deleted = await deleteCourse(id)
  return deleted ? noContent() : notFound()
}
