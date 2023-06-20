using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class Pessoa
{
    public int PessoaId { get; set; }

    public string Matricula { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Ug { get; set; } = null!;

    public string Setor { get; set; } = null!;

    public DateTime? DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public virtual ICollection<Resoluco> Resolucos { get; set; } = new List<Resoluco>();
}
