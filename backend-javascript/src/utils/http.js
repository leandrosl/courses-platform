export function json(data, init = {}) {
  return Response.json(data, init)
}

export function noContent() {
  return new Response(null, { status: 204 })
}

export function validationProblem(errors) {
  return Response.json(
    {
      type: 'https://tools.ietf.org/html/rfc9110#section-15.5.1',
      title: 'One or more validation errors occurred.',
      status: 400,
      errors,
    },
    { status: 400 },
  )
}

export function notFound() {
  return new Response(null, { status: 404 })
}

export function methodNotAllowed() {
  return Response.json({ message: 'Method not allowed.' }, { status: 405 })
}

export function badRequest(message) {
  return Response.json({ message }, { status: 400 })
}
