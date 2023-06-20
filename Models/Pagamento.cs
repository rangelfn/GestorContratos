using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class Pagamento
{
    public int PagamentoId { get; set; }

    public string NotaLancamento { get; set; } = null!;

    public string PreparacaoPagamento { get; set; } = null!;

    public string OrdemBancaria { get; set; } = null!;

    public decimal Valor { get; set; }

    public DateTime DataPagamento { get; set; }

    public string Parcela { get; set; } = null!;

    public virtual ICollection<PagamentosTipo> PagamentosTipos { get; set; } = new List<PagamentosTipo>();
}
