using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;


namespace GestorContratos.Models;

public partial class GestorContratosContext : DbContext
{
    public GestorContratosContext()
    {
    }

    public GestorContratosContext(DbContextOptions<GestorContratosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aditivo> Aditivos { get; set; }

    public virtual DbSet<Apostilamento> Apostilamentos { get; set; }

    public virtual DbSet<Auditoria> Auditorias { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<ContratosUsuario> ContratosUsuarios { get; set; }

    public virtual DbSet<Despesa> Despesas { get; set; }

    public virtual DbSet<Edital> Editais { get; set; }

    public virtual DbSet<Pagamento> Pagamentos { get; set; }

    public virtual DbSet<PagamentosTipo> PagamentosTipos { get; set; }

    public virtual DbSet<Pessoa> Pessoas { get; set; }

    public virtual DbSet<Portaria> Portarias { get; set; }

    public virtual DbSet<Resoluco> Resolucoes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VwContratosDespesa> VwContratosDespesas { get; set; }

    public virtual DbSet<VwContratosEdital> VwContratosEditais { get; set; }

    public virtual DbSet<VwContratosPagamento> VwContratosPagamentos { get; set; }

    public virtual DbSet<VwContratosPessoa> VwContratosPessoas { get; set; }

    public virtual DbSet<VwContratosPortaria> VwContratosPortarias { get; set; }

    public virtual DbSet<VwContratosValorTotalPagamento> VwContratosValorTotalPagamentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aditivo>(entity =>
        {
            entity.HasKey(e => e.AditivoId).HasName("PK__Aditivos__D53C3AD1CE0B6173");

            entity.Property(e => e.AditivoId)
                .ValueGeneratedNever()
                .HasColumnName("AditivoID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataFim).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Contrato).WithMany(p => p.Aditivos)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Aditivos__Contra__3D5E1FD2");
        });

        modelBuilder.Entity<Apostilamento>(entity =>
        {
            entity.HasKey(e => e.ApostilamentoId).HasName("PK__Apostila__7A79C7433A05EB18");

            entity.Property(e => e.ApostilamentoId)
                .ValueGeneratedNever()
                .HasColumnName("ApostilamentoID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataFim).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Contrato).WithMany(p => p.Apostilamentos)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Apostilam__Contr__412EB0B6");
        });

        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Acao)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Antes)
                .HasMaxLength(4000)
                .IsUnicode(false);
            entity.Property(e => e.Chave)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DataHora).HasColumnType("datetime");
            entity.Property(e => e.Depois)
                .HasMaxLength(4000)
                .IsUnicode(false);
            entity.Property(e => e.Tabela)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(70)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.ContratoId).HasName("PK__Contrato__B238E95376BC7AAD");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("AuditarContratos");
                    tb.HasTrigger("RestricaoFinaisDeSemana");
                });

            entity.Property(e => e.ContratoId)
                .ValueGeneratedNever()
                .HasColumnName("ContratoID");
            entity.Property(e => e.Contratada)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Contratante)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.Extrato)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LinkPublico)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Objeto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProcessoSei)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UnidadeGestora)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ContratosUsuario>(entity =>
        {
            entity.HasKey(e => e.ContratosUsuariosId).HasName("PK__Contrato__C6EFEE0128F5075A");

            entity.Property(e => e.ContratosUsuariosId).HasColumnName("ContratosUsuariosID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Contrato).WithMany(p => p.ContratosUsuarios)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Contratos__Contr__5DCAEF64");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ContratosUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Contratos__Usuar__5CD6CB2B");
        });

        modelBuilder.Entity<Despesa>(entity =>
        {
            entity.HasKey(e => e.DespesaId).HasName("PK__Despesas__5AAE1C8BAC316BB0");

            entity.Property(e => e.DespesaId)
                .ValueGeneratedNever()
                .HasColumnName("DespesaID");
            entity.Property(e => e.Acao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.Elemento)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Fonte)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Natureza)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Programa)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Contrato).WithMany(p => p.Despesas)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Despesas__Contra__4CA06362");
        });

        modelBuilder.Entity<Edital>(entity =>
        {
            entity.HasKey(e => e.EditalId).HasName("PK__Editais__02E8E87380EBF855");

            entity.Property(e => e.EditalId)
                .ValueGeneratedNever()
                .HasColumnName("EditalID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataPublicacao).HasColumnType("date");
            entity.Property(e => e.LinkPublico)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Contrato).WithMany(p => p.Editais)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Editais__Contrat__398D8EEE");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.PagamentoId).HasName("PK__Pagament__977DE7D31FE3B5B6");

            entity.Property(e => e.PagamentoId)
                .ValueGeneratedNever()
                .HasColumnName("PagamentoID");
            entity.Property(e => e.DataPagamento).HasColumnType("date");
            entity.Property(e => e.NotaLancamento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OrdemBancaria)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Parcela)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PreparacaoPagamento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<PagamentosTipo>(entity =>
        {
            entity.HasKey(e => e.NotaEmpenho).HasName("PK__Pagament__92028342B71256CE");

            entity.ToTable("PagamentosTipo");

            entity.Property(e => e.NotaEmpenho).ValueGeneratedNever();
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataCadastro).HasColumnType("date");
            entity.Property(e => e.PagamentoId).HasColumnName("PagamentoID");

            entity.HasOne(d => d.Contrato).WithMany(p => p.PagamentosTipos)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Pagamento__Contr__45F365D3");

            entity.HasOne(d => d.Pagamento).WithMany(p => p.PagamentosTipos)
                .HasForeignKey(d => d.PagamentoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Pagamento__Pagam__46E78A0C");
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.PessoaId).HasName("PK__Pessoas__2F5F5632A23D07BD");

            entity.Property(e => e.PessoaId)
                .ValueGeneratedNever()
                .HasColumnName("PessoaID");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("CPF");
            entity.Property(e => e.DataFim).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.Matricula)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Setor)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Ug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("UG");
        });

        modelBuilder.Entity<Portaria>(entity =>
        {
            entity.HasKey(e => e.PortariaId).HasName("PK__Portaria__19534B50087C6CAC");

            entity.Property(e => e.PortariaId)
                .ValueGeneratedNever()
                .HasColumnName("PortariaID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataPublicacao).HasColumnType("date");
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UnidadeGestora)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Contrato).WithMany(p => p.Portaria)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Portarias__Contr__4F7CD00D");
        });

        modelBuilder.Entity<Resoluco>(entity =>
        {
            entity.HasKey(e => e.ResolucaoId).HasName("PK__Resoluco__646E61B3B8E9679C");

            entity.Property(e => e.ResolucaoId).HasColumnName("ResolucaoID");
            entity.Property(e => e.DataFim).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.PessoaId).HasColumnName("PessoaID");
            entity.Property(e => e.PortariaId).HasColumnName("PortariaID");
            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Pessoa).WithMany(p => p.Resolucos)
                .HasForeignKey(d => d.PessoaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Resolucoe__Pesso__5812160E");

            entity.HasOne(d => d.Portaria).WithMany(p => p.Resolucos)
                .HasForeignKey(d => d.PortariaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Resolucoe__Porta__571DF1D5");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798672D3ED1");

            entity.Property(e => e.UsuarioId)
                .ValueGeneratedNever()
                .HasColumnName("UsuarioID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LoginCpf)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LoginCPF");
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwContratosDespesa>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwContratosDespesas");

            entity.Property(e => e.Acao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Contratada)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Contratante)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.Elemento)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Extrato)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fonte)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.LinkPublico)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Natureza)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Objeto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProcessoSei)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Programa)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UnidadeGestora)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VwContratosEdital>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwContratosEditais");

            entity.Property(e => e.Contratada)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Contratante)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.EditalDataPublicacao).HasColumnType("date");
            entity.Property(e => e.EditalLink)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EditalNumero)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Extrato)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LinkPublico)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Objeto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProcessoSei)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UnidadeGestora)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VwContratosPagamento>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwContratosPagamentos");

            entity.Property(e => e.Contratada)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Contratante)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataCadastro).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.DataPagamento).HasColumnType("date");
            entity.Property(e => e.Extrato)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LinkPublico)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NotaLancamento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Objeto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OrdemBancaria)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Parcela)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PreparacaoPagamento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProcessoSei)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UnidadeGestora)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorPagamento).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VwContratosPessoa>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwContratosPessoas");

            entity.Property(e => e.Contratada)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Contratante)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContratoDataInicio).HasColumnType("date");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("CPF");
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataFim).HasColumnType("date");
            entity.Property(e => e.Extrato)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LinkPublico)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Matricula)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Objeto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PessoaDataInicio).HasColumnType("date");
            entity.Property(e => e.PessoaUg)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PessoaUG");
            entity.Property(e => e.ProcessoSei)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Setor)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UnidadeGestora)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VwContratosPortaria>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwContratosPortarias");

            entity.Property(e => e.Contratada)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Contratante)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.Extrato)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LinkPublico)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Objeto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PortariaDataPublicacao).HasColumnType("date");
            entity.Property(e => e.PortariaNumero)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PortariaProtocolo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PortariaUnidadeGestora)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProcessoSei)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UnidadeGestora)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VwContratosValorTotalPagamento>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwContratosValorTotalPagamentos");

            entity.Property(e => e.Contratada)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Contratante)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.Extrato)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LinkPublico)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Objeto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProcessoSei)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UnidadeGestora)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorTotalPagamentos).HasColumnType("decimal(38, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
