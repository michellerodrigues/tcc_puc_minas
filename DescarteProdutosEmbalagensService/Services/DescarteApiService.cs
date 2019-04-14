using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using DescarteService.Data.Interfaces;
using DescarteService.Data.Models;
using DescarteService.DataContext;
using DescarteService.Service.Utils;
using DescarteService.Services.Messages;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace DescarteService.Services
{
    public class DescarteApiService : IDescarteApiService
    {  
        AppDataContext _context;
 
        public DescarteApiService( AppDataContext context)
        {
            this._context = context;
        }



        static string EstoqueServicesURL = Startup.AppSettings.EstoqueServicesURL;
        public ObterProdutosVencidosMessageResponse ObterProdutosVencidos()
        {
           // var _factory = new DesignTimeDbContextFactory();

           var repository = new AgendamentoDescarteRepository(_context);


            ObterProdutosVencidosMessageResponse response = HttpRestClient.GetAsync<ObterProdutosVencidosMessageResponse>(string.Format("{0}/{1}", EstoqueServicesURL, (object)"vencidos")).GetAwaiter().GetResult();
            if (response != null)
            {            
             
                if(response.codRetorno!=1)
                {
                    //var produtosOrdenadosPorFabricante = response.LoteProdutosVecidos.OrderBy(p=>p.EmailFabricante).ToList();

                    var listaFabricantes = response.LoteProdutosVecidos.Select(x => x.EmailFabricante).Distinct().ToList();
                    
                    foreach(string emailFabricante in listaFabricantes)
                    {
                        
                        ComunicarDescartePendenteMessageRequest request = new ComunicarDescartePendenteMessageRequest();
                        DatasDisponiveisMessage data15 = new  DatasDisponiveisMessage(){Data=DateTime.Now.AddDays(15),LinkAgendamento=String.Format("http://localhost:9009/agendar?lote=1234&data={0}",DateTime.Now.AddDays(15).ToString("yyyyMMdd"))};
                        DatasDisponiveisMessage data30 = new  DatasDisponiveisMessage(){Data=DateTime.Now.AddDays(30),LinkAgendamento=String.Format("http://localhost:9009/agendar?lote=1234&data={0}",DateTime.Now.AddDays(30).ToString("yyyyMMdd"))};
                        DatasDisponiveisMessage data45 = new  DatasDisponiveisMessage(){Data=DateTime.Now.AddDays(45),LinkAgendamento=String.Format("http://localhost:9009/agendar?lote=1234&data={0}",DateTime.Now.AddDays(45).ToString("yyyyMMdd"))};
                        request.DatasDisponiveis = new List<DatasDisponiveisMessage>();
                        request.DatasDisponiveis.Add(data15);
                        request.DatasDisponiveis.Add(data30);
                        request.DatasDisponiveis.Add(data45);
                        
                        var listaProdutos = response.LoteProdutosVecidos.Where(f=>f.EmailFabricante==emailFabricante).ToList();

                        request.ListaProdutos = new List<DescartePendente>();

                        foreach(ProdutoMessage produto in listaProdutos)
                        {
                            DescartePendente descarte = new DescartePendente(){DataVencimento=produto.DataVencimento, IdItemEstoque=produto.IdItemEstoque, NomeProduto=produto.NomeProduto,QtdeprodutoDisponivel=produto.QtdeprodutoDisponivel};
                            request.ListaProdutos.Add(descarte);
                        }
                        //TODO: incluir logica no banco de dados dos lotes e agendamentos possíveis
                        request.EmailRemetente = emailFabricante;
                        request.NomeArquivo="DescarteProdutoVencido";
                        
                        string jobId = BackgroundJob.Enqueue<EmailService>(js => js.EnviarDescarteProdutoPendente(request));
                        
                        //colocar uma lista e jobs aqui com os emails...
                        response.codRetorno = 0;
                        response.StatusRetorno = String.Format("Podutos Vencidos enviados para a fila de notificação. Job Id: '{0}' Por favor, aguarde.", jobId);
                    }
                    return response;
                }
                else{
                    return response;
                }
            }
            return response;
        }

        public void SalvarLotesDescartePendentes(ObterProdutosVencidosMessageResponse produtosVencidos)
        {
            var repository = new LoteDescarteRepository(_context);

            if ((produtosVencidos != null) && (produtosVencidos.codRetorno!=1))
            {          
                var listaFabricantes = produtosVencidos.LoteProdutosVecidos.Select(x => x.EmailFabricante).Distinct().ToList();  
                
                foreach(string emailFabricante in listaFabricantes)
                {
                     var loteDescarte = new LoteDescarte(){EmailResponsavelDescarte=emailFabricante,NomeResponsavelDescarte=emailFabricante.Split('@')[0]};
                     
                     var listaProdutos = produtosVencidos.LoteProdutosVecidos.Where(f=>f.EmailFabricante==emailFabricante).ToList();
                     repository.Create(new LoteDescarte(){EmailResponsavelDescarte=emailFabricante,NomeResponsavelDescarte=emailFabricante.Split('@')[0]});
                     foreach(ProdutoMessage produto in listaProdutos)
                     {
                         var produtoDescarte = new ProdutoDescarte(){
                             LoteDescarte = loteDescarte,
                             DataVecimentoProduto = Convert.ToDateTime(produto.DataVencimento),
                             IdITemEstoque = produto.IdItemEstoque
                         };
                       
                     }

                }                
            }
        }      
    }
}