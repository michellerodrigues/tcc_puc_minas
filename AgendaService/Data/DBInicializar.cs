using AgendaService.DataContext;
using AgendaService.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AgendaService.Data
{
    public static class DBInicializar
    {
        public static void StartDataBase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDataContext>();
                if(context.Database.EnsureCreated())
                {
                    var resp1 = new Responsavel { NomeResponsavel="Michelle Rodrigues",Email="mica_fabricante@mailinator.com"};
                    var resp2 = new Responsavel { NomeResponsavel="Cleiton Rodrigues",Email="cleiton_revendedor@mailinator.com"};
                    var resp3 = new Responsavel { NomeResponsavel="Cleber Rodrigues",Email="cleber_fabricante@mailinator.com"};
                                        
                    context.Responsaveis.Add(resp1);
                    context.Responsaveis.Add(resp2);
                    context.Responsaveis.Add(resp3);

                    context.SaveChanges();
                }
                
            }
        }
    }
}