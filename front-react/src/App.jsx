const highlights = [
  'React 19 configurado em JavaScript puro',
  'Vite com HMR acessível via Docker',
  'Estrutura pronta para evoluir o front da plataforma',
]

export default function App() {
  return (
    <main className="shell">
      <section className="hero">
        <p className="eyebrow">Courses Platform</p>
        <h1>Front-end React com Vite e Docker</h1>
        <p className="lead">
          Ambiente inicial para desenvolver a interface da plataforma de cursos com
          recarregamento rápido, sem TypeScript e com execução isolada em container.
        </p>

        <div className="actions">
          <a href="http://localhost:8080/health" target="_blank" rel="noreferrer">
            Ver backend
          </a>
          <a href="https://vite.dev/guide/" target="_blank" rel="noreferrer">
            Documentação do Vite
          </a>
        </div>
      </section>

      <section className="panel">
        <h2>O que já está pronto</h2>
        <ul>
          {highlights.map((item) => (
            <li key={item}>{item}</li>
          ))}
        </ul>
      </section>
    </main>
  )
}
