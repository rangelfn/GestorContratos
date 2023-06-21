using System;
using System.Collections.Generic;

namespace GestorContratos.Models;
public partial class Resolucao
{
    public int ResolucaoId { get; set; }
    public string Tipo { get; set; } = null!;
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public int? PortariaId { get; set; }
    public int? PessoaId { get; set; }
    public virtual Pessoa? Pessoa { get; set; }
    public virtual Portaria? Portaria { get; set; }
}
