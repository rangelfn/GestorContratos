﻿using System;
using System.Collections.Generic;

namespace GestorContratos.Models;

public partial class Contrato
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

    public string Modalidade { get; set; } = null!;

    public decimal? Valor { get; set; }

    public virtual ICollection<Aditivo> Aditivos { get; set; } = new List<Aditivo>();

    public virtual ICollection<Apostilamento> Apostilamentos { get; set; } = new List<Apostilamento>();

    public virtual ICollection<DespesaOrcamentaria> DespesasOrcamentaria { get; set; } = new List<DespesaOrcamentaria>();

    public virtual ICollection<Edital> Editais { get; set; } = new List<Edital>();

    public virtual ICollection<PagamentosTipo> PagamentosTipos { get; set; } = new List<PagamentosTipo>();

    public virtual ICollection<Portaria> Portaria { get; set; } = new List<Portaria>();

    public virtual ICollection<UsuariosContrato> UsuariosContratos { get; set; } = new List<UsuariosContrato>();
}
