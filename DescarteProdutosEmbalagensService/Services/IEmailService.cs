using System;
using DescarteService.Services.Messages;

namespace DescarteService.Services
{
    public interface IEmailService
    {
        void EnviarEmailDescartePendente(string emailRemetente, string body, string nomeAnexo, string assunto);
        void EnviarDescarteProdutoPendente(ComunicarDescartePendenteMessageRequest request, Guid IdLoteAgendamento);
    }
}