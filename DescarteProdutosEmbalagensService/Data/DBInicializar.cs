using DescarteService.DataContext;
using DescarteService.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DescarteService.Data
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

                    var lote1 = new LoteDescarte { NomeResponsavelDescarte="Michelle Rodrigues 1 ",EmailResponsavelDescarte="mica-revenda1@mailinator.com"};

                    context.LoteDescartes.Add(lote1);
    
                    var produto1 = new ProdutoDescarte { Nome="Cupinicida 3MAX ", DataVecimentoProduto = DateTime.Now.AddDays(45), IdITemEstoque = new Guid(),LoteDescarte=lote1}; 

                    context.ProdutoDescartes.Add(produto1);

                    var agendamento1 = new AgendamentoDescarteSolicitado {DataPropostaAgendamento = DateTime.Now.AddDays(90).ToString("yyyyMMdd"), DataEnvioEmail = DateTime.Now,LoteDescarte = lote1,StatusProposta="Pendente Despacho"};

                    context.AgendamentoDescarteSolicitados.Add(agendamento1);
                    context.SaveChanges();
                } 
                
            }
        }
    }
}