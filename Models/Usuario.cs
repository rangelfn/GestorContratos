using System;
using System.Collections.Generic;

namespace GestorContratos.Models;
public partial class Usuario
{
    public int UsuarioId { get; set; }
    public string LoginCpf { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public virtual ICollection<UsuariosContrato> UsuariosContratos { get; set; } = new List<UsuariosContrato>();
}
