# Backend JavaScript com Bun e Docker

API REST em Bun com PostgreSQL, espelhando as rotas e funcionalidades da API em C#.

## Stack

- Bun
- Bun.serve
- Bun.SQL
- PostgreSQL 17
- Docker Compose

## Rotas

- `GET /health`
- `GET /api/courses`
- `GET /api/courses/{id}`
- `POST /api/courses`
- `PUT /api/courses/{id}`
- `DELETE /api/courses/{id}`

## Como subir

```bash
cd backend-javascript
docker compose up --build
```

A API ficará disponível em `http://localhost:8080`.

## Observações

- Na inicialização, a aplicação cria a tabela `courses` automaticamente se ela ainda não existir.
- O contrato do recurso `Course` segue o mesmo formato JSON da API C#: `id`, `title`, `description`, `workloadHours` e `createdAtUtc`.
