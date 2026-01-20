# OharaAPI 📚

A **OharaAPI** é a base de um sistema de biblioteca real, pensado para uso em ambiente corporativo e evolução contínua em produção.

Este projeto nasceu de uma necessidade prática: construir uma API sólida, capaz de crescer com a empresa, suportar novas regras de negócio e manter a qualidade do código ao longo do tempo.

A solução foi estruturada com **Clean Architecture** e **Domain-Driven Design (DDD)** para garantir que o domínio seja o centro do sistema, mantendo a aplicação preparada para mudanças futuras.

Este repositório representa a fundação de um produto em evolução

---

## 🚀 Tecnologias Utilizadas

*   **Framework:** .NET 8.0
*   **Linguagem:** C#
*   **ORM:** Entity Framework Core
*   **Banco de Dados:** SQL Server
*   **Documentação:** Swagger (OpenAPI)
*   **Containerização:** Docker

---

## 🏗️ Arquitetura do Projeto

O projeto está dividido em camadas para garantir a separação de responsabilidades:

*   **Ohara.API.Internal:** Camada de apresentação (Web API) com os Controllers e configurações de entrada.
*   **Ohara.API.Application:** Contém a lógica de negócio, serviços e interfaces de aplicação.
*   **Ohara.API.Domain:** O coração do projeto. Contém as entidades, enums e interfaces de repositório.
*   **Ohara.API.Database:** Implementação da persistência de dados e mapeamentos do Entity Framework.
*   **Ohara.API.Ioc:** Camada responsável pela Inversão de Controle e Injeção de Dependência.
*   **Ohara.API.Shared:** Projetos de suporte com DTOs (Requests/Responses) compartilhados.

---

## ⚙️ Como Executar o Projeto

### Pré-requisitos
*   SDK do .NET 8.0 instalado.
*   SQL Server configurado e rodando.

### Passo a Passo

1.  **Clonar o repositório:**
    ```bash
    git clone https://github.com/Namanosbad/OharaAPI.git
    cd OharaAPI
    ```

2.  **Configurar o Banco de Dados:**
    Atualize a `ConnectionString` no arquivo `appsettings.json` dentro do projeto `Ohara.API.Internal`.

3.  **Executar as Migrações:**
    Abra o terminal na raiz do projeto e execute:
    ```bash
    dotnet ef database update --project Ohara.API.Database --startup-project Ohara.API.Internal
    ```

4.  **Rodar a API:**
    ```bash
    dotnet run --project Ohara.API.Internal
    ```
    Acesse `https://localhost:7001/swagger` para visualizar a documentação interativa.

---

## 🛠️ Endpoints Principais

### Livros
*   `GET /api/livros` - Lista todos os livros.
*   `GET /api/livros/{id}` - Busca um livro por ID.
*   `GET /api/genero-livros` - Busca um livro por genero.
*   `POST /api/livros` - Cadastra um novo livro.
*   `PUT /api/livros` - Atualiza um livro cadastrado.
*   `DELETE /api/livros` - Deleta um livro cadastrado.

### Autores
*   `GET /api/autores` - Lista todos os autores e seus respectivos livros.
*   `POST /api/autores` - Cadastra um novo autor.

---

## 📅 Roteiro de Desenvolvimento (Roadmap)

- [x] Estrutura base e Clean Architecture.
- [x] Implementação de Repositórios e Services.
- [x] Criação dos Controllers de Livros e Autores.
- [ ] **Próximo Passo:** Implementação da Interface Desktop (.NET MAUI).
- [ ] **Próximo Passo:** Deploy em servidor de produção.

---
*Desenvolvido por [Namanosbad](https://github.com/Namanosbad)*
