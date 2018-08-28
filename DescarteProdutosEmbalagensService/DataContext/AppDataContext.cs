using DescarteService.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace DescarteService.DataContext{
    
    public class AppDataContext : DbContext{
        public AppDataContext(DbContextOptions<AppDataContext> options) : base (options)
        {

        }
        public DbSet<Produto> Produtos {get;set;}

        public DbSet<Estoque> Estoques {get;set;}
        public DbSet<Fornecedor> Fornecedores {get;set;}
        public DbSet<Revendedor> Revendedores {get;set;}

        public DbSet<FornecedorProduto> FornecedorProduto {get;set;}

        public DbSet<RevendedorProduto> RevendedorProduto {get;set;}


        protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
            builder.Entity<FornecedorProduto>()
            .HasKey(t => new { t.FornecedorId, t.ProdutoId });
            
            builder.Entity<RevendedorProduto>()
            .HasKey(t => new { t.RevendedorId, t.ProdutoId });
    }
    }
}