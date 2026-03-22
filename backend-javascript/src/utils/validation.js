function isPlainObject(value) {
  return typeof value === 'object' && value !== null && !Array.isArray(value)
}

export async function parseJsonBody(request) {
  try {
    return await request.json()
  } catch {
    return null
  }
}

export function validateSaveCourseRequest(payload) {
  if (!isPlainObject(payload)) {
    return {
      valid: false,
      errors: {
        body: ['A valid JSON object is required.'],
      },
    }
  }

  const errors = {}
  const title = typeof payload.title === 'string' ? payload.title.trim() : ''
  const description = payload.description
  const workloadHours = payload.workloadHours

  if (!title) {
    errors.title = ['Title is required.']
  } else if (title.length > 200) {
    errors.title = ['Title must be 200 characters or fewer.']
  }

  if (description !== undefined && description !== null && typeof description !== 'string') {
    errors.description = ['Description must be a string.']
  } else if (typeof description === 'string' && description.length > 1000) {
    errors.description = ['Description must be 1000 characters or fewer.']
  }

  if (!Number.isInteger(workloadHours) || workloadHours <= 0) {
    errors.workloadHours = ['WorkloadHours must be greater than zero.']
  }

  if (Object.keys(errors).length > 0) {
    return { valid: false, errors }
  }

  return {
    valid: true,
    value: {
      title,
      description: typeof description === 'string' ? description.trim() : null,
      workloadHours,
    },
  }
}
