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
    public class DescarteApiService
    {   
        static string EstoqueServicesURL = Startup.AppSettings.EstoqueServicesURL;
        public ObterProdutosVencidosMessageResponse ObterProdutosVencidos()
        {
            ObterProdutosVencidosMessageResponse response = HttpRestClient.GetAsync<ObterProdutosVencidosMessageResponse>(string.Format("{0}/{1}", EstoqueServicesURL, (object)"vencidos")).GetAwaiter().GetResult();
            if (response != null)
            {
             
                var produtosOrdenadosPorFabricante = response.LoteProdutosVecidos.OrderBy(p=>p.EmailFabricante).ToList();
                if(response.codRetorno!=1)
                {

                    var listaFabricantes = response.LoteProdutosVecidos.Select(x => x.EmailFabricante).Distinct().ToList();
                    
                    foreach(string emailFabricante in listaFabricantes)
                    {
                        ComunicarDescartePendenteMessageRequest request = new ComunicarDescartePendenteMessageRequest();
                        DatasDisponiveisMessage data15 = new  DatasDisponiveisMessage(){Data=DateTime.Now.AddDays(15),LinkAgendamento="http://localhost:1515/agendar/lote1234&data15Dias"};
                        DatasDisponiveisMessage data30 = new  DatasDisponiveisMessage(){Data=DateTime.Now.AddDays(30),LinkAgendamento="http://localhost:1515/agendar/lote1234&data30Dias"};
                        DatasDisponiveisMessage data45 = new  DatasDisponiveisMessage(){Data=DateTime.Now.AddDays(45),LinkAgendamento="http://localhost:1515/agendar/lote1234&data45Dias"}
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

                        request.EmailRemetente = emailFabricante;
                        request.NomeArquivo="DescarteProdutoVencido";
                        request.NomeResponsavel=listaProdutos.First().NomeResponsavel;

                        string jobId = BackgroundJob.Enqueue<EmailService>(js => js.EnviarDescarteProdutoPendente(request));
                        
                        response.StatusCode = 200;
                        response.StatusMessage = "OK";
                        response.Message = string.Format("Email Nome: '{0}' enviado para a fila de gravação no disco. Job Id: '{1}'Por favor, aguarde.", assuntoEmail, jobId);
                        
                    }

                }
                else{
                    return response;
                }
            }
        }      
    }
}