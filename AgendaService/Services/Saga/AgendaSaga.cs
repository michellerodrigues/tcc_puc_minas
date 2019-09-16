using System;
using System.Threading.Tasks;
using NServiceBus;
using Messages.Descartes.Commands;
using AgendaService.Data.SagaData;
using Messages.Descartes.Events;
using Hangfire;
using AgendaService.Services;
using AgendaService.Services.Interfaces;

namespace AgendaService.Saga
{
   
    public class AgendaSaga : Saga<AgendaSagaData>,
        IAmStartedByMessages<AgendarRetiradaCommand>,
        IHandleMessages<RetiradaAgendadaEvent>,
        IHandleMessages<ConfirmarAgendamentoRetiradaCommand>,
        IHandleMessages<AgendamentoRetiradaConfirmadoEvent>,
        IHandleMessages<CancelarAgendamentoRetiradaCommand>,
        IHandleMessages<CancelarAgendamentoRetiradaConfirmadaCommand>,
        IHandleMessages<AgendamentoRetiradaConfirmadaCanceladoEvent>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<AgendaSagaData> mapper)
        {
            mapper.ConfigureMapping<AgendarRetiradaCommand>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<RetiradaAgendadaEvent>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<ConfirmarAgendamentoRetiradaCommand>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<AgendamentoRetiradaConfirmadoEvent>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<CancelarAgendamentoRetiradaCommand>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<CancelarAgendamentoRetiradaConfirmadaCommand>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<AgendamentoRetiradaConfirmadaCanceladoEvent>(message => message.Id).ToSaga(saga => saga.AgendaId);   
        }

        private readonly IAgendaApiService _agendaApiService;
        public AgendaSaga(IAgendaApiService agendaApiService)
        {
            _agendaApiService = agendaApiService;
        }
        public Task Handle(AgendarRetiradaCommand message, IMessageHandlerContext context)
        {
            return context.Publish(new RetiradaAgendadaEvent(message.Id,message.EmailAgente));
        }

        public Task Handle(RetiradaAgendadaEvent message, IMessageHandlerContext context)
        {  
             return context.SendLocal(new ConfirmarAgendamentoRetiradaCommand()
            {SolicitadoEm=message.DataRegistro,EmailSolicitacao=message.EmailSolicitante,Id=message.Id});        
        }

        public Task Handle(ConfirmarAgendamentoRetiradaCommand message, IMessageHandlerContext context)
        {
            string mensagem= String.Format("Para CONFIRMAR o agendamento, clique no link abaixo</br> http://localhost:9009/api/agenda/confirmar/{0}/{1}. Para CANCELAR, clique:http://localhost:9009/api/agenda/cancelar/{2}/{3}",message.Id, message.EmailSolicitacao,message.Id, message.EmailSolicitacao);
            return Task.Factory.StartNew(async() => { await AgendaApiService.EnviarEmailAgendamento(message.EmailSolicitacao,"Favor Confirmar Agendamento",mensagem);}); 
        }

        public Task Handle(AgendamentoRetiradaConfirmadoEvent message, IMessageHandlerContext context)
        {
            //colocar handle pra Triagem
            string mensagem= String.Format("Agendamento Confirmado com sucesso. Seu descarte será encaminhado para a triagem",message.Id);
            Task.Factory.StartNew(async() => { await AgendaApiService.EnviarEmailAgendamento(message.EmailConfirmacao,"Agendamento Confirmado",mensagem);});     

            return context.Publish(new RealizarTriagemCommand()
            {DataEntrada=message.ConfirmadoEm,EmailAgente=message.EmailConfirmacao,Id=message.Id}); 
        }
        public Task Handle(CancelarAgendamentoRetiradaConfirmadaCommand message, IMessageHandlerContext context)
        {
            string mensagem= String.Format("Agendamento CANCELADO com sucesso.",message.Id);
            
            return Task.Factory.StartNew(async() => { await AgendaApiService.EnviarEmailAgendamento(message.EmailSolicitante,"Agendamento Cancelado",mensagem);}); 
        }

        public Task Handle(CancelarAgendamentoRetiradaCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public Task Handle(AgendamentoRetiradaConfirmadaCanceladoEvent message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }
    }
}