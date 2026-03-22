# Backend C# com Docker

Este diretório sobe uma API RESTful em ASP.NET Core com PostgreSQL via Docker Compose.

## Stack

- ASP.NET Core / .NET 10
- Entity Framework Core 10 com Npgsql
- PostgreSQL 17
- Docker Compose

## Estrutura

```text
backend-csharp/
├── docker-compose.yml
├── Dockerfile
└── src/CoursesPlatform.Api
```

## Como subir

1. Copie o arquivo de ambiente:

```bash
cp .env.example .env
```

2. Suba os containers:

```bash
docker compose up --build
```

3. A API ficará disponível em:

- `http://localhost:8080/health`
- `http://localhost:8080/api/courses`

## Endpoints

- `GET /health`
- `GET /api/courses`
- `GET /api/courses/{id}`
- `POST /api/courses`
- `PUT /api/courses/{id}`
- `DELETE /api/courses/{id}`

## Observações

- Na inicialização, a aplicação executa `Database.MigrateAsync()` e aplica a migração inicial automaticamente.
- A string de conexão usada no container da API vem de `ConnectionStrings__DefaultConnection`.
