# GestorContratos
 Gestor de Contratos ASP.Net Core MVC Data First

- Pré-requisitos necessários: SQL SEVER 19, MSSQL Sever Managemente Studio, Visual Studio 2022 com Net 7.0  (todos Comunity Edition).

- NOTA: Esse sistema foi elaborado no conceito DATA FIRST utilizando princípios Scaffolding (Engenharia Reversa) com Entity Framework, foi desenvolvido localmente com estrutura DOCKER. Antes de iniciar o projeto você deve abrir o MSSQL com SQL SEVER instalado localhost e rodar as Query scripts SQL de criação do banco de dados com suas tabelas, das views e das triggers. Após realizada essa etapa Abra o Visual Studio Code 2022 Crie um novo projeto Asp.Net Core Web APP MVC com Net 7.0 e siga o passo-a-passo.


- Passo 1: Instalar os pacotes Microsoft EF para habilitar as ferramentas de scaffolding.
- Install-Package Microsoft.EntityFrameworkCore.SqlServer
- Install-Package Microsoft.EntityFrameworkCore.Design
- Install-Package Microsoft.EntityFrameworkCore.Toolsa
- Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design

- Passo 2: Configurar o arquivo appsettings.json (acrecentar as seguintes linhas)

- "ConnectionStrings": {
-   "DefaultConnection": "Data Source=localhost;Initial Catalog=GestorContratos;Integrated Security=True;TrustServerCertific`ate=True;"
- }


- Passo 3: Configurar o arquivo Program.cs para ler o arquivo appsettings.json (acrecentar as seguintes linhas depois da linha builder.Services.AddControllersWithViews(); )

- // Configure the app configuration by loading appsettings.json
- builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

- Passo 4: Rodar o comando Scaffold-DbContext

- Scaffold-DbContext "Data Source=localhost;Initial Catalog=GestorContratos;Integrated Security=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

- Passo 5: Criar os controladores com o commando codegenerator 

- dotnet aspnet-codegenerator controller -name ContratosController -m Contrato -dc GestorContratosContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

- Fonte:
- https://learn.microsoft.com/pt-pt/ef/core/what-is-new/ef-core-7.0/whatsnew	
- https://learn.microsoft.com/pt-pt/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli#prerequisites

