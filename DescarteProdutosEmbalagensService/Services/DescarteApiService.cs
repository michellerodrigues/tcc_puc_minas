using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DescarteService.Data.Interfaces;
using DescarteService.Data.Models;
using DescarteService.DataContext;
using DescarteService.Service.Utils;
using Messages.Descartes.Messages;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Messages.Services.Messages;
using System.Runtime.CompilerServices;

namespace DescarteService.Services
{
    public class DescarteApiService : IDescarteApiService
    {
        private readonly IAgendamentoDescarteRepository _agendamentoDescarteRepository;
        private readonly ILoteDescarteRepository _loteDescarteRepository;
        
        public DescarteApiService(IAgendamentoDescarteRepository agendamentoDescarteRepository,ILoteDescarteRepository loteDescarteRepository)
        {
            _agendamentoDescarteRepository = agendamentoDescarteRepository;
            _loteDescarteRepository = loteDescarteRepository;
        }

        static string EstoqueServicesURL = Startup.AppSettings.EstoqueServicesURL;
        
        public ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados()
        {

            ObterProdutosFinalizadosMessageResponse response = HttpRestClient.GetAsync<ObterProdutosFinalizadosMessageResponse>(string.Format("{0}/{1}", EstoqueServicesURL, (object)"finalizados")).GetAwaiter().GetResult();
            if ((response != null) && (response.codRetorno != 1))
            {
                SalvarLotesDescartePendentesFinalizados(response);
                var jobid = BackgroundJob.Enqueue<DescarteApiService>(js => js.ComunicarLotesParaRetirada("DescarteProdutoFinalizado"));

                //colocar uma lista e jobs aqui com os emails...
                response.codRetorno = 0;
                response.StatusRetorno = String.Format("Podutos Finalizados enviados para a fila de notificação. Job: {0}. Por favor, aguarde.", jobid);

            }
              if(response==null)
            {
                response = new ObterProdutosFinalizadosMessageResponse()
                {
                    codRetorno = -1,
                    StatusRetorno = "Não foi possível conectar com o serviço de Estoque. Aguarde a próxima execução"
                };                
            }
            return response;
        }
        public ObterProdutosVencidosMessageResponse ObterProdutosVencidos()
        {
            ObterProdutosVencidosMessageResponse response = HttpRestClient.GetAsync<ObterProdutosVencidosMessageResponse>(string.Format("{0}/{1}", EstoqueServicesURL, (object)"vencidos")).GetAwaiter().GetResult();

            if ((response != null) && (response.codRetorno != 1))
            {
                SalvarLotesDescartePendentes(response);
                var jobid = BackgroundJob.Enqueue<DescarteApiService>(js => js.ComunicarLotesParaRetirada("DescarteProdutoVencido"));

                //colocar uma lista e jobs aqui com os emails...
                response.codRetorno = 0;
                response.StatusRetorno = String.Format("Podutos Vencidos enviados para a fila de notificação. Job: {0}. Por favor, aguarde.", jobid);

            }
            if(response==null)
            {
                response = new ObterProdutosVencidosMessageResponse()
                {
                    codRetorno = -1,
                    StatusRetorno = "Não foi possível conectar com o serviço de Estoque. Aguarde a próxima execução"
                };                
            }
            return response;
        }

        public ObterAgendamentoMessageResponse ObterAgendamentoEnviado(Guid lote, string data)
        {
            var response = new ObterAgendamentoMessageResponse()
            {
                codRetorno = 0,
                StatusRetorno = "ok"
            };

            var agendamentoEnviado = _agendamentoDescarteRepository.FindAgendamentoEnviado(lote, data);

            if (agendamentoEnviado != null)
            {
                response.DataEnvioEmail = agendamentoEnviado.DataEnvioEmail;
                response.DataProposta = agendamentoEnviado.DataPropostaAgendamento;
                response.EmailResponsavel = agendamentoEnviado.LoteDescarte.EmailResponsavelDescarte;
                response.Lote = agendamentoEnviado.LoteDescarte.Id;
                response.NomeResponsavel = agendamentoEnviado.LoteDescarte.NomeResponsavelDescarte;
                response.StatusProposta = agendamentoEnviado.StatusProposta;

                return response;

            }
            else
            {
                response.codRetorno = -1;
                response.StatusRetorno = "Agendamento não encontrado";

                return response;
            }
        }

