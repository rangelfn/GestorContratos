USE GestorContratos

-- View: Contratos com Detalhes de Editais
CREATE VIEW vwContratosEditais
AS
SELECT c.*, e.Numero AS EditalNumero, e.LinkPublico AS EditalLink, e.DataPublicacao AS EditalDataPublicacao
FROM Contratos c
LEFT JOIN Editais e ON c.ContratoID = e.ContratoID;


-- View: Contratos com Detalhes de Pagamentos
CREATE VIEW vwContratosPagamentos AS
SELECT c.ContratoID, c.UnidadeGestora, c.Extrato, c.Contratante, c.Contratada, c.Objeto, c.Vigencia,
       c.DataInicio, c.ProcessoSei, c.LinkPublico, c.DataAssinatura, c.ProtocoloDiof, c.Modalidade,
       c.Valor, pt.NotaEmpenho, pt.Modalidade AS ModalidadePagamento, pt.DataCadastro,
       p.NotaLancamento, p.PreparacaoPagamento, p.OrdemBancaria, p.Valor AS ValorPagamento,
       p.DataPagamento, p.Parcela
FROM Contratos c
JOIN PagamentosTipo pt ON c.ContratoID = pt.ContratoID
JOIN Pagamentos p ON pt.PagamentoID = p.PagamentoID;


-- View: Contratos com Valor Total de Pagamentos
CREATE VIEW vwContratosValorTotalPagamentos
AS
SELECT c.ContratoID, c.UnidadeGestora, c.Extrato, c.Contratante, c.Contratada, c.Objeto, c.Vigencia, c.DataInicio, c.ProcessoSei, c.LinkPublico, c.DataAssinatura, c.ProtocoloDiof, c.Modalidade, c.Valor,
       ISNULL(SUM(p.Valor), 0) AS ValorTotalPagamentos
FROM Contratos c
LEFT JOIN PagamentosTipo pt ON c.ContratoID = pt.ContratoID
LEFT JOIN Pagamentos p ON pt.PagamentoID = p.PagamentoID
GROUP BY c.ContratoID, c.UnidadeGestora, c.Extrato, c.Contratante, c.Contratada, c.Objeto, c.Vigencia, c.DataInicio, c.ProcessoSei, c.LinkPublico, c.DataAssinatura, c.ProtocoloDiof, c.Modalidade, c.Valor;


-- View: Contratos com Detalhes de Despesas
CREATE VIEW vwContratosDespesas
AS
SELECT c.*, d.Programa, d.Acao, d.Fonte, d.Natureza, d.Elemento
FROM Contratos c
LEFT JOIN Despesas d ON c.ContratoID = d.ContratoID;


-- View: Contratos com Informações de Portarias
CREATE VIEW vwContratosPortarias
AS
SELECT c.*, p.Numero AS PortariaNumero, p.ProtocoloDiof AS PortariaProtocolo, p.UnidadeGestora AS PortariaUnidadeGestora, p.DataPublicacao AS PortariaDataPublicacao
FROM Contratos c
LEFT JOIN Portarias p ON c.ContratoID = p.ContratoID;


-- View: Contratos com Informações de Pessoas
CREATE VIEW vwContratosPessoas
AS
SELECT c.ContratoID, c.UnidadeGestora, c.Extrato, c.Contratante, c.Contratada, c.Objeto, c.Vigencia, c.DataInicio AS ContratoDataInicio, c.ProcessoSei, c.LinkPublico, c.DataAssinatura, c.ProtocoloDiof, c.Modalidade, c.Valor,
       p.Matricula, p.Nome, p.CPF, p.UG AS PessoaUG, p.Setor, p.DataInicio AS PessoaDataInicio, p.DataFim
FROM Contratos c
LEFT JOIN ContratosUsuarios cu ON c.ContratoID = cu.ContratoID
LEFT JOIN Usuarios u ON cu.UsuarioID = u.UsuarioID
LEFT JOIN Pessoas p ON u.LoginCPF = p.CPF;
