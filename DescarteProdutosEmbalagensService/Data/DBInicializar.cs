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
                    var lote2 = new LoteDescarte { NomeResponsavelDescarte="Fornecedor 1",EmailResponsavelDescarte="mica-fornecedor1@mailinator.com"};
           
                    context.LoteDescartes.Add(lote1);
                    context.LoteDescartes.Add(lote2);
    
                    var produto1 = new ProdutoDescarte { Nome="Cupinicida 3MAX ", DataVecimentoProduto = DateTime.Now.AddDays(45), IdITemEstoque = new Guid(),LoteDescarte=lote1}; 
                    var produto2 = new ProdutoDescarte { Nome="Herbicidas Hortalis", DataVecimentoProduto = DateTime.Now.AddDays(30), IdITemEstoque = new Guid(), LoteDescarte=lote1}; 
                    var produto3 = new ProdutoDescarte { Nome="Inseticida Fatalis",DataVecimentoProduto = DateTime.Now.AddDays(15), IdITemEstoque = new Guid(),LoteDescarte=lote2}; 
                    
                    context.ProdutoDescartes.Add(produto1);
                    context.ProdutoDescartes.Add(produto2);
                    context.ProdutoDescartes.Add(produto3);

                    var agendamento1 = new ComunicadosDeAgendamentoEnviados {DataPropostaAgendamento = DateTime.Now.AddDays(90), DataEnvioEmail = DateTime.Now,LoteDescarte = lote1,StatusProposta="Pendente Despacho"};
                    var agendamento2 = new ComunicadosDeAgendamentoEnviados { DataPropostaAgendamento = DateTime.Now.AddDays(90), DataEnvioEmail = DateTime.Now,LoteDescarte = lote2};
                    
                    context.AgendamentoDescartes.Add(agendamento1);
                    context.AgendamentoDescartes.Add(agendamento2);
                    context.SaveChanges();
                }
                
            }
        }
    }
}