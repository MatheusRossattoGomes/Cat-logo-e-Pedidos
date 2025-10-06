# Desafio Técnico - Catálogo e Pedidos

Este projeto é uma aplicação Full-Stack de Catálogo e Pedidos construída com .NET 8 (backend), Angular 17+ (frontend) e PostgreSQL, totalmente orquestrada com Docker e Docker Compose.

O foco foi a construção de uma arquitetura robusta (Clean Architecture), a implementação das funcionalidades de negócio mais críticas e a garantia de um ambiente de desenvolvimento e execução consistente com Docker.

## Stack de Tecnologias

| Categoria | Tecnologia |
|-----------|------------|
| Backend | .NET 8 (ASP.NET Core Web API) |
| Frontend | Angular 17+ (TypeScript) |
| Banco de Dados | PostgreSQL |
| Acesso a Dados | Entity Framework Core |
| Componentes UI | Angular Material (para Dialogs) |
| Infraestrutura | Docker e Docker Compose |

---

## Como Executar o Projeto

### Pré-requisitos
- Docker Desktop instalado e em execução.

### Instruções

1.  **Clone o Repositório**
    ```bash
    git clone [https://www.youtube.com/watch?v=RqfwLeY952s](https://www.youtube.com/watch?v=RqfwLeY952s)
    cd [nome-da-pasta-do-projeto]
    ```

2.  **Execute o Docker Compose**
    Na raiz do projeto, execute o comando abaixo. Ele irá construir as imagens do backend e frontend e iniciar todos os contêineres necessários (API, App Web e Banco de Dados).
    ```bash
    docker-compose up --build
    ```
    *A primeira execução pode demorar alguns minutos para baixar as imagens base e instalar as dependências.*

3.  **Aplique as Migrações do Banco de Dados**
    Com o ambiente Docker rodando, abra um **novo terminal** na pasta `backend/` e execute o comando para criar as tabelas e popular os dados iniciais:
    ```bash
    dotnet ef database update --startup-project src/Api
    ```

4.  **Acesse a Aplicação**
    - **Ambiente de Desenvolvimento (com Hot-Reload):** [http://localhost:4200](http://localhost:4200)

---

## Funcionalidades Implementadas

- ✅ **Estrutura Full-Stack com Docker:** A aplicação completa é executada com Docker, garantindo consistência entre os ambientes.
- ✅ **Backend com Clean Architecture:** A API .NET foi estruturada nas camadas `Domain`, `Application` (Services), `Infrastructure` (Repositories) e `Api`.
- ✅ **CRUD de Produtos e Clientes:** Funcionalidade completa de Criação, Leitura, Atualização e Exclusão para produtos e clientes, com formulários em modal.
- ✅ **Criação de Pedidos (Estrutura):**
    - Foi criada a tela de listagem de pedidos.
    - Foi implementado o formulário complexo para criação de um novo pedido, com busca dinâmica de clientes e produtos, e uma grid para os itens.
    - A lógica de negócio no backend (transação, validação de estoque, idempotência) foi implementada na `OrderService`.
- ✅ **Componentes Genéricos no Frontend:** Foram criados componentes reutilizáveis para a **Grid** de dados, **Formulário** dinâmico e **Botões de Ação** (Adicionar, Editar, Excluir).
- ✅ **Testes Automatizados (Iniciado):** O projeto de testes unitários (`Application.UnitTests`) foi criado e configurado, com um teste de regra de negócio como prova de conceito.

---

## O Que Faltou (Próximos Passos)

Devido ao tempo, algumas funcionalidades e otimizações do escopo original ficaram como próximos passos para a evolução do projeto:

- **Finalizar o CRUD de Pedidos:** Conectar o formulário de criação de pedidos ao endpoint do backend e implementar a edição/exclusão.
- **Paginação e Ordenação:** Implementar a paginação e a ordenação por colunas nas grids, tanto no frontend quanto no backend.
- **Filtros Avançados:** Evoluir o filtro de texto atual para filtros mais complexos.
- **Otimização com Dapper:** A listagem de dados está funcional com EF Core. O próximo passo seria refatorar essas consultas para usar Dapper, conforme o requisito de otimização.
- **Validações Completas:** Adicionar validações de negócio mais robustas na camada de serviço do backend (ex: verificar se um SKU já existe).
- **Cobertura de Testes:** Expandir a suíte de testes unitários para cobrir todas as regras de negócio implementadas.
- **Tratamento de Erros Global:** Implementar o `HttpInterceptor` no Angular para tratar os erros da API de forma centralizada.

---

## Uso de Inteligência Artificial

Conforme permitido, uma IA foi utilizada como assistente de programação ("pair programming") para as seguintes tarefas:

- **Configuração de Ambiente:** Auxílio na configuração inicial do ambiente Docker, `docker-compose.yml` e `Dockerfiles`, e na resolução de problemas de ambiente complexos (Node.js, PowerShell, Docker networking).
- **Criação de Estrutura:** Geração de scripts para criar a estrutura de pastas e arquivos do projeto por linha de comando.
- **Geração de Código Padrão ("Boilerplate"):** Criação das estruturas iniciais das classes, DTOs, interfaces, serviços e componentes Angular.
- **Mapeamentos e Seeds:** Auxílio na criação dos mapeamentos do EF Core e na geração dos dados de seed.
- **Criação de Telas e Componentes:** Geração de templates HTML/CSS para os componentes Angular, incluindo a lógica inicial para componentes genéricos como a grid e o formulário.
- **Depuração:** Assistência na identificação e correção de erros de compilação, execução e lógica em C#, TypeScript e configurações do Docker.