        private void SalvarLotesDescartePendentes(BaseResponseMessage descarteResponse)
        {
            var produtosVencidos = descarteResponse as ObterProdutosVencidosMessageResponse;

            if ((produtosVencidos != null) && (produtosVencidos.codRetorno != 1))
            {
                var listaFabricantes = produtosVencidos.LoteProdutosVecidos.Select(x => x.EmailFabricante).Distinct().ToList();

                foreach (string emailFabricante in listaFabricantes)
                {
                    var loteDescarte = new LoteDescarte() { EmailResponsavelDescarte = emailFabricante, NomeResponsavelDescarte = emailFabricante.Split('@')[0] };

                    var listaProdutos = produtosVencidos.LoteProdutosVecidos.Where(f => f.EmailFabricante == emailFabricante).ToList();

                    foreach (ProdutoMessage produto in listaProdutos)
                    {
                        var produtoDescarte = new ProdutoDescarte()
                        {
                            Nome = produto.NomeProduto,
                            LoteDescarte = loteDescarte,
                            DataVecimentoProduto = Convert.ToDateTime(produto.DataVencimento),
                            IdITemEstoque = produto.IdItemEstoque,
                            QtdeDispUnidade = Convert.ToDecimal(produto.QtdeprodutoDisponivel)
                        };
                        loteDescarte.ProdutosDescartes.Add(produtoDescarte);
                    }

                    produtosVencidos.DatasOfertadas = new List<string>(){
                    DateTime.Now.AddDays(15).ToString("yyyyMMdd"),
                    DateTime.Now.AddDays(30).ToString("yyyyMMdd"),
                    DateTime.Now.AddDays(45).ToString("yyyyMMdd")
                    };
                    foreach (string data in produtosVencidos.DatasOfertadas)
                    {
                        var agendamento = new AgendamentoDescarteSolicitado()
                        {
                            DataPropostaAgendamento = data,
                            Id = Guid.NewGuid(),
                            LoteDescarte = loteDescarte,
                            StatusProposta = "Pendente Envio Email"
                        };
                        _agendamentoDescarteRepository.Create(agendamento);
                    }

                }
            }
        }

