using System;
using System.Collections.Generic;

namespace GestorContratos.Models;
public partial class Auditoria
{
    public string? Tabela { get; set; }
    public string? Acao { get; set; }
    public string? Usuario { get; set; }
    public DateTime? DataHora { get; set; }
    public string? Chave { get; set; }
    public string? Antes { get; set; }
    public string? Depois { get; set; }
}
