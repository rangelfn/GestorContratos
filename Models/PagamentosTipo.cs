using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class PagamentosTipo
{
    public int PagamentosTipo1 { get; set; }

    public string NotaEmpenho { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public DateTime DataCadastro { get; set; }

    public int? ContratoId { get; set; }

    public int? PagamentoId { get; set; }

    public virtual Contrato? Contrato { get; set; }

    public virtual Pagamento? Pagamento { get; set; }
}
