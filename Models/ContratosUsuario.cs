using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class ContratosUsuario
{
    public int ContratosUsuariosId { get; set; }

    public int? UsuarioId { get; set; }

    public int? ContratoId { get; set; }

    public virtual Contrato? Contrato { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
