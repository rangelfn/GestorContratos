using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class VwDespesasPorContrato
{
    public string UnidadeGestora { get; set; } = null!;

    public string ProcessoSei { get; set; } = null!;

    public string Contratada { get; set; } = null!;

    public string Objeto { get; set; } = null!;

    public string Modalidade { get; set; } = null!;

    public decimal? Valor { get; set; }

    public string? Programa { get; set; }

    public string? Acao { get; set; }

    public string? Fonte { get; set; }

    public string? Natureza { get; set; }

    public string? Elemento { get; set; }
}
