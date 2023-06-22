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
    public virtual DbSet<Despesa> Despesas { get; set; }
    public virtual DbSet<Edital> Editais { get; set; }
    public virtual DbSet<Pagamento> Pagamentos { get; set; }
    public virtual DbSet<PagamentosTipo> PagamentosTipos { get; set; }
    public virtual DbSet<Pessoa> Pessoas { get; set; }
    public virtual DbSet<Portaria> Portarias { get; set; }
    public virtual DbSet<Resolucao> Resolucoes { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<UsuariosContrato> UsuariosContratos { get; set; }
    public virtual DbSet<VwContratosDespesa> VwContratosDespesas { get; set; }
    public virtual DbSet<VwContratosEditais> VwContratosEditais { get; set; }
    public virtual DbSet<VwContratosPagamento> VwContratosPagamentos { get; set; }
    public virtual DbSet<VwContratosPessoa> VwContratosPessoas { get; set; }
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
            entity.HasKey(e => e.AditivoId).HasName("PK__Aditivos__D53C3AD1AEA995BE");

            entity.Property(e => e.AditivoId).ValueGeneratedNever().HasColumnName("AditivoID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataFim).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("datetime");
            entity.Property(e => e.Descricao).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Numero).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Contrato).WithMany(p => p.Aditivos)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Aditivos__Contra__681373AD");
        });

        modelBuilder.Entity<Apostilamento>(entity =>
        {
            entity.HasKey(e => e.ApostilamentoId).HasName("PK__Apostila__7A79C74380928215");

            entity.Property(e => e.ApostilamentoId).ValueGeneratedNever().HasColumnName("ApostilamentoID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataFim).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.Descricao).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Numero).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Contrato).WithMany(p => p.Apostilamentos)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Apostilam__Contr__6BE40491");
        });

        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Acao).HasMaxLength(10).IsUnicode(false);
            entity.Property(e => e.Antes).HasMaxLength(4000).IsUnicode(false);
            entity.Property(e => e.Chave).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.DataHora).HasColumnType("datetime");
            entity.Property(e => e.Depois).HasMaxLength(4000).IsUnicode(false);
            entity.Property(e => e.Tabela).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Usuario).HasMaxLength(70).IsUnicode(false);
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.ContratoId).HasName("PK__Contrato__B238E953EDFA1AB3");

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

        modelBuilder.Entity<Despesa>(entity =>
        {
            entity.HasKey(e => e.DespesaId).HasName("PK__Despesas__5AAE1C8B31F4FD8E");

            entity.Property(e => e.DespesaId).ValueGeneratedNever().HasColumnName("DespesaID");
            entity.Property(e => e.Acao).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.Elemento).HasMaxLength(8).IsUnicode(false);
            entity.Property(e => e.Fonte).HasMaxLength(12).IsUnicode(false);
            entity.Property(e => e.Natureza).HasMaxLength(14).IsUnicode(false);
            entity.Property(e => e.Programa).HasMaxLength(255).IsUnicode(false);

            entity.HasOne(d => d.Contrato).WithMany(p => p.Despesas)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Despesas__Contra__7755B73D");
        });

        modelBuilder.Entity<Edital>(entity =>
        {
            entity.HasKey(e => e.EditalId).HasName("PK__Editais__02E8E87336B68DC8");

            entity.Property(e => e.EditalId).ValueGeneratedNever().HasColumnName("EditalID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataPublicacao).HasColumnType("date");
            entity.Property(e => e.LinkPublico).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Numero).HasMaxLength(255).IsUnicode(false);

            entity.HasOne(d => d.Contrato).WithMany(p => p.Editais)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Editais__Contrat__6442E2C9");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.PagamentoId).HasName("PK__Pagament__977DE7D39E50FBF9");

            entity.Property(e => e.PagamentoId).ValueGeneratedNever().HasColumnName("PagamentoID");
            entity.Property(e => e.DataPagamento).HasColumnType("date");
            entity.Property(e => e.NotaLancamento).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.OrdemBancaria).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Parcela).HasMaxLength(10).IsUnicode(false);
            entity.Property(e => e.PreparacaoPagamento).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<PagamentosTipo>(entity =>
        {
            entity.HasKey(e => e.NotaEmpenho).HasName("PK__Pagament__92028342918DE517");

            entity.ToTable("PagamentosTipo");

            entity.Property(e => e.NotaEmpenho).ValueGeneratedNever();
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataCadastro).HasColumnType("date");
            entity.Property(e => e.PagamentoId).HasColumnName("PagamentoID");
            entity.Property(e => e.Tipo).HasMaxLength(255).IsUnicode(false);

            entity.HasOne(d => d.Contrato).WithMany(p => p.PagamentosTipos)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Pagamento__Contr__70A8B9AE");

            entity.HasOne(d => d.Pagamento).WithMany(p => p.PagamentosTipos)
                .HasForeignKey(d => d.PagamentoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Pagamento__Pagam__719CDDE7");
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.PessoaId).HasName("PK__Pessoas__2F5F563281C0F90B");

            entity.Property(e => e.PessoaId).ValueGeneratedNever().HasColumnName("PessoaID");
            entity.Property(e => e.Cpf).HasMaxLength(11).IsUnicode(false).HasColumnName("CPF");
            entity.Property(e => e.Matricula).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Nome).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Setor).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Ug).HasMaxLength(255).IsUnicode(false).HasColumnName("UG");
        });

        modelBuilder.Entity<Portaria>(entity =>
        {
            entity.HasKey(e => e.PortariaId).HasName("PK__Portaria__19534B508D52228C");

            entity.Property(e => e.PortariaId).ValueGeneratedNever().HasColumnName("PortariaID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataPublicacao).HasColumnType("date");
            entity.Property(e => e.Numero).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);

            entity.HasOne(d => d.Contrato).WithMany(p => p.Portaria)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Portarias__Contr__7A3223E8");
        });

        modelBuilder.Entity<Resolucao>(entity =>
        {
            entity.HasKey(e => e.ResolucaoId).HasName("PK__Resoluco__646E61B3D1DF8102");

            entity.Property(e => e.ResolucaoId).HasColumnName("ResolucaoID");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.PessoaId).HasColumnName("PessoaID");
            entity.Property(e => e.PortariaId).HasColumnName("PortariaID");
            entity.Property(e => e.Tipo).HasMaxLength(255).IsUnicode(false);

            entity.HasOne(d => d.Pessoa).WithMany(p => p.Resolucos)
                .HasForeignKey(d => d.PessoaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Resolucoe__Pesso__02C769E9");

            entity.HasOne(d => d.Portaria).WithMany(p => p.Resolucos)
                .HasForeignKey(d => d.PortariaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Resolucoe__Porta__01D345B0");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798A0B99FF4");

            entity.Property(e => e.UsuarioId).ValueGeneratedNever().HasColumnName("UsuarioID");
            entity.Property(e => e.Email).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.LoginCpf).HasMaxLength(255).IsUnicode(false).HasColumnName("LoginCPF");
            entity.Property(e => e.Senha).HasMaxLength(255).IsUnicode(false);
        });

        modelBuilder.Entity<UsuariosContrato>(entity =>
        {
            entity.HasKey(e => e.UsuariosContratosId).HasName("PK__Usuarios__024B63CA53FC4B88");

            entity.Property(e => e.UsuariosContratosId).HasColumnName("UsuariosContratosID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Contrato).WithMany(p => p.UsuariosContratos)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UsuariosC__Contr__0880433F");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuariosContratos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UsuariosC__Usuar__078C1F06");
        });

        modelBuilder.Entity<VwContratosDespesa>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwContratosDespesas");

            entity.Property(e => e.Acao).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Contratada).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Contratante).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.Elemento).HasMaxLength(8).IsUnicode(false);
            entity.Property(e => e.Extrato).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Fonte).HasMaxLength(12).IsUnicode(false);
            entity.Property(e => e.LinkPublico).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Natureza).HasMaxLength(14).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Programa).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VwContratosEditais>(entity =>
        {
            entity
                .HasNoKey().ToView("vwContratosEditais");

            entity.Property(e => e.Contratada).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Contratante).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.EditalDataPublicacao).HasColumnType("date");
            entity.Property(e => e.EditalLink).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.EditalNumero).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Extrato).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.LinkPublico).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
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
            entity.Property(e => e.NotaLancamento).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.OrdemBancaria).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Parcela).HasMaxLength(10).IsUnicode(false);
            entity.Property(e => e.PreparacaoPagamento).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorPagamento).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VwContratosPessoa>(entity =>
        {
            entity.HasNoKey().ToView("vwContratosPessoas");

            entity.Property(e => e.Contratada).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Contratante).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ContratoDataInicio).HasColumnType("date");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.Cpf).HasMaxLength(11).IsUnicode(false).HasColumnName("CPF");
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.Extrato).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.LinkPublico).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Matricula).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Nome).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.PessoaUg).HasMaxLength(255).IsUnicode(false).HasColumnName("PessoaUG");
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Setor).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VwContratosValorTotalPagamento>(entity =>
        {
            entity.HasNoKey().ToView("vwContratosValorTotalPagamentos");

            entity.Property(e => e.Contratada).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Contratante).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.DataAssinatura).HasColumnType("date");
            entity.Property(e => e.DataInicio).HasColumnType("date");
            entity.Property(e => e.Extrato).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.LinkPublico).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Objeto).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProcessoSei).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ProtocoloDiof).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.UnidadeGestora).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorTotalPagamentos).HasColumnType("decimal(38, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
