using EstoqueService.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace EstoqueService.DataContext{
    
    public class AppDataContext : DbContext{

        public AppDataContext(DbContextOptions<AppDataContext> options) : base (options)
        {

        }
        public DbSet<Produto> Produtos {get;set;}

        public DbSet<Estoque> Estoques {get;set;}
        public DbSet<Fornecedor> Fornecedores {get;set;}
        public DbSet<Revendedor> Revendedores {get;set;}

        protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);            
        }
    }
}