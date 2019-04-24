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
        private readonly AppDataContext _context;
 
        public DescarteApiService( AppDataContext context)
        {
            this._context = context;
        }

        static string EstoqueServicesURL = Startup.AppSettings.EstoqueServicesURL;
        public ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados()
        {
           // var _factory = new DesignTimeDbContextFactory();

           var repository = new AgendamentoDescarteRepository(_context);
           var loteRepository = new LoteDescarteRepository(_context);

            ObterProdutosFinalizadosMessageResponse response = HttpRestClient.GetAsync<ObterProdutosFinalizadosMessageResponse>(string.Format("{0}/{1}", EstoqueServicesURL, (object)"finalizados")).GetAwaiter().GetResult();
            if ((response != null) &&  (response.codRetorno!=1))
            {            
                SalvarLotesDescartePendentesFinalizados(response);
                var jobid = BackgroundJob.Enqueue<DescarteApiService>(js => js.ComunicarLotesParaRetirada("DescarteProdutoFinalizado"));

                //colocar uma lista e jobs aqui com os emails...
                response.codRetorno = 0;
                response.StatusRetorno = String.Format("Podutos Finalizados enviados para a fila de notificação. Job: {0}. Por favor, aguarde.",jobid);

            }
            return response;
        }
        public ObterProdutosVencidosMessageResponse ObterProdutosVencidos()
        {
           // var _factory = new DesignTimeDbContextFactory();

           var repository = new AgendamentoDescarteRepository(_context);
           var loteRepository = new LoteDescarteRepository(_context);

            ObterProdutosVencidosMessageResponse response = HttpRestClient.GetAsync<ObterProdutosVencidosMessageResponse>(string.Format("{0}/{1}", EstoqueServicesURL, (object)"vencidos")).GetAwaiter().GetResult();
            if ((response != null) &&  (response.codRetorno!=1))
            {            
                SalvarLotesDescartePendentes(response);
                var jobid = BackgroundJob.Enqueue<DescarteApiService>(js => js.ComunicarLotesParaRetirada("DescarteProdutoVencido"));

                //colocar uma lista e jobs aqui com os emails...
                response.codRetorno = 0;
                response.StatusRetorno = String.Format("Podutos Vencidos enviados para a fila de notificação. Job: {0}. Por favor, aguarde.",jobid);

            }
            return response;
        }
        
        public void SalvarLotesDescartePendentes(BaseResponseMessage descarteResponse)
        {
            var repository = new LoteDescarteRepository(_context);
            var repositoryAgendamento = new AgendamentoDescarteRepository(_context);

            var produtosVencidos = descarteResponse as ObterProdutosVencidosMessageResponse;

            if ((produtosVencidos != null) && (produtosVencidos.codRetorno!=1))
            {          
                var listaFabricantes = produtosVencidos.LoteProdutosVecidos.Select(x => x.EmailFabricante).Distinct().ToList();  
                
                foreach(string emailFabricante in listaFabricantes)
                {
                     var loteDescarte = new LoteDescarte(){EmailResponsavelDescarte=emailFabricante,NomeResponsavelDescarte=emailFabricante.Split('@')[0]};
                     
                     var listaProdutos = produtosVencidos.LoteProdutosVecidos.Where(f=>f.EmailFabricante==emailFabricante).ToList();
                   
                     foreach(ProdutoMessage produto in listaProdutos)
                     {
                         var produtoDescarte = new ProdutoDescarte(){
                             Nome = produto.NomeProduto,
                             LoteDescarte = loteDescarte,
                             DataVecimentoProduto = Convert.ToDateTime(produto.DataVencimento),
                             IdITemEstoque = produto.IdItemEstoque,
                             QtdeDispUnidade = Convert.ToDecimal(produto.QtdeprodutoDisponivel)
                         };
                         loteDescarte.ProdutosDescartes.Add(produtoDescarte);                       
                     }

                    var agendamento = new ComunicadosDeAgendamentoEnviados()
                    {
                            DataEnvioEmail = DateTime.Now,
                            DataPropostaAgendamento = DateTime.Now.AddDays(15),
                            Id = Guid.NewGuid(),
                            LoteDescarte = loteDescarte,
                            StatusProposta = "Pendente Envio Email"
                    };

                    repositoryAgendamento.Create(agendamento);
                }                
            }
        }      

        public void SalvarLotesDescartePendentesFinalizados(BaseResponseMessage descarteResponse)
        {
            var repository = new LoteDescarteRepository(_context);
            var repositoryAgendamento = new AgendamentoDescarteRepository(_context);

            var produtosFinalizados = descarteResponse as ObterProdutosFinalizadosMessageResponse;

            if ((produtosFinalizados != null) && (produtosFinalizados.codRetorno!=1))
            {          
                var listaRevendedores = produtosFinalizados.LoteProdutosFinalizados.Select(x => x.EmailRevendedor).Distinct().ToList();  
                
                foreach(string emailRevendedor in listaRevendedores)
                {
                     var loteDescarte = new LoteDescarte(){EmailResponsavelDescarte=emailRevendedor,NomeResponsavelDescarte=emailRevendedor.Split('@')[0]};
                     
                     var listaProdutos = produtosFinalizados.LoteProdutosFinalizados.Where(f=>f.EmailRevendedor==emailRevendedor).ToList();
                   
                     foreach(ProdutoMessage produto in listaProdutos)
                     {
                         var produtoDescarte = new ProdutoDescarte(){
                             Nome = produto.NomeProduto,
                             LoteDescarte = loteDescarte,
                             DataVecimentoProduto = Convert.ToDateTime(produto.DataVencimento),
                             IdITemEstoque = produto.IdItemEstoque,
                              QtdeDispUnidade = Convert.ToDecimal(produto.QtdeprodutoDisponivel)
                         };
                         loteDescarte.ProdutosDescartes.Add(produtoDescarte);                       
                     }

                    var agendamento = new ComunicadosDeAgendamentoEnviados()
                    {
                            DataEnvioEmail = DateTime.Now,
                            DataPropostaAgendamento = DateTime.Now.AddDays(15),
                            Id = Guid.NewGuid(),
                            LoteDescarte = loteDescarte,
                            StatusProposta = "Pendente Envio Email"
                    };

                    repositoryAgendamento.Create(agendamento);
                }                
            }
        }      
    
        public void ComunicarLotesParaRetirada(string tipoEmail)
        {
              var repository = new AgendamentoDescarteRepository(_context);
              var repositoryLote = new LoteDescarteRepository(_context);

              var listaEmailParaEnviar = repository.FindAgendamentoPendenteEnvioEmail();

              foreach(ComunicadosDeAgendamentoEnviados agendamento in listaEmailParaEnviar)
              {
                    var loteDescarte = agendamento.LoteDescarte;

                    var produtos = loteDescarte.ProdutosDescartes;

                    ComunicarDescartePendenteMessageRequest request = new ComunicarDescartePendenteMessageRequest();
                    DatasDisponiveisMessage data15 = new  DatasDisponiveisMessage(){Data=DateTime.Now.AddDays(15),LinkAgendamento=String.Format("http://localhost:9009/agendar?lote={0}&data={1}",loteDescarte.LoteDescarteId, DateTime.Now.AddDays(15).ToString("yyyyMMdd"))};
                    DatasDisponiveisMessage data30 = new  DatasDisponiveisMessage(){Data=DateTime.Now.AddDays(30),LinkAgendamento=String.Format("http://localhost:9009/agendar?lote={0}&data={1}",loteDescarte.LoteDescarteId, DateTime.Now.AddDays(30).ToString("yyyyMMdd"))};
                    DatasDisponiveisMessage data45 = new  DatasDisponiveisMessage(){Data=DateTime.Now.AddDays(45),LinkAgendamento=String.Format("http://localhost:9009/agendar?lote={0}&data={1}",loteDescarte.LoteDescarteId, DateTime.Now.AddDays(45).ToString("yyyyMMdd"))};
                    request.DatasDisponiveis = new List<DatasDisponiveisMessage>();
                    request.DatasDisponiveis.Add(data15);
                    request.DatasDisponiveis.Add(data30);
                    request.DatasDisponiveis.Add(data45);


                    foreach(ProdutoDescarte produto in produtos)
                    {
                        DescartePendente descarte = new DescartePendente(){DataVencimento=produto.DataVecimentoProduto.ToShortDateString(), IdItemEstoque=produto.IdITemEstoque, NomeProduto=produto.Nome};
                        request.ListaProdutos.Add(descarte);
                    }

                    request.EmailRemetente = agendamento.LoteDescarte.EmailResponsavelDescarte;
                    request.NomeArquivo=tipoEmail;
                   
                   BackgroundJob.Enqueue<EmailService>(js => js.EnviarDescarteProdutoPendente(request));         
              }
        }
    }
}