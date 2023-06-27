using System;
using System.Collections.Generic;
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
    public virtual DbSet<DespesaOrcamentaria> DespesasOrcamentaria { get; set; }
    public virtual DbSet<VwDespesasPorContrato> DespesasPorContratos { get; set; }
    public virtual DbSet<Edital> Editais { get; set; }
    public virtual DbSet<Pagamento> Pagamentos { get; set; }
    public virtual DbSet<PagamentosTipo> PagamentosTipos { get; set; }
    public virtual DbSet<Pessoa> Pessoas { get; set; }
    public virtual DbSet<PessoasPortaria> PessoasPortarias { get; set; }
    public virtual DbSet<Portaria> Portarias { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<UsuariosContrato> UsuariosContratos { get; set; }
    public virtual DbSet<ViewContratosPagamento> ViewContratosPagamentos { get; set; }
    public virtual DbSet<ViewEditaisPorContrato> ViewEditaisPorContratos { get; set; }
    public virtual DbSet<ViewPagamentosTotalPorContrato> ViewPagamentosTotalPorContratos { get; set; }
    public virtual DbSet<VwContratosPagamento> VwContratosPagamentos { get; set; }
    public virtual DbSet<VwPortariasPorContrato> VwPortariasPorContratos { get; set; }
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
        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Acao).HasMaxLength(10).IsUnicode(false);
            entity.Property(e => e.Antes).HasMaxLength(4000).IsUnicode(false);
            entity.Property(e => e.Chave).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.DataHora).HasColumnType("date");
            entity.Property(e => e.Depois).HasMaxLength(4000).IsUnicode(false);
            entity.Property(e => e.Tabela).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Usuario).HasMaxLength(70).IsUnicode(false);
        });

        modelBuilder.Entity<Aditivo>(entity =>
        {
            entity.HasKey(e => e.AditivoId).HasName("PK__Aditivos__D53C3AD11F63A710");

            entity.Property(e => e.AditivoId).HasColumnName("AditivoID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataAditivos).HasColumnType("date");
            entity.Property(e => e.Descricao).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Numero).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Contrato).WithMany(p => p.Aditivos)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Aditivos__Contra__151B244E");
        });

        modelBuilder.Entity<Apostilamento>(entity =>
        {
            entity.HasKey(e => e.ApostilamentoId).HasName("PK__Apostila__7A79C74302283868");

            entity.Property(e => e.ApostilamentoId).HasColumnName("ApostilamentoID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataAditivos).HasColumnType("date");
            entity.Property(e => e.Descricao).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Numero).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Contrato).WithMany(p => p.Apostilamentos)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Apostilam__Contr__17F790F9");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.ContratoId).HasName("PK__Contrato__B238E953AD3BC4EE");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("AuditarContratos");
                    tb.HasTrigger("RestricaoFinaisDeSemana");
                });

            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.Contratada).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Contratante).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.Extrato).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.LinkPublico).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Modalidade).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(4000).IsUnicode(false);
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<DespesaOrcamentaria>(entity =>
        {
            entity.HasKey(e => e.DespesaId).HasName("PK__Despesas__5AAE1C8B8887EE42");

            entity.Property(e => e.DespesaId).HasColumnName("DespesaID");
            entity.Property(e => e.Acao).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.Elemento).HasMaxLength(8).IsUnicode(false);
            entity.Property(e => e.Fonte).HasMaxLength(12).IsUnicode(false);
            entity.Property(e => e.Natureza).HasMaxLength(14).IsUnicode(false);
            entity.Property(e => e.Programa).HasMaxLength(255).IsUnicode(false);

            entity.HasOne(d => d.Contrato).WithMany(p => p.DespesasOrcamentaria)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DespesasO__Contr__236943A5");
        });

        modelBuilder.Entity<Edital>(entity =>
        {
            entity.HasKey(e => e.EditalId).HasName("PK__Editais__02E8E873F94B7613");

            entity.Property(e => e.EditalId).HasColumnName("EditalID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataEdital).HasColumnType("date");
            entity.Property(e => e.EditalTipo).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.LinkPublico).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Numero).HasMaxLength(255).IsUnicode(false);

            entity.HasOne(d => d.Contrato).WithMany(p => p.Editais)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Editais__Contrat__123EB7A3");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.PagamentoId).HasName("PK__Pagament__977DE7D3B9DE5FE4");

            entity.Property(e => e.PagamentoId).HasColumnName("PagamentoID");
            entity.Property(e => e.DataPagamento).HasColumnType("date");
            entity.Property(e => e.NotaLancamento).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.OrdemBancaria).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Parcela).HasMaxLength(10).IsUnicode(false);
            entity.Property(e => e.PreparacaoPagamento).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<PagamentosTipo>(entity =>
        {
            entity.HasKey(e => e.PagamentosTipo1).HasName("PK__Pagament__4737204CCA647014");

            entity.ToTable("PagamentosTipo");

            entity.Property(e => e.PagamentosTipo1).HasColumnName("PagamentosTipo");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataCadastro).HasColumnType("date");
            entity.Property(e => e.NotaEmpenho).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.PagamentoId).HasColumnName("PagamentoID");
            entity.Property(e => e.Tipo).HasMaxLength(255).IsUnicode(false);

            entity.HasOne(d => d.Contrato).WithMany(p => p.PagamentosTipos)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Pagamento__Contr__1CBC4616");

        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.PessoaId).HasName("PK__Pessoas__2F5F563265D87DFC");

            entity.Property(e => e.PessoaId).HasColumnName("PessoaID");
            entity.Property(e => e.Cpf).HasMaxLength(11).IsUnicode(false).HasColumnName("CPF");
            entity.Property(e => e.Matricula).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Nome).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Setor).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Ug).HasMaxLength(255).IsUnicode(false).HasColumnName("UG");
        });

        modelBuilder.Entity<PessoasPortaria>(entity =>
        {
            entity.HasKey(e => e.ResolucaoId).HasName("PK__PessoasP__646E61B35036A7C6");

            entity.Property(e => e.ResolucaoId).HasColumnName("ResolucaoID");
            entity.Property(e => e.FuncaoPessoa).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.PessoaId).HasColumnName("PessoaID");
            entity.Property(e => e.PortariaId).HasColumnName("PortariaID");
            entity.Property(e => e.TipoPortaria).HasMaxLength(255).IsUnicode(false);

            entity.HasOne(d => d.Pessoa).WithMany(p => p.PessoasPortaria)
                .HasForeignKey(d => d.PessoaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PessoasPo__Pesso__2CF2ADDF");

            entity.HasOne(d => d.Portaria).WithMany(p => p.PessoasPortaria)
                .HasForeignKey(d => d.PortariaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PessoasPo__Porta__2BFE89A6");
        });

        modelBuilder.Entity<Portaria>(entity =>
        {
            entity.HasKey(e => e.PortariaId).HasName("PK__Portaria__19534B50F851C538");

            entity.Property(e => e.PortariaId).HasColumnName("PortariaID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataPublicacao).HasColumnType("date");
            entity.Property(e => e.Numero).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Tipo).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);

            entity.HasOne(d => d.Contrato).WithMany(p => p.Portaria)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Portarias__Contr__2645B050");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798A5448495");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Email).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.LoginCpf).HasMaxLength(255).IsUnicode(false).HasColumnName("LoginCPF");
            entity.Property(e => e.Nome).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(255).IsUnicode(false);
        });

        modelBuilder.Entity<UsuariosContrato>(entity =>
        {
            entity.HasKey(e => e.UsuariosContratosId).HasName("PK__Usuarios__024B63CA914A6D56");

            entity.Property(e => e.UsuariosContratosId).HasColumnName("UsuariosContratosID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Contrato).WithMany(p => p.UsuariosContratos)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UsuariosC__Contr__32AB8735");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuariosContratos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UsuariosC__Usuar__31B762FC");
        });

        modelBuilder.Entity<ViewContratosPagamento>(entity =>
        {
            entity.HasNoKey().ToView("ViewContratosPagamentos");

            entity.Property(e => e.Contratada).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.DataPagamento).HasColumnType("date");
            entity.Property(e => e.Modalidade).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.NotaEmpenho).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.NotaLancamento).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(4000).IsUnicode(false);
            entity.Property(e => e.OrdemBancaria).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.PreparacaoPagamento).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Tipo).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorPagamento).HasColumnType("decimal(10, 2)");
        });
        modelBuilder.Entity<VwDespesasPorContrato>(entity =>
        {
            entity.HasNoKey().ToView("DespesasPorContratos");

            entity.Property(e => e.Acao).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Contratada).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Elemento).HasMaxLength(8).IsUnicode(false);
            entity.Property(e => e.Fonte).HasMaxLength(12).IsUnicode(false);
            entity.Property(e => e.Modalidade).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Natureza).HasMaxLength(14).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(4000).IsUnicode(false);
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Programa).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ViewEditaisPorContrato>(entity =>
        {
            entity.HasNoKey().ToView("ViewEditaisPorContratos");

            entity.Property(e => e.Contratada).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.DataEdital).HasColumnType("date");
            entity.Property(e => e.EditalTipo).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Modalidade).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Numero).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(4000).IsUnicode(false);
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ViewPagamentosTotalPorContrato>(entity =>
        {
            entity.HasNoKey().ToView("ViewPagamentosTotalPorContrato");

            entity.Property(e => e.Contratada).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Modalidade).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.NotasLancamento).HasMaxLength(8000).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(4000).IsUnicode(false);
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorTotalPagamentos).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<VwContratosPagamento>(entity =>
        {
            entity.HasNoKey().ToView("vwContratosPagamentos");

            entity.Property(e => e.Contratada).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Contratante).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataCadastro).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.DataPagamento).HasColumnType("date");
            entity.Property(e => e.Extrato).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.LinkPublico).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Modalidade).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ModalidadePagamento).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.NotaEmpenho).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.NotaLancamento).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(4000).IsUnicode(false);
            entity.Property(e => e.OrdemBancaria).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Parcela).HasMaxLength(10).IsUnicode(false);
            entity.Property(e => e.PreparacaoPagamento).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorPagamento).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VwPortariasPorContrato>(entity =>
        {
            entity.HasNoKey().ToView("vwPortariasPorContratos");

            entity.Property(e => e.Contratada).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.DataPublicacao).HasColumnType("date");
            entity.Property(e => e.Modalidade).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Numero).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(4000).IsUnicode(false);
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
