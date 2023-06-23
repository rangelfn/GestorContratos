using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class ViewContratosPagamento
{
    public string UnidadeGestora { get; set; } = null!;

    public string ProcessoSei { get; set; } = null!;

    public string Contratada { get; set; } = null!;

    public string Objeto { get; set; } = null!;

    public string Modalidade { get; set; } = null!;

    public decimal? Valor { get; set; }

    public string NotaLancamento { get; set; } = null!;

    public string PreparacaoPagamento { get; set; } = null!;

    public string OrdemBancaria { get; set; } = null!;

    public DateTime DataPagamento { get; set; }

    public decimal ValorPagamento { get; set; }

    public string NotaEmpenho { get; set; } = null!;

    public string Tipo { get; set; } = null!;
}
