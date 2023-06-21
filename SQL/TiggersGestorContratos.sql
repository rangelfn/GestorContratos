------------------------------------------------
--Ttrigger para bloquear gravações no DB nos FDS
------------------------------------------------
USE GestorContratos;
GO
CREATE TRIGGER RestricaoFinaisDeSemana
ON Contratos 
FOR INSERT, UPDATE, DELETE
AS
BEGIN
    -- Verifica se o dia atual é sábado ou domingo
    IF DATEPART(WEEKDAY, GETDATE()) IN (1, 7)
    BEGIN
        -- Cancela a operação atual gerando um erro
        RAISERROR ('Alterações nos finais de semana não são permitidas.', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
END
-------------------------------------------------
-- Trigger para salvar alterações para Auditorias
-------------------------------------------------
CREATE TRIGGER AuditarContratos
ON Contratos
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @Acao VARCHAR(10)

    -- Verifica o tipo de ação
    IF EXISTS(SELECT * FROM inserted)
    BEGIN
        IF EXISTS(SELECT * FROM deleted)
            SET @Acao = 'Update' -- Ação de atualização
        ELSE
            SET @Acao = 'Insert' -- Ação de inserção
    END
    ELSE
        SET @Acao = 'Delete' -- Ação de exclusão

    -- Insere os registros de auditoria
    INSERT INTO Auditorias (Tabela, Acao, Usuario, DataHora, Chave, Antes, Depois)
    SELECT 'Contratos', @Acao, SUSER_SNAME(), GETDATE(), 
        COALESCE(CONVERT(VARCHAR(100), i.ContratoID), CONVERT(VARCHAR(100), d.ContratoID)), -- Chave primária
        CONVERT(NVARCHAR(MAX), (SELECT * FROM deleted FOR XML AUTO, ELEMENTS)), -- Valores antes da ação
        CONVERT(NVARCHAR(MAX), (SELECT * FROM inserted FOR XML AUTO, ELEMENTS)) -- Valores depois da ação
    FROM inserted i
    FULL OUTER JOIN deleted d ON i.ContratoID = d.ContratoID -- Compara registros inseridos e deletados
END

---------------------------------
-- Listar as Triggers existentes
---------------------------------
SELECT name, object_name(parent_id) AS table_name
FROM sys.triggers
WHERE parent_id > 0

-------------------------------------
-- Habilitar e desabilitar as trigger
-------------------------------------
DISABLE TRIGGER AuditarContratos ON Contratos;
ENABLE TRIGGER AuditarContratos ON Contratos;

DISABLE TRIGGER RestricaoFinaisDeSemana ON Contratos;
ENABLE TRIGGER RestricaoFinaisDeSemana ON Contratos;

-------------------------------------
-- Verifica Status das triggers
-------------------------------------
SELECT name, is_disabled
FROM sys.triggers;

