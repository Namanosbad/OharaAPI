# OharaAPI 📚⚖️

<p align="center">
  <em>Gestão de biblioteca jurídica com arquitetura em camadas, API REST e cliente MAUI/Blazor.</em>
</p>

<p align="center">
  <a href="#-visão-geral">Visão Geral</a> •
  <a href="#-galeria-do-projeto">Galeria</a> •
  <a href="#-arquitetura">Arquitetura</a> •
  <a href="#-como-rodar">Como Rodar</a> •
  <a href="#-endpoints">Endpoints</a>
</p>

---

## ✨ Visão Geral

> A **OharaAPI** é baseada em um sistema de biblioteca real que criei para um escritório com cerca de 30 advogados. Mas calma: este repositório é uma versão simplificada do software original. Algumas funções e regras específicas foram retiradas, já que aqui não eram necessárias, então o que você encontra é um exemplo funcional, seguro e fácil de explorar.

> Este projeto nasceu de uma necessidade prática: construir uma API sólida, capaz de crescer com a empresa, suportar novas regras de negócio e manter a qualidade do código ao longo do tempo. A solução foi estruturada com *Clean Architecture* e *Domain-Driven Design (DDD)* para garantir que o domínio seja o centro do sistema, mantendo a aplicação preparada para mudanças futuras.

> O nome e todo o design são referências claras a One Piece, um anime que é parte importante da minha vida. Ohara é o nome da ilha que abrigava a maior biblioteca do mundo, famosa por seus estudiosos e pelo conhecimento que preservava, até ser destruída pelo próprio Governo, por medo do que esse conhecimento poderia revelar ao mundo. A ideia deste projeto é te fazer alocar e organizar cada parte importante do conhecimento, conhecer cada autor e explorar cada obra que ele escreveu, trazendo um pouco do espírito de descoberta e aprendizado da própria ilha de Ohara, para que o conhecimento nunca morra.

---

## 🖼️ Galeria do Projeto

### Tela inicial / Home

![Home do projeto](https://github.com/user-attachments/assets/15b01934-b287-455e-9927-c68c68dd6ac2)

### Lista de livros

![Lista de livros](https://github.com/user-attachments/assets/9df56787-e82b-4978-8cf3-a834a4b49274)


### Cadastro de livro

![Cadastro de livro](docs/images/livros-cadastro.png)

> Substitua por um print real em `docs/images/livros-cadastro.png`.

### Lista de autores

![Lista de autores](docs/images/autores-lista.png)

> Substitua por um print real em `docs/images/autores-lista.png`.

### Swagger / documentação da API

![Swagger da API](docs/images/swagger.png)

> Substitua por um print real em `docs/images/swagger.png`.

---

## 🧱 Arquitetura

A solução está organizada em camadas para separar responsabilidades:

- **Ohara.API.Internal**
  - Host da API, controllers, Swagger e middleware global de exceções.
- **Ohara.API.Application**
  - Serviços de aplicação, interfaces e mapeamentos.
- **Ohara.API.Domain**
  - Entidades centrais e contratos de repositório.
- **Ohara.API.Database**
  - `DbContext`, configurações de entidade e repositórios EF Core.
- **Ohara.API.Ioc**
  - Injeção de dependências e registro de serviços.
- **Ohara.API.Shared**
  - DTOs de request/response, enums e modelos compartilhados.
- **Ohara.APP.Client**
  - Cliente MAUI/Blazor para consumo dos endpoints.

---

## 🛠️ Stack Tecnológica

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core (SQL Server)**
- **AutoMapper**
- **Swagger (OpenAPI)**
- **Docker**
- **.NET MAUI Blazor Hybrid**

---

## 🚀 Como Rodar

### Pré-requisitos

- SDK do **.NET 8**
- Instância do **SQL Server**
- Workloads MAUI instaladas

### 1) Clonar o repositório

```bash
git clone https://github.com/Namanosbad/OharaAPI.git
cd OharaAPI
```

### 2) Configurar banco

Ajuste a string de conexão em:

- `Ohara.API.Internal/appsettings.json`
- seção: `DbConfig:ConnectionString`

### 3) Aplicar migrations

```bash
dotnet ef database update --project Ohara.API.Database --startup-project Ohara.API.Internal
```

### 4) Executar API

```bash
dotnet run --project Ohara.API.Internal
```

A API está configurada para escutar em **8080** no `Program.cs`.

- Swagger: `http://localhost:8080/swagger`

> Observação: existem perfis adicionais no `launchSettings.json` para execução por IDE.

---

## 🐳 Rodando com Docker (API)

```bash
docker build -f Ohara.API.Internal/Dockerfile -t ohara-api .
docker run --rm -p 8080:8080 ohara-api
```

Depois acesse:

- `http://localhost:8080/swagger`

---

## 🔌 Endpoints

Base path: `api/v1`

### Livros (`/api/v1/livros`)

- `GET /api/v1/livros`
- `GET /api/v1/livros/{id}`
- `GET /api/v1/livros/titulo?titulo=...`
- `GET /api/v1/livros/genero?genero=...`
- `POST /api/v1/livros/cadastrar`
- `PUT /api/v1/livros/{id}`
- `DELETE /api/v1/livros/{id}`

### Autor (`/api/v1/autor`)

- `GET /api/v1/autor/listar`
- `GET /api/v1/autor/{id}`
- `GET /api/v1/autor?nome=...`

---

## 📚 Regras de Negócio (Resumo)

- Cadastro de livro exige **título** e **nome do autor**.
- Se o autor não existir, ele é criado automaticamente.
- Não permite livro duplicado com mesmo título para o mesmo autor.
- `BusinessException` retorna `400 Bad Request` via middleware global.

---

## 🖥️ Cliente MAUI

O cliente `Ohara.APP.Client` consome a API via `HttpClient` nomeado (`API`), com URL base definida em:

- `Ohara.APP.Client/appsettings.json`

Exemplo atual:

```json
{
  "APISettings": {
    "BaseUrl": "http://localhost:7143"
  }
}
```

Se sua API estiver em outra porta, ajuste esse valor.

Build do cliente:

```bash
dotnet build Ohara.APP.Client/Ohara.APP.Client.csproj
```

---

## 👤 Autor

Desenvolvido por [Namanosbad](https://github.com/Namanosbad).
