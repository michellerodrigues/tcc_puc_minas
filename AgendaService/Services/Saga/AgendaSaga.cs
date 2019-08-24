using System;
using System.Threading.Tasks;
using NServiceBus;
using Messages.Descartes.Commands;
using AgendaService.Data.SagaData;
using Messages.Descartes.Events;
using Hangfire;
using AgendaService.Services;

namespace AgendaService.Saga
{
    public class AgendaSaga : Saga<AgendaSagaData>,
        IAmStartedByMessages<AgendarRetiradaCommand>,
        IHandleMessages<AgendamentoRealizadoEvent>,
        IHandleMessages<ConfirmarAgendamentoCommand>,
        IHandleMessages<AgendamentoConfirmadoEvent>,
        IHandleMessages<CancelarAgendamentoRetiradaCommand>,
        IHandleMessages<AgendamentoRealizadoCanceladoEvent>,
        IHandleMessages<CancelarAgendamentoConfirmadoRetiradaCommand>,
        IHandleMessages<AgendamentoConfirmadoCanceladoEvent>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<AgendaSagaData> mapper)
        {
            mapper.ConfigureMapping<AgendarRetiradaCommand>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<AgendamentoRealizadoEvent>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<ConfirmarAgendamentoCommand>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<AgendamentoConfirmadoEvent>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<CancelarAgendamentoRetiradaCommand>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<AgendamentoRealizadoCanceladoEvent>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<CancelarAgendamentoConfirmadoRetiradaCommand>(message => message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<AgendamentoConfirmadoCanceladoEvent>(message => message.Id).ToSaga(saga => saga.AgendaId);   
        }

        public Task Handle(AgendarRetiradaCommand message, IMessageHandlerContext context)
        {
            return context.Publish(new AgendamentoRealizadoEvent(message.Id,message.EmailAgente));
        }

        public Task Handle(AgendamentoRealizadoEvent message, IMessageHandlerContext context)
        {  
            //aqui deverá ficar o código que envia um email com o link do proximo passo para o cliente, no caso, a confirmação;          
            return context.SendLocal(new ConfirmarAgendamentoCommand()
            {SolicitadoEm=message.DataRegistro,EmailSolicitacao=message.EmailSolicitante,Id=message.Id});        
            // context.Publish(new ConfirmarAgendamentoCommand(message.Id,message.DataRegistro,message.EmailSolicitante));
        }

        public Task Handle(ConfirmarAgendamentoCommand message, IMessageHandlerContext context)
        {
            //incluir link para cancelamento.
            string mensagem= String.Format("Para CONFIRMAR o agendamento, clique no link abaixo</br> http://localhost:9009/api/agenda/confirmar?Agenda={0}&email={1}. Para CANCELAR, clique:http://localhost:9009/api/agenda/cancelar?Agenda={2}&email={3}",message.Id, message.EmailSolicitacao,message.Id, message.EmailSolicitacao);
            return Task.Factory.StartNew(async() => { await AgendaApiService.EnviarEmailAgendamento(message.EmailSolicitacao,"Favor Confirmar Agendamento",mensagem);}); 
        }

        public Task Handle(AgendamentoConfirmadoEvent message, IMessageHandlerContext context)
        {
            //colocar handle pra Triagem
            string mensagem= String.Format("Agendamento Confirmado com sucesso. Seu descarte será encaminhado para a triagem",message.Id);
            Task.Factory.StartNew(async() => { await AgendaApiService.EnviarEmailAgendamento(message.EmailConfirmacao,"Agendamento Confirmado",mensagem);});     

            return context.SendLocal(new ConfirmarAgendamentoCommand()
            {SolicitadoEm=message.DataRegistro,EmailSolicitacao=message.EmailSolicitante,Id=message.Id}); 
        }
        public Task Handle(CancelarAgendamentoConfirmadoRetiradaCommand message, IMessageHandlerContext context)
        {
            string mensagem= String.Format("Agendamento CANCELADO com sucesso.",message.Id);
            
            return Task.Factory.StartNew(async() => { await AgendaApiService.EnviarEmailAgendamento(message.EmailConfirmacao,"Agendamento Cancelado",mensagem);}); 
        }

        public Task Handle(AgendamentoConfirmadoCanceladoEvent message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public Task Handle(CancelarAgendamentoRetiradaCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public Task Handle(AgendamentoRealizadoCanceladoEvent message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }


    }
}