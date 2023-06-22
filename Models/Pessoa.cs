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
    public virtual ICollection<Resolucao> Resolucos { get; set; } = new List<Resolucao>();
}