        private void SalvarLotesDescartePendentesFinalizados(BaseResponseMessage descarteResponse)
        {
            var produtosFinalizados = descarteResponse as ObterProdutosFinalizadosMessageResponse;

            if ((produtosFinalizados != null) && (produtosFinalizados.codRetorno != 1))
            {
                var listaRevendedores = produtosFinalizados.LoteProdutosFinalizados.Select(x => x.EmailRevendedor).Distinct().ToList();

                foreach (string emailRevendedor in listaRevendedores)
                {
                    var loteDescarte = new LoteDescarte() { EmailResponsavelDescarte = emailRevendedor, NomeResponsavelDescarte = emailRevendedor.Split('@')[0] };

                    var listaProdutos = produtosFinalizados.LoteProdutosFinalizados.Where(f => f.EmailRevendedor == emailRevendedor).ToList();

                    foreach (ProdutoMessage produto in listaProdutos)
                    {
                        var produtoDescarte = new ProdutoDescarte()
                        {
                            Nome = produto.NomeProduto,
                            LoteDescarte = loteDescarte,
                            DataVecimentoProduto = Convert.ToDateTime(produto.DataVencimento),
                            IdITemEstoque = produto.IdItemEstoque,
                            QtdeDispUnidade = Convert.ToDecimal(produto.QtdeprodutoDisponivel)
                        };
                        loteDescarte.ProdutosDescartes.Add(produtoDescarte);
                    }

                    produtosFinalizados.DatasOfertadas = new List<string>(){
                        DateTime.Now.AddDays(15).ToString("yyyyMMdd"),
                        DateTime.Now.AddDays(30).ToString("yyyyMMdd"),
                        DateTime.Now.AddDays(45).ToString("yyyyMMdd")
                    };

                    foreach (string data in produtosFinalizados.DatasOfertadas)
                    {
                        var agendamento = new AgendamentoDescarteSolicitado()
                        {
                            DataPropostaAgendamento = data,
                            Id = Guid.NewGuid(),
                            LoteDescarte = loteDescarte,
                            StatusProposta = "Pendente Envio Email"
                        };
                        _agendamentoDescarteRepository.Create(agendamento);
                    }
                }
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ComunicarLotesParaRetirada(string tipoEmail)
        {
            lock(typeof(EmailService))
            {  
                var agendamentos = _agendamentoDescarteRepository.FindAgendamentoPendenteEnvioEmail();

                var lotes = agendamentos.GroupBy(l => l.LoteDescarte);

                foreach (var lote in lotes)
                {
                    var agendamentosPorLote = agendamentos.Where(a => a.LoteDescarteId == lote.Key.Id).OrderBy(a => a.DataPropostaAgendamento).ToList();

                    ComunicarDescartePendenteMessageRequest request = new ComunicarDescartePendenteMessageRequest();
                    request.ListaProdutos = new List<DescartePendente>();
                    request.DatasDisponiveis = new List<DatasDisponiveisMessage>();

                    foreach (AgendamentoDescarteSolicitado agendamento in agendamentosPorLote)
                    {
                        DatasDisponiveisMessage data = new DatasDisponiveisMessage() { Data = DateTime.Now, LinkAgendamento = String.Format("{0}/agendar?lote={1}&data={2}", Startup.AppSettings.AgendaServicesURL, agendamento.Id, agendamento.DataPropostaAgendamento) };
                        request.DatasDisponiveis.Add(data);

                        var produtos = agendamento.LoteDescarte.ProdutosDescartes.ToList();

                        foreach (ProdutoDescarte produto in produtos)
                        {
                            DescartePendente descarte = new DescartePendente() { DataVencimento = produto.DataVecimentoProduto.ToShortDateString(), IdItemEstoque = produto.IdITemEstoque, NomeProduto = produto.Nome, QtdeprodutoDisponivel=produto.QtdeDispUnidade.ToString() };
                            request.ListaProdutos.Add(descarte);
                        }
                        request.EmailRemetente = agendamento.LoteDescarte.EmailResponsavelDescarte;
                        request.NomeResponsavel = agendamento.LoteDescarte.NomeResponsavelDescarte;
                    }

                    request.NomeArquivo = tipoEmail;
                    BackgroundJob.Enqueue<EmailService>(js => js.EnviarDescarteProdutoPendente(request, lote.Key.Id));
                }
            }            
        }


        public ObterAgendamentoPendenteMessageResponse ObterAgendamentoPendente()
        {

            var response = new ObterAgendamentoPendenteMessageResponse()
            {
                codRetorno = 0,
                StatusRetorno = "ok",
                listaPendencias = new List<ObterAgendamentoMessageResponse>()
            };

            var agendamentoPendentes = _agendamentoDescarteRepository.FindAgendamentoPendenteEnvioEmail();

            foreach (AgendamentoDescarteSolicitado agendamentoPendente in agendamentoPendentes)
            {
                ObterAgendamentoMessageResponse agendamento = new ObterAgendamentoMessageResponse()
                {
                    DataProposta = agendamentoPendente.DataPropostaAgendamento,
                    EmailResponsavel = agendamentoPendente.LoteDescarte.EmailResponsavelDescarte,
                    Lote = agendamentoPendente.LoteDescarte.Id,
                    NomeResponsavel = agendamentoPendente.LoteDescarte.NomeResponsavelDescarte,
                    StatusProposta = agendamentoPendente.StatusProposta
                };

                response.listaPendencias.Add(agendamento);
            }
            return response;
        }
        
    }
}
