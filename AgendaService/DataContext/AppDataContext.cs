using AgendaService.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace AgendaService.DataContext{
    
    public class AppDataContext : DbContext{

        public AppDataContext(DbContextOptions<AppDataContext> options) : base (options)
        {

        }
        public DbSet<Agenda> Agendas {get;set;}
        public DbSet<Responsavel> Responsaveis {get;set;}

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);            
        }
    }
}