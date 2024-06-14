# school-system

school-system é uma aplicação web ASP.NET Core MVC projetada para gerenciar informações de escolas, incluindo a adição, edição e exclusão de escolas. O sistema também permite o upload de logotipos das escolas e a visualização de todas as escolas cadastradas.

## Tecnologias Utilizadas

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- HTML/CSS
- JavaScript

## Funcionalidades

### Adicionar Escola

- **Rota**: `/School/Add`
- **Descrição**: Permite adicionar uma nova escola, incluindo o upload de um logotipo.
- **Método HTTP**: GET, POST

### Editar Escola

- **Rota**: `/School/EditSchool/{id}`
- **Descrição**: Permite editar informações de uma escola existente, incluindo a atualização do logotipo.
- **Método HTTP**: GET, POST

### Listar Escolas

- **Rota**: `/School`
- **Descrição**: Exibe uma lista de todas as escolas cadastradas.
- **Método HTTP**: GET

### Excluir Escola

- **Rota**: `/School/DeleteConfirmed/{id}`
- **Descrição**: Exclui uma escola especificada pelo seu ID.
- **Método HTTP**: POST

## Estrutura do Projeto

### Controladores

- **SchoolController**: Controlador principal responsável pelas operações CRUD relacionadas às escolas.

### Modelos

- **School**: Modelo que representa uma escola.

### Contexto

- **SchoolContext**: Contexto do Entity Framework Core para interação com o banco de dados.

## Configuração e Execução

### Pré-requisitos

- .NET 7.0 SDK
- SQL Server

### Passos para Configuração

1. Clone o repositório.
   ```bash
   git clone https://github.com/seu-usuario/school-system.git
   cd school-system
   ```

2. Restaure as dependências.
   ```bash
   dotnet restore
   ```

3. Atualize a string de conexão com o banco de dados no arquivo `appsettings.json`.
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=school-system;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```

4. Aplique as migrações do Entity Framework Core para criar o banco de dados.
   ```bash
   dotnet ef database update
   ```

5. Execute a aplicação.
   ```bash
   dotnet run
   ```

6. Acesse a aplicação no navegador.
   ```url
   https://localhost:5001
   ```

## Estrutura de Pastas

- **wwwroot/edumin/uploads/**: Diretório para uploads gerais.
- **wwwroot/edumin/vendor/lightgallery/src/images/**: Diretório para imagens da galeria.
- **wwwroot/Uploads/Logos/**: Diretório para armazenar os logotipos das escolas.

## Dependências

- Microsoft.AspNetCore.Identity.EntityFrameworkCore - Version 7.0.3
- Microsoft.AspNetCore.Identity.UI - Version 8.0.6
- Microsoft.EntityFrameworkCore - Version 7.0.4
- Microsoft.EntityFrameworkCore.SqlServer - Version 7.0.4
- Microsoft.EntityFrameworkCore.Tools - Version 7.0.4

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou enviar pull requests.
