-- Criação da stored procedure
CREATE PROCEDURE RealizarBackupDiario
AS
BEGIN
    DECLARE @NomeDoBancoDeDados NVARCHAR(128)
    DECLARE @NomeDoArquivo NVARCHAR(128)
    DECLARE @CaminhoDoArquivo NVARCHAR(256)
    DECLARE @ComandoSQL NVARCHAR(500)

    -- Nome do banco de dados que deseja fazer backup
    SET @NomeDoBancoDeDados = 'GestorContratos'

    -- Geração do nome do arquivo de backup com a data e hora atual
    SET @NomeDoArquivo = 'BKP_GestorContratos_' + REPLACE(REPLACE(CONVERT(NVARCHAR(20), GETDATE(), 120), '-', ''), ' ', '_') + '.bak'

    -- Caminho completo para salvar o arquivo de backup
    SET @CaminhoDoArquivo = 'C:\00Projetos\GestorContratos\' + @NomeDoArquivo

    -- Comando SQL para realizar o backup
    SET @ComandoSQL = 'BACKUP DATABASE ' + QUOTENAME(@NomeDoBancoDeDados) + ' TO DISK = ''' + @CaminhoDoArquivo + ''''

    -- Executar o backup
    EXEC sp_executesql @ComandoSQL
END
