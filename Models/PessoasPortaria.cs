using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class PessoasPortaria
{
    public int ResolucaoId { get; set; }

    public string TipoPortaria { get; set; } = null!;

    public string FuncaoPessoa { get; set; } = null!;

    public int? PessoaId { get; set; }

    public int? PortariaId { get; set; }

    public virtual Pessoa? Pessoa { get; set; }

    public virtual Portaria? Portaria { get; set; }
}
