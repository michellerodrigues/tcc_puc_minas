using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AgendaService.Data.Interfaces;
using AgendaService.Data.Models;
using AgendaService.DataContext;
using AgendaService.Service.Utils;
using AgendaService.Services.Interfaces;
using Messages.Descartes.Commands;
using NServiceBus;
using Messages.Descartes.Messages;
using System.Linq;

namespace AgendaService.Services
{
    public class AgendaApiService : IAgendaApiService
    {
   //     IMessageSession _messageSession;
        IAgendaRepository _agendaRepository;
        IResponsavelRepository _responsavelRepository;
 
        static string DescarteServicesURL = Startup.AppSettings.DescarteServicesURL;

      //  public AgendaApiService(IMessageSession messageSession, IAgendaRepository agendaRepository, IResponsavelRepository responsavelRepository)
        public AgendaApiService(IAgendaRepository agendaRepository, IResponsavelRepository responsavelRepository)      
        {
         //   this._messageSession = messageSession;
            _agendaRepository = agendaRepository;
            _responsavelRepository = responsavelRepository;
 
        }

        public AgendamentoMessage AgendarRetirada(Guid lote, string Data)
        {     
            AgendamentoMessage agendamento = VerificarAgendamentoSolicitado(lote, Data);
            
            DateTime registro=DateTime.Now;

            if(agendamento!=null && agendamento.codRetorno==0)
            { 
                //buscar o responsável pelo agendamento pelo email informado
                var responsavel = _responsavelRepository.FindResponsavelByEmail(agendamento.Email).ToList().FirstOrDefault();

                if(responsavel==null)
                {
                    agendamento.codRetorno=1;
                    agendamento.StatusRetorno="Email do Responsavel Inválido";
                    return agendamento;
                }

                //salvar no banco   
                //lote do descarte ou id do agendamento: agendamento deverá pertencer a uma só pessoa? 
                //Na realidade sim...mas podem ter outros posto que o agendamento não confirmado pode 
                //ser cancelado
                _agendaRepository.Create(new Agenda(){
                    DataAgenda=agendamento.DataRegistro,
                    DataExpiracao=registro.AddDays(7),
                    DataCriacao = registro,
                    DataStatus = registro,
                    StatusAgenda="Agendamento Requisitado",
                    LoteDescarte = lote.ToString(),
                    Verificada=true,
                    Responsavel = responsavel
                });

                agendamento.StatusRetorno="Agendamento recebido. Você receberá um e-mail para confirmação.";
            }
            else
            {
                agendamento.codRetorno=1;
                agendamento.StatusRetorno="Agendamento Não encontrado";
            }   
            return agendamento;
        }

        private AgendamentoMessage VerificarAgendamentoSolicitado(Guid lote, string data)
        {  
            ObterAgendamentoMessageResponse response = HttpRestClient.GetAsync<ObterAgendamentoMessageResponse>(string.Format("{0}/{1}?lote={2}&data={3}", DescarteServicesURL, (object)"agendamento/enviado",lote, data)).GetAwaiter().GetResult();
           
            AgendamentoMessage agendamento = null;
            
            if ((response != null) &&  (response.codRetorno!=1))
            {
                 agendamento = new AgendamentoMessage()
                 {
                    IdAgendamento=lote,
                    DataRegistro=DateTime.Now,   
                    Email=response.EmailResponsavel,
                    codRetorno=response.codRetorno           
                 };

            }
            else
            {
                 agendamento = new AgendamentoMessage()
                {
                    codRetorno=1,
                    StatusRetorno="Agendamento Não localizado"
                };
            }
            return agendamento;
        }

        public AgendaCanceladaMessageResponse CancelarAgenda(Guid idAgenda)
        {
         //   IAgendaRepository AgendaRepository = new AgendaRepository(_context);

            AgendaCanceladaMessageResponse response = new AgendaCanceladaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Agenda Cancelada Com Sucesso";
            response.AgendaCancelada = new AgendaMessage();

            var Agenda = _agendaRepository.GetById(idAgenda);

            if (Agenda == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Agenda Não encontrada";
                return response;
            }

            if (Agenda.StatusAgenda != "Pendente" && Agenda.StatusAgenda != "Confirmada")
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Agenda não pode ser cancelada. Veja seu Status";
                response.AgendaCancelada =  PrepararAgendaRetorno(Agenda);;
                return response;
            }

            var dataAgora = DateTime.Now;
            Agenda.DataStatus = dataAgora;
            Agenda.StatusAgenda = "Cancelada";

            _agendaRepository.Update(Agenda);
            response.AgendaCancelada =  PrepararAgendaRetorno(Agenda);
            return response;
        }


