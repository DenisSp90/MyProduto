using iText.Commons.Actions.Contexts;
using Microsoft.EntityFrameworkCore;
using MyProduto.Models;

namespace MyProduto.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar o relacionamento entre Produto e MovimentacaoEstoque
        modelBuilder.Entity<Produto>()
            .HasMany(p => p.MovimentacoesEstoque)
            .WithOne(m => m.Produto)
            .HasForeignKey(m => m.ProdutoId);

        // Configurar o carregamento automático das MovimentacoesEstoque ao carregar Produto
        modelBuilder.Entity<Produto>()
            .Navigation(p => p.MovimentacoesEstoque)
            .AutoInclude();
    }
}
