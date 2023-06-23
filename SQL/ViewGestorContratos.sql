USE GestorContratos
-------------------------------
-- View: Editais por contratos
-------------------------------
CREATE VIEW ViewEditaisPorContratos
AS
SELECT C.UnidadeGestora, c.ProcessoSei, C.Contratada, C.Objeto, C.Modalidade, C.Valor,
       E.Numero, E.EditalTipo, E.DataEdital
FROM Contratos C
INNER JOIN Editais E ON C.ContratoID = E.ContratoID;

---------------------------------
-- View: Pagamentos por contratos
----------------------------------
CREATE VIEW vwContratosPagamentos AS
SELECT c.ContratoID, c.UnidadeGestora, c.Extrato, c.Contratante, c.Contratada, c.Objeto, c.Vigencia,
       c.DataInicio, c.ProcessoSei, c.LinkPublico, c.DataAssinatura, c.ProtocoloDiof, c.Modalidade,
       c.Valor, pt.NotaEmpenho, pt.Tipo AS ModalidadePagamento, pt.DataCadastro,
       p.NotaLancamento, p.PreparacaoPagamento, p.OrdemBancaria, p.Valor AS ValorPagamento,
       p.DataPagamento, p.Parcela
FROM Contratos c
JOIN PagamentosTipo pt ON c.ContratoID = pt.ContratoID
JOIN Pagamentos p ON pt.PagamentoID = p.PagamentoID;

---------------------------------------------
-- View: Contratos com Detalhes de Pagamentos
---------------------------------------------

CREATE VIEW ViewContratosPagamentos
AS
SELECT C.UnidadeGestora, C.ProcessoSei, C.Contratada, C.Objeto, C.Modalidade, C.Valor,
       P.NotaLancamento, P.PreparacaoPagamento, P.OrdemBancaria, P.DataPagamento, P.Valor AS ValorPagamento,
       PT.NotaEmpenho, PT.Tipo
FROM Contratos C
INNER JOIN PagamentosTipo PT ON C.ContratoID = PT.ContratoID
INNER JOIN Pagamentos P ON PT.PagamentoID = P.PagamentoID;


-------------------------------------------------
-- View: Valor Total de Pagamentos por contratos
-------------------------------------------------

CREATE VIEW ViewPagamentosTotalPorContrato
AS
SELECT C.UnidadeGestora, C.ProcessoSei, C.Contratada, C.Objeto, C.Modalidade, C.Valor,
       STRING_AGG(P.NotaLancamento, ', ') AS NotasLancamento, 
       SUM(P.Valor) AS ValorTotalPagamentos
FROM Contratos C
INNER JOIN PagamentosTipo PT ON C.ContratoID = PT.ContratoID
INNER JOIN Pagamentos P ON PT.PagamentoID = P.PagamentoID
GROUP BY C.UnidadeGestora, C.ProcessoSei, C.Contratada, C.Objeto, C.Modalidade, C.Valor;


---------------------------------------------
-- View: Despesas Orçamentaria por contratos
---------------------------------------------
CREATE VIEW vwDespesasPorContratos
AS
SELECT C.UnidadeGestora, C.ProcessoSei, C.Contratada, C.Objeto, C.Modalidade, C.Valor,
       D.Programa, D.Acao, D.Fonte, D.Natureza, D.Elemento
FROM Contratos C
INNER JOIN DespesasOrcamentaria D ON C.ContratoID = D.ContratoID;


---------------------------------
-- View: Portarias por contratos
---------------------------------

CREATE VIEW vwPortariasPorContratos
AS
SELECT C.UnidadeGestora, C.ProcessoSei, C.Contratada, C.Objeto, C.Modalidade, C.Valor,
       P.Numero, P.ProtocoloDiof, P.DataPublicacao
FROM Contratos C
INNER JOIN Portarias P ON C.ContratoID = P.ContratoID;


---------------------------------
-- View: Pessoas por Contratos
---------------------------------
CREATE VIEW vwPessoasPorContratos
AS
SELECT C.UnidadeGestora, C.ProcessoSei, C.Contratada, C.Objeto, C.Modalidade, C.Valor,
       PE.Matricula, PE.Nome,
       PP.FuncaoPessoa, PP.TipoPortaria
FROM Contratos C
INNER JOIN Portarias P ON C.ContratoID = P.ContratoID
INNER JOIN PessoasPortarias PP ON P.PortariaID = PP.PortariaID
INNER JOIN Pessoas PE ON PP.PessoaID = PE.PessoaID;


