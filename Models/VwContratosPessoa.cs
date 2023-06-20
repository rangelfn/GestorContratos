using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class VwContratosPessoa
{
    public int ContratoId { get; set; }

    public string UnidadeGestora { get; set; } = null!;

    public string Extrato { get; set; } = null!;

    public string Contratante { get; set; } = null!;

    public string Contratada { get; set; } = null!;

    public string Objeto { get; set; } = null!;

    public int Vigencia { get; set; }

    public DateTime ContratoDataInicio { get; set; }

    public string ProcessoSei { get; set; } = null!;

    public string LinkPublico { get; set; } = null!;

    public DateTime DataAssinatura { get; set; }

    public string ProtocoloDiof { get; set; } = null!;

    public bool? Modalidade { get; set; }

    public decimal? Valor { get; set; }

    public string? Matricula { get; set; }

    public string? Nome { get; set; }

    public string? Cpf { get; set; }

    public string? PessoaUg { get; set; }

    public string? Setor { get; set; }

    public DateTime? PessoaDataInicio { get; set; }

    public DateTime? DataFim { get; set; }
}
