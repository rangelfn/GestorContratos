using System;
using System.Collections.Generic;

namespace GestorContratos.Models;
public partial class Despesa
{
    public int DespesaId { get; set; }
    public string? Programa { get; set; }
    public string? Acao { get; set; }
    public string? Fonte { get; set; }
    public string? Natureza { get; set; }
    public string? Elemento { get; set; }
    public int? ContratoId { get; set; }
    public virtual Contrato? Contrato { get; set; }
}
