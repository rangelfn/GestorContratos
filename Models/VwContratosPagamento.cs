using System;
using System.Collections.Generic;

namespace GestorContratos.Models;
public partial class VwContratosPagamento
{
    public int ContratoId { get; set; }
    public string UnidadeGestora { get; set; } = null!;
    public string Extrato { get; set; } = null!;
    public string Contratante { get; set; } = null!;
    public string Contratada { get; set; } = null!;
    public string Objeto { get; set; } = null!;
    public int Vigencia { get; set; }
    public DateTime DataInicio { get; set; }
    public string ProcessoSei { get; set; } = null!;
    public string LinkPublico { get; set; } = null!;
    public DateTime DataAssinatura { get; set; }
    public string ProtocoloDiof { get; set; } = null!;
    public bool? Modalidade { get; set; }
    public decimal? Valor { get; set; }
    public int NotaEmpenho { get; set; }
    public bool? ModalidadePagamento { get; set; }
    public DateTime DataCadastro { get; set; }
    public string NotaLancamento { get; set; } = null!;
    public string PreparacaoPagamento { get; set; } = null!;
    public string OrdemBancaria { get; set; } = null!;
    public decimal ValorPagamento { get; set; }
    public DateTime DataPagamento { get; set; }
    public string Parcela { get; set; } = null!;
}
