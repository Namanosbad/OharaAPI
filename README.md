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

> A **OharaAPI** é uma adaptação de um sistema real que desenvolvi para um escritório com cerca de 30 advogados. O projeto original era voltado para a gestão interna de documentos e fluxos específicos do escritório.

> Para este repositório, adaptei a estrutura e a arquitetura do sistema para um novo contexto: uma biblioteca fictícia inspirada no universo de One Piece.

> Algumas regras de negócio e funcionalidades específicas do projeto original foram removidas, pois não faziam sentido nesse novo cenário. O objetivo aqui é apresentar uma versão simplificada, funcional e segura, que demonstre a base arquitetural e as boas práticas utilizadas no sistema real, mas em um contexto mais leve e fácil de explorar.

> O nome e todo o design são referências claras a One Piece, um anime que é parte importante da minha vida. Ohara é o nome da ilha que abrigava a maior biblioteca do mundo, famosa por seus estudiosos e pelo conhecimento que preservava, até ser destruída pelo próprio Governo, por medo do que esse conhecimento poderia revelar ao mundo. A ideia deste projeto é te fazer alocar e organizar cada parte importante do conhecimento, conhecer cada autor e explorar cada obra que ele escreveu, trazendo um pouco do espírito de descoberta e aprendizado da própria ilha de Ohara, para que o conhecimento nunca morra.

---

## 🖼️ Galeria do Projeto

### Tela inicial / Home

![OharaAPI](https://github.com/user-attachments/assets/15b01934-b287-455e-9927-c68c68dd6ac2)

### Lista de livros

![Lista de livros](https://github.com/user-attachments/assets/9df56787-e82b-4978-8cf3-a834a4b49274)

### Detalhes do livro
![Detalhes do livros](https://github.com/user-attachments/assets/f16e1beb-8eb7-4c85-adb6-e675b599e4fe)

### Cadastro de livro

![Cadastro de livro](https://github.com/user-attachments/assets/4b0809e8-4c69-4ab8-be3a-3851d084e746)

### Editar livro

![Editar livro](https://github.com/user-attachments/assets/a99a4398-da85-41a7-a90e-af994c7cc022)

### Lista de autores

![Lista de autores](https://github.com/user-attachments/assets/fec4a2e3-3983-4e87-b709-e519ad712dbe)

### Detalhes do autor
![Detalhes do livros](https://github.com/user-attachments/assets/5940be67-fe0d-4117-9fbf-5edd09e805f3)

### Swagger / documentação da API

![Swagger da API](https://github.com/user-attachments/assets/1d5de4b6-739b-4aad-ac35-3901cffc50c8)

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
