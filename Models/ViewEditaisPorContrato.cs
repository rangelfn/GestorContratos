using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class ViewEditaisPorContrato
{
    public string UnidadeGestora { get; set; } = null!;

    public string ProcessoSei { get; set; } = null!;

    public string Contratada { get; set; } = null!;

    public string Objeto { get; set; } = null!;

    public string Modalidade { get; set; } = null!;

    public decimal? Valor { get; set; }

    public string Numero { get; set; } = null!;

    public string EditalTipo { get; set; } = null!;

    public DateTime DataEdital { get; set; }
}
