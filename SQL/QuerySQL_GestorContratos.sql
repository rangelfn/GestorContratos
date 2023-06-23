--Criando o banco de dados
CREATE DATABASE GestorContratos

--Selecionando o banco criado
USE GestorContratos

-- Criação da tabela Contratos
CREATE TABLE Contratos (
  ContratoID INT PRIMARY KEY IDENTITY,
  UnidadeGestora VARCHAR(255) NOT NULL,
  Extrato VARCHAR(255) NOT NULL,
  Contratante VARCHAR(255) NOT NULL,
  Contratada VARCHAR(255) NOT NULL,
  Objeto VARCHAR(4000) NOT NULL,
  Vigencia INT NOT NULL,
  DataInicio DATE NOT NULL,
  ProcessoSei VARCHAR(255) NOT NULL,
  LinkPublico VARCHAR(255) NOT NULL,
  DataAssinatura DATE NOT NULL,
  ProtocoloDiof VARCHAR(255) NOT NULL,
  Modalidade VARCHAR(255) NOT NULL,
  Valor DECIMAL(10, 2)
);

-- Criação da tabela Editais
CREATE TABLE Editais (
  EditalID INT PRIMARY KEY IDENTITY,
  Numero VARCHAR(255) NOT NULL,
  EditalTipo VARCHAR(255) NOT NULL,
  LinkPublico VARCHAR(255) NOT NULL,
  DataEdital DATE NOT NULL,
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Criação da tabela Aditivos
CREATE TABLE Aditivos (
  AditivoID INT PRIMARY KEY IDENTITY,
  Numero VARCHAR(255) NOT NULL,
  Descricao VARCHAR(255) NOT NULL,
  DataAditivos DATE,
  Valor DECIMAL(10, 2) NOT NULL,
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Criação da tabela Apostilamentos com restrição CHECK nos campos DataInicio e Data Fim
CREATE TABLE Apostilamentos (
  ApostilamentoID INT PRIMARY KEY IDENTITY,
  Numero VARCHAR(255) NOT NULL,
  Descricao VARCHAR(255) NOT NULL,
  DataAditivos DATE,
  Valor DECIMAL(10, 2) NOT NULL,
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Criação da tabela PagamentoTipo
CREATE TABLE PagamentosTipo (
  PgtTipoID INT PRIMARY KEY IDENTITY,
  NotaEmpenho VARCHAR (255) NOT NULL,
  Tipo VARCHAR(255) NOT NULL,
  DataCadastro DATE NOT NULL,
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Criação da tabela Pagamentos
CREATE TABLE Pagamentos (
  PagamentoID INT PRIMARY KEY IDENTITY,
  NotaLancamento VARCHAR(255) NOT NULL,
  PreparacaoPagamento VARCHAR(255) NOT NULL,
  OrdemBancaria VARCHAR(255) NOT NULL,
  Valor DECIMAL(10, 2) NOT NULL,
  DataPagamento DATE NOT NULL,
  Parcela VARCHAR(10) NOT NULL
  PgtTipoID INT,
  FOREIGN KEY (PgtTipoID) REFERENCES PagamentosTipo (PgtTipoID) ON DELETE CASCADE
);

-- Criação da tabela Despesa com restrição CHECK nos campos "Fonte, Natureza e Elemento" utilizando a expressão regular
CREATE TABLE DespesasOrcamentaria (
  DespesaID INT PRIMARY KEY IDENTITY,
  Programa VARCHAR(255),
  Acao VARCHAR(255),
  Fonte VARCHAR(12) CHECK (Fonte LIKE '[0-9].[0-9][0-9][0-9].[0-9][0-9][0-9][0-9][0-9][0-9]'),
  Natureza VARCHAR(14) CHECK (Natureza LIKE '[0-9].[0-9].[0-9][0-9].[0-9][0-9].[0-9][0-9]'),
  Elemento VARCHAR(8) CHECK (Elemento LIKE '[0-9][0-9].[0-9][0-9].[0-9][0-9]'),
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Criação da tabela Portaria
CREATE TABLE Portarias (
  PortariaID INT PRIMARY KEY IDENTITY,
  Numero VARCHAR(255) NOT NULL,
  ProtocoloDiof VARCHAR(255) NOT NULL,
  UnidadeGestora VARCHAR(255) NOT NULL,
  Tipo VARCHAR(255) NOT NULL,
  DataPublicacao DATE,
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Criação da tabela Pessoa com restrição CHECK nos campos CPF e DataInicio e Data Fim
CREATE TABLE Pessoas (
  PessoaID INT PRIMARY KEY IDENTITY,
  Matricula  VARCHAR(255) NOT NULL,
  Nome VARCHAR(255) NOT NULL,
  CPF VARCHAR(11) NOT NULL CHECK (LEN(CPF) = 11),
  UG VARCHAR(255) NOT NULL,
  Setor VARCHAR(255) NOT NULL
);

-- Criação da tabela Resolucao com restrição CHECK nos campos DataInicio e Data Fim
CREATE TABLE PessoasPortarias (
  ResolucaoID INT IDENTITY(1,1) PRIMARY KEY,
  TipoPortaria VARCHAR(255) NOT NULL,
  FuncaoPessoa VARCHAR(255) NOT NULL,
  PessoaID INT,
  FOREIGN KEY (PortariaID) REFERENCES Portarias (PortariaID) ON DELETE CASCADE, 
  PortariaID INT,
  FOREIGN KEY (PessoaID) REFERENCES Pessoas (PessoaID) ON DELETE CASCADE
);

-- Criação da tabela Usuarios
CREATE TABLE Usuarios (
  UsuarioID INT PRIMARY KEY IDENTITY,
  LoginCPF VARCHAR(255) NOT NULL,
  Nome VARCHAR(255) NOT NULL,
  Email VARCHAR(255) NOT NULL,
  Senha VARCHAR(255) NOT NULL
);

-- Criação da tabela ContratosUsuarios
CREATE TABLE UsuariosContratos (
  UsuariosContratosID INT IDENTITY(1,1) PRIMARY KEY,
  UsuarioID INT,
  FOREIGN KEY (UsuarioID) REFERENCES Usuarios (UsuarioID) ON DELETE CASCADE,
  ContratoID INT,
  FOREIGN KEY (ContratoID) REFERENCES Contratos (ContratoID) ON DELETE CASCADE
);

-- Criação da tabela Auditoria
CREATE TABLE Auditorias (
  Tabela VARCHAR(50),
  Acao VARCHAR(10),
  Usuario VARCHAR(70),
  DataHora DATE,
  Chave VARCHAR (255),
  Antes VARCHAR (4000),
  Depois VARCHAR (4000)
);
