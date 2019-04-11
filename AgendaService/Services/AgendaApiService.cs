using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AgendaService.Data.Interfaces;
using AgendaService.Data.Models;
using AgendaService.DataContext;
using AgendaService.Services.Interfaces;
using AgendaService.Services.Messages;
using Messages.Descartes.Commands;
using Messages.Descartes.Events;
using NServiceBus;

namespace AgendaService.Services
{
    public class AgendaApiService : IAgendaApiService
    {
        IMessageSession _messageSession;

        AppDataContext _context;
 
        public AgendaApiService(IMessageSession messageSession, AppDataContext context)
        {
            this._messageSession = messageSession;
            this._context = context;
        }

        public AgendaApiService()
        {
        }

        public async Task AgendarRetirada(AgendamentoMessage agendamento)
        {              
            await _messageSession.SendLocal(new AgendarRetiradaCommand()
            {DataAgendamento=DateTime.Now,DataRegistro=agendamento.DataRegistro,EmailAgente=agendamento.Email,Id=agendamento.IdAgendamento});      
        }

        public AgendaCanceladaMessageResponse CancelarAgenda(Guid idAgenda)
        {
            IAgendaRepository AgendaRepository = new AgendaRepository(_context);

            AgendaCanceladaMessageResponse response = new AgendaCanceladaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Agenda Cancelada Com Sucesso";
            response.AgendaCancelada = new AgendaMessage();

            var Agenda = AgendaRepository.GetById(idAgenda);

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

            AgendaRepository.Update(Agenda);
            response.AgendaCancelada =  PrepararAgendaRetorno(Agenda);
            return response;
        }


        public AgendaConfirmadaMessageResponse ConfirmarAgenda(Guid idAgenda)
        {              
            var resposta = new AgendaConfirmadaMessageResponse();
            
            //_context.SendLocal(new AgendamentoConfirmadoEvent(){ConfirmadoEm=DateTime.Now,Id=idAgenda, EmailConfirmacao="meuemail"});
                   
            IAgendaRepository AgendaRepository = new AgendaRepository(_context);

            AgendaConfirmadaMessageResponse response = new AgendaConfirmadaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Agenda Confirmada Com Sucesso";
            response.AgendaConfirmada = new AgendaMessage();

            var Agenda = AgendaRepository.GetById(idAgenda);

            if (Agenda == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Agenda Não encontrada";
                return response;
            }


            if (Agenda.StatusAgenda != "Pendente")
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Agenda não pode ser confirmada. Veja seu Status";
                response.AgendaConfirmada = PrepararAgendaRetorno(Agenda);
                return response;
            }


            var dataAgora = DateTime.Now;
            Agenda.DataStatus = dataAgora;
            Agenda.StatusAgenda = "Cancelada";

            AgendaRepository.Update(Agenda);
            response.AgendaConfirmada = PrepararAgendaRetorno(Agenda);
            return response;
        }

        public AgendaFinalizadaMessageResponse FinalizarAgenda(Guid idAgenda)
        {
            IAgendaRepository AgendaRepository = new AgendaRepository(_context);

            AgendaFinalizadaMessageResponse response = new AgendaFinalizadaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Agenda Finalizada Com Sucesso";
            response.AgendaFinalizada = new AgendaMessage();

            var Agenda = AgendaRepository.GetById(idAgenda);

            if (Agenda == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Agenda Não encontrada";
                return response;
            }


            if (Agenda.StatusAgenda != "Confirmada")
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Agenda não pode ser finalizada. Veja seu Status";
                response.AgendaFinalizada = PrepararAgendaRetorno(Agenda);
                return response;
            }

            var dataAgora = DateTime.Now;
            Agenda.DataStatus = dataAgora;
            Agenda.StatusAgenda = "Finalizada";

            AgendaRepository.Update(Agenda);
            response.AgendaFinalizada = PrepararAgendaRetorno(Agenda);
            return response;
        }

        public ObterListaAgendaStatusMessageResponse ObterAgendasPorStatus(string status)
        {
            IAgendaRepository estoqueRepository = new AgendaRepository(_context);

            ObterListaAgendaStatusMessageResponse response = new ObterListaAgendaStatusMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = String.Format("Agendas {0}s Retornados com sucesso", status);

            var Agendas = estoqueRepository.FindAgendaStatus(status);

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
            IAgendaRepository estoqueRepository = new AgendaRepository(_context);

            ObterAgendaExpiradaMessageResponse response = new ObterAgendaExpiradaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Agendas expiradas Retornadas com sucesso";

            var Agendas = estoqueRepository.FindAgendaExpirada();

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
                DataCriacao = Agenda.DataCriacao,
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