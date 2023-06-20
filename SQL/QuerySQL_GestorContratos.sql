--Criando o banco de dados

CREATE DATABASE GestorContratos

--Selecionando o banco criado
USE GestorContratos

-- Cria��o da tabela Contratos
CREATE TABLE Contratos (
  ContratoID INT PRIMARY KEY,
  UnidadeGestora VARCHAR(255) NOT NULL,
  Extrato VARCHAR(255) NOT NULL,
  Contratante VARCHAR(255) NOT NULL,
  Contratada VARCHAR(255) NOT NULL,
  Objeto VARCHAR(255) NOT NULL,
  Vigencia INT NOT NULL,
  DataInicio DATE NOT NULL,
  ProcessoSei VARCHAR(255) NOT NULL,
  LinkPublico VARCHAR(255) NOT NULL,
  DataAssinatura DATE NOT NULL,
  ProtocoloDiof VARCHAR(255) NOT NULL,
  Modalidade BIT,
  Valor DECIMAL(10, 2)
);

-- Cria��o da tabela Editais
CREATE TABLE Editais (
  EditalID INT PRIMARY KEY,
  Numero VARCHAR(255) NOT NULL,
  LinkPublico VARCHAR(255) NOT NULL,
  DataPublicacao DATE NOT NULL,
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Cria��o da tabela Aditivos
CREATE TABLE Aditivos (
  AditivoID INT PRIMARY KEY,
  Numero VARCHAR(255) NOT NULL,
  Descricao VARCHAR(255) NOT NULL,
  DataInicio DATE,
  DataFim DATE,
  CHECK (DataInicio <= DataFim),
  Valor DECIMAL(10, 2) NOT NULL,
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Cria��o da tabela Apostilamentos com restri��o CHECK nos campos DataInicio e Data Fim
CREATE TABLE Apostilamentos (
  ApostilamentoID INT PRIMARY KEY,
  Numero VARCHAR(255) NOT NULL,
  Descricao VARCHAR(255) NOT NULL,
  DataInicio DATE,
  DataFim DATE,
  CHECK (DataInicio <= DataFim),
  Valor DECIMAL(10, 2) NOT NULL,
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Cria��o da tabela Pagamentos
CREATE TABLE Pagamentos (
  PagamentoID INT PRIMARY KEY,
  NotaLancamento VARCHAR(255) NOT NULL,
  PreparacaoPagamento VARCHAR(255) NOT NULL,
  OrdemBancaria VARCHAR(255) NOT NULL,
  Valor DECIMAL(10, 2) NOT NULL,
  DataPagamento DATE NOT NULL,
  Parcela VARCHAR(10) NOT NULL
);

-- Cria��o da tabela PagamentoTipo
CREATE TABLE PagamentosTipo (
  NotaEmpenho INT PRIMARY KEY,
  Modalidade BIT,
  DataCadastro DATE NOT NULL,
  ContratoID INT,
  PagamentoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE, 
  FOREIGN KEY (PagamentoID) REFERENCES Pagamentos (PagamentoID) ON DELETE CASCADE 
);

-- Cria��o da tabela Despesa com restri��o CHECK nos campos "Fonte, Natureza e Elemento" utilizando a express�o regular
CREATE TABLE Despesas (
  DespesaID INT PRIMARY KEY,
  Programa VARCHAR(255),
  Acao VARCHAR(255),
  Fonte VARCHAR(12) CHECK (Fonte LIKE '[0-9].[0-9][0-9][0-9].[0-9][0-9][0-9][0-9][0-9][0-9]'),
  Natureza VARCHAR(14) CHECK (Natureza LIKE '[0-9].[0-9].[0-9][0-9].[0-9][0-9].[0-9][0-9]'),
  Elemento VARCHAR(8) CHECK (Elemento LIKE '[0-9][0-9].[0-9][0-9].[0-9][0-9]'),
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Cria��o da tabela Portaria
CREATE TABLE Portarias (
  PortariaID INT PRIMARY KEY,
  Numero VARCHAR(255) NOT NULL,
  ProtocoloDiof VARCHAR(255) NOT NULL,
  UnidadeGestora VARCHAR(255) NOT NULL,
  DataPublicacao DATE,
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Cria��o da tabela Pessoa com restri��o CHECK nos campos CPF e DataInicio e Data Fim
CREATE TABLE Pessoas (
  PessoaID INT PRIMARY KEY,
  Matricula  VARCHAR(255) NOT NULL,
  Nome VARCHAR(255) NOT NULL,
  CPF VARCHAR(11) NOT NULL CHECK (LEN(CPF) = 11),
  UG VARCHAR(255) NOT NULL,
  Setor VARCHAR(255) NOT NULL,
  DataInicio DATE,
  DataFim DATE,
  CHECK (DataInicio <= DataFim)
);

-- Cria��o da tabela Resolucao com restri��o CHECK nos campos DataInicio e Data Fim
CREATE TABLE Resolucoes (
  ResolucaoID INT IDENTITY(1,1) PRIMARY KEY,
  Tipo VARCHAR(255) NOT NULL,
  DataInicio DATE,
  DataFim DATE,
  CHECK (DataInicio <= DataFim),
  PortariaID INT,
  PessoaID INT,
  FOREIGN KEY (PortariaID) REFERENCES Portarias (PortariaID) ON DELETE CASCADE, 
  FOREIGN KEY (PessoaID) REFERENCES Pessoas (PessoaID) ON DELETE CASCADE
);

-- Cria��o da tabela Usuarios
CREATE TABLE Usuarios (
  UsuarioID INT PRIMARY KEY,
  LoginCPF VARCHAR(255) NOT NULL,
  Email VARCHAR(255) NOT NULL,
  Senha VARCHAR(255) NOT NULL
);

-- Cria��o da tabela ContratosUsuarios
CREATE TABLE ContratosUsuarios (
  ContratosUsuariosID INT IDENTITY(1,1) PRIMARY KEY,
  UsuarioID INT,
  ContratoID INT,
  FOREIGN KEY (UsuarioID) REFERENCES Usuarios (UsuarioID) ON DELETE CASCADE, 
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Cria��o da tabela Auditoria
CREATE TABLE Auditorias (
  Tabela VARCHAR(50),
  Acao VARCHAR(10),
  Usuario VARCHAR(70),
  DataHora DATETIME,
  Chave VARCHAR (255),
  Antes VARCHAR (4000),
  Depois VARCHAR (4000)
);