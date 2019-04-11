using DescarteService.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace DescarteService.DataContext{    
    public class AppDataContext : DbContext{
        public AppDataContext(DbContextOptions<AppDataContext> options) : base (options)
        {

        }
        public DbSet<ComunicadosDeAgendamentoEnviados> AgendamentoDescartes {get;set;}
        public DbSet<LoteDescarte> LoteDescartes {get;set;}
        public DbSet<ProdutoDescarte> ProdutoDescartes {get;set;}

        protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        }
    }
}