using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class Edital
{
    public int EditalId { get; set; }

    public string Numero { get; set; } = null!;

    public string EditalTipo { get; set; } = null!;

    public string LinkPublico { get; set; } = null!;

    public DateTime DataEdital { get; set; }

    public int? ContratoId { get; set; }

    public virtual Contrato? Contrato { get; set; }
}
