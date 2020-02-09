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
            mapper.ConfigureMapping<AgendarRetiradaCommand>(message =>message.Id).ToSaga(saga => saga.AgendaId);
            //mapper.ConfigureMapping<AgendarRetiradaCommand>(message =>message.Tipo).ToSaga(saga => saga.Tipo);
            mapper.ConfigureMapping<RetiradaAgendadaEvent>(message=>message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<ConfirmarAgendamentoRetiradaCommand>(message=>message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<AgendamentoRetiradaConfirmadoEvent>(message=>message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<CancelarAgendamentoRetiradaCommand>(message=>message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<CancelarAgendamentoRetiradaConfirmadaCommand>(message=>message.Id).ToSaga(saga => saga.AgendaId);
            mapper.ConfigureMapping<AgendamentoRetiradaConfirmadaCanceladoEvent>(message=>message.Id).ToSaga(saga => saga.AgendaId);   
        }
        IAgendaApiService _agendaApiService;
        public AgendaSaga(IAgendaApiService agendaApiService)
        {
            _agendaApiService = agendaApiService;
        }
        public Task Handle(AgendarRetiradaCommand message, IMessageHandlerContext context)
        {
            var retorno = _agendaApiService.AgendarRetirada(message.Id, message.DataAgendamento);
            
            if(retorno.codRetorno==0)
            {
                //message.Tipo = typeof(AgendarRetiradaCommand).ToString();
                return context.Publish(new RetiradaAgendadaEvent(message.Id,retorno.Email));
            }
            else
            {
                message.Status=retorno.StatusRetorno;
                if(!String.IsNullOrEmpty(retorno.Email))
                {
                    string mensagem = String.Format("O agendamento solicitado não é válido. Motivo:{0}",message.Status);
                    return Task.Factory.StartNew( async() => { await AgendaApiService.EnviarEmailAgendamento(retorno.Email,"Solicitação Inválida",mensagem);}); 

                }
                MarkAsComplete();
                return Task.CompletedTask;  
            }
         
        }

        public Task Handle(RetiradaAgendadaEvent message, IMessageHandlerContext context)
        {  
            string mensagem= String.Format("Para CONFIRMAR o agendamento, clique no link abaixo</br> http://localhost:9009/api/agenda/confirmar/{0}/{1}. Para CANCELAR, clique:http://localhost:9009/api/agenda/cancelar/{2}/{3}",message.Id, message.DataRegistro,message.Id, message.DataRegistro);
            return Task.Factory.StartNew(async() => { await AgendaApiService.EnviarEmailAgendamento(message.EmailSolicitante,"Favor Confirmar Agendamento",mensagem);});       
        }

        public Task Handle(ConfirmarAgendamentoRetiradaCommand message, IMessageHandlerContext context)
        {
            var retorno = _agendaApiService.ConfirmarAgendamento(message.Id, message.EmailSolicitacao);
            
            if(retorno.codRetorno==0)
            {
                return context.Publish(new AgendamentoRetiradaConfirmadoEvent(message.Id,message.EmailSolicitacao));
            }
            MarkAsComplete();
            return Task.CompletedTask; 
        }

        public Task Handle(AgendamentoRetiradaConfirmadoEvent message, IMessageHandlerContext context)
        {
            //colocar handle pra Triagem
            string mensagem= String.Format("Agendamento Confirmado com sucesso. Seu descarte será encaminhado para a triagem",message.Id);
            Task.Factory.StartNew(async() => { await AgendaApiService.EnviarEmailAgendamento(message.EmailConfirmacao,"Agendamento Confirmado",mensagem);});     

            //a triagem deveá se conectar ao agendamento e escutar o evento de 'AgendamentoRetiradaConfirmadoEvent'
            return context.Send(new RealizarTriagemCommand()
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