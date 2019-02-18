using TriagemService.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace TriagemService.DataContext{
    
    public class AppDataContext : DbContext{

        public AppDataContext(DbContextOptions<AppDataContext> options) : base (options)
        {

        }
        public DbSet<Triagem> Triagens {get;set;}
        public DbSet<Operador> Operadores {get;set;}

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);            
        }
    }
}