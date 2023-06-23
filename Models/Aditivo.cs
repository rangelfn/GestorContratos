using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class Aditivo
{
    public int AditivoId { get; set; }

    public string Numero { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public DateTime? DataAditivos { get; set; }

    public decimal Valor { get; set; }

    public int? ContratoId { get; set; }

    public virtual Contrato? Contrato { get; set; }
}