        public AgendaConfirmadaMessageResponse ConfirmarAgendamento(Guid lote, string email)
        {              
            var resposta = new AgendaConfirmadaMessageResponse();

            AgendaConfirmadaMessageResponse response = new AgendaConfirmadaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Agenda Confirmada Com Sucesso";
            response.AgendaConfirmada = new AgendaMessage();

            var Agenda = _agendaRepository.FindAgendaByLoteAgendamento(lote.ToString());

            if (Agenda == null || Agenda.Count()==0) 
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Agenda Não encontrada";
                return response;
            }

            var agenda = Agenda.LastOrDefault();


            if (agenda.StatusAgenda != "Agendamento Requisitado")
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Agenda não pode ser Confirmada";
                response.AgendaConfirmada = PrepararAgendaRetorno(agenda);
                return response;
            }


            var dataAgora = DateTime.Now;
            agenda.DataStatus = dataAgora;
            agenda.StatusAgenda = "Confirmada";

            _agendaRepository.Update(agenda);
            response.AgendaConfirmada = PrepararAgendaRetorno(agenda);                
            
            return response;
        }

        public AgendaFinalizadaMessageResponse FinalizarAgenda(Guid idAgenda)
        {
         //   IAgendaRepository AgendaRepository = new AgendaRepository(_context);

            AgendaFinalizadaMessageResponse response = new AgendaFinalizadaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Agenda Finalizada Com Sucesso";
            response.AgendaFinalizada = new AgendaMessage();

            var Agenda = _agendaRepository.GetById(idAgenda);

            if (Agenda == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Agenda Não encontrada";
                return response;
            }


            if (Agenda.StatusAgenda != "Agendamento Requisitado")
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Agenda não pode ser Finalizada. Favor Verificar Status";
                response.AgendaFinalizada = PrepararAgendaRetorno(Agenda);
                return response;
            }

            var dataAgora = DateTime.Now;
            Agenda.DataStatus = dataAgora;
            Agenda.StatusAgenda = "Finalizada";

            _agendaRepository.Update(Agenda);
            response.AgendaFinalizada = PrepararAgendaRetorno(Agenda);
            return response;
        }

        public ObterListaAgendaStatusMessageResponse ObterAgendasPorStatus(string status)
        {
          //  IAgendaRepository estoqueRepository = new AgendaRepository(_context);

            ObterListaAgendaStatusMessageResponse response = new ObterListaAgendaStatusMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = String.Format("Agendas {0}s Retornados com sucesso", status);

            var Agendas = _agendaRepository.FindAgendaStatus(status);

            if (Agendas == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = String.Format("Não existem Agendas com o status: {0}", status);
            }

            foreach (Agenda Agenda in Agendas)
            {
                response.ListaAgendaStatus.Add(PrepararAgendaRetorno(Agenda));
            }

            return response;
        }

        public ObterAgendaExpiradaMessageResponse ObterAgendaExpirada()
        {
            //IAgendaRepository estoqueRepository = new AgendaRepository(_context);

            ObterAgendaExpiradaMessageResponse response = new ObterAgendaExpiradaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Agendas expiradas Retornadas com sucesso";

            var Agendas = _agendaRepository.FindAgendaExpirada();

            if (Agendas == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Não existem Agendas expiradas";
            }

            foreach (Agenda Agenda in Agendas)
            {
                response.ListaAgendasExpiradas.Add(PrepararAgendaRetorno(Agenda));
            }

            return response;
        }

        private AgendaMessage PrepararAgendaRetorno(Agenda Agenda)
        {
            var AgendaMessage = new AgendaMessage()
            {
                DataStatus = Agenda.DataCriacao,
                EmailResponsavel = Agenda.Responsavel.Email,
                Responsavel = Agenda.Responsavel.NomeResponsavel,
                IdAgenda = Agenda.Id,
                LoteDescarte = Agenda.LoteDescarte,
                StatusAgenda = Agenda.StatusAgenda
            };

            return AgendaMessage;
        }


        public static Task EnviarEmailAgendamento(string remetente,string assunto, string mensagem)
        {
            try
            {
                var client = new SmtpClient(Startup.AppSettings.EnvioEmail.ServidorSMTP, Startup.AppSettings.EnvioEmail.PortaServidor)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Startup.AppSettings.EnvioEmail.UsuarioEmail, Startup.AppSettings.EnvioEmail.SenhaEmail),
                    EnableSsl = true
                };

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(Startup.AppSettings.EnvioEmail.UsuarioEmail);
                mail.To.Add(remetente);
                mail.Subject = String.Format("AgroPop informa: {0}",assunto);

                mail.Body = mensagem;

                mail.IsBodyHtml = true;
                client.Send(mail);
                
                return Task.CompletedTask;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Exception in sendEmail:" + ex.Message);
            }
        }
    }
}