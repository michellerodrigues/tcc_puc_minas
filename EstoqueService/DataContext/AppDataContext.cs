using EstoqueService.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace EstoqueService.DataContext{
    
    public class AppDataContext : DbContext{

        public AppDataContext(DbContextOptions<AppDataContext> options) : base (options)
        {

        }
        public DbSet<Produto> Produtos {get;set;}

        public DbSet<Estoque> Estoques {get;set;}
        public DbSet<Fabricante> Fabricantes {get;set;}
        public DbSet<Revendedor> Revendedores {get;set;}

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            /*
            builder.Entity<Estoque>().HasOne(p => p.Produto).WithMany(e => e.Estoques);
            builder.Entity<Estoque>().HasOne(p => p.Revendedor).WithMany(e => e.Estoques);
            builder.Entity<Estoque>().HasOne(p => p.Fabricante).WithMany(e => e.Estoques);

            builder.Entity<Produto>().HasMany(p => p.Estoques).WithOne(e => e.Produto);
            builder.Entity<Fabricante>().HasMany(p => p.Estoques).WithOne(e => e.Fabricante);
            builder.Entity<Revendedor>().HasMany(p => p.Estoques).WithOne(e => e.Revendedor); */
        }
    }
}