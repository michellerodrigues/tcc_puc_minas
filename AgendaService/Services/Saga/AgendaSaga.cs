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
        AgendaApiService agendaService;
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

        public AgendaSaga(AgendaApiService agendaService)
        {
            this.agendaService = agendaService;
        }

        public Task Handle(AgendarRetiradaCommand message, IMessageHandlerContext context)
        {
            return context.Publish(new AgendamentoRealizadoEvent(message.Id,message.EmailAgente));
        }

        public Task Handle(AgendamentoRealizadoEvent message, IMessageHandlerContext context)
        {
            return context.Publish(new ConfirmarAgendamentoCommand(message.Id,message.DataRegistro,message.EmailSolicitante));
        }

        public Task Handle(ConfirmarAgendamentoCommand message, IMessageHandlerContext context)
        {
            //incluir link para cancelamento.
            string mensagem= String.Format("Para confirmar o agendamento, clique no link abaixo</br> http://linkAplicacaoAgenda/agenda/confirmar?Agenda={0}&email={1},",message.Id, message.EmailSolicitacao);
            return Task.Factory.StartNew(async() => { await AgendaApiService.EnviarEmailAgendamento(message.EmailSolicitacao,"Favor Confirmar Agendamento",mensagem);}); 
        }

        public Task Handle(AgendamentoConfirmadoEvent message, IMessageHandlerContext context)
        {
            //colocar hangle pra Triagem
            string mensagem= String.Format("Agendamento Confirmado com sucesso. Seu descarte será encaminhado para a triagem",message.Id);
            return Task.Factory.StartNew(async() => { await AgendaApiService.EnviarEmailAgendamento(message.EmailConfirmacao,"Agendamento Confirmado",mensagem);}); 
        }
        public Task Handle(CancelarAgendamentoConfirmadoRetiradaCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
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