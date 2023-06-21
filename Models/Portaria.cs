using System;
using System.Collections.Generic;

namespace GestorContratos.Models;
public partial class Portaria
{
    public int PortariaId { get; set; }
    public string Numero { get; set; } = null!;
    public string ProtocoloDiof { get; set; } = null!;
    public string UnidadeGestora { get; set; } = null!;
    public DateTime? DataPublicacao { get; set; }
    public int? ContratoId { get; set; }
    public virtual Contrato? Contrato { get; set; }
    public virtual ICollection<Resolucao> Resolucos { get; set; } = new List<Resolucao>();
}
