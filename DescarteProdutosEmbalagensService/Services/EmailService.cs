using DescarteService.Data.Models;
using DescarteService.DataContext;
using Hangfire;
using Messages.Services.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace DescarteService.Services
{
    public class EmailService:IEmailService
    {
        private readonly AppDataContext _context;

        public EmailService(AppDataContext context)
        {
            this._context = context;
        }


        public void EnviarEmailDescartePendente(string emailRemetente, string body, string nomeAnexo, string assunto)
        {            
            var client = new SmtpClient(Startup.AppSettings.EnvioEmail.ServidorSMTP, Startup.AppSettings.EnvioEmail.PortaServidor)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Startup.AppSettings.EnvioEmail.UsuarioEmail, Startup.AppSettings.EnvioEmail.SenhaEmail),
                EnableSsl = true
            };

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(Startup.AppSettings.EnvioEmail.UsuarioEmail);
            mail.To.Add(emailRemetente);
            mail.Subject = assunto;
            mail.Body = body;
            mail.Attachments.Add(new Attachment(@nomeAnexo));
            mail.IsBodyHtml = true;
            client.Send(mail); 
        }


        public void EnviarDescarteProdutoPendente(ComunicarDescartePendenteMessageRequest request, Guid IdLoteAgendamento )
        {
            try
            {
                var body="";
                if (request.NomeArquivo == "DescarteProdutoVencido")
                {
                    body = PrepararMensagemCorpoEmail(request.DatasDisponiveis, Startup.AppSettings.MensagemPadraoDescarteProutoVencido);
                }
                else
                {
                    body = PrepararMensagemCorpoEmail(request.DatasDisponiveis, Startup.AppSettings.MensagemPadraoDescarteEmbalagens);
                }

                string nomeArq = PrepararAnexoEmail(request.ListaProdutos, DateTime.UtcNow.ToString("yyyyMMddHHmmssfff",
                                            CultureInfo.InvariantCulture), request.NomeResponsavel, request.NomeArquivo);
             
                
                BackgroundJob.Enqueue(() => EnviarEmailDescartePendente(request.EmailRemetente, body, nomeArq,"AgroPop informa: Descarte Pendentes"));

                var repositoryAgendamento = new AgendamentoDescarteRepository(_context);
                
                var agendamentosPorLote = repositoryAgendamento.FindAgendamentoPorLote(IdLoteAgendamento);

                              
                foreach (AgendamentoDescarteSolicitado agendamento in agendamentosPorLote)
                {  
                    agendamento.StatusProposta="Email Enviado";
                    agendamento.DataEnvioEmail = DateTime.Now;
                    repositoryAgendamento.Update(agendamento);
                } 

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Exception in sendEmail:" + ex.StackTrace);
            }
        }


        private string PrepararAnexoEmail(List<DescartePendente> ListaProdutos, string data, string FabricanteNome, string nomeArquivo)
        {
            var delimiter = "\t";
            string nomeArq = String.Format("{0}{1}_{2}_{3}.{4}", "c:\\temp\\", data, FabricanteNome, nomeArquivo, "txt");
            using (TextWriter tw = new StreamWriter(nomeArq))
            {
                tw.WriteLine(String.Format("{0}{1}{2}", "Produto", delimiter, "QuantidadeEmEstoque"));
                foreach (var produtoFinalizado in ListaProdutos)
                    tw.WriteLine(String.Format("{0}{1}{2}", produtoFinalizado.NomeProduto, delimiter, produtoFinalizado.QtdeprodutoDisponivel));
            }
            return nomeArq;
        }

        private string PrepararMensagemCorpoEmail(List<DatasDisponiveisMessage> datasDisp, string mensagem)
        {

            //verificar porque não está chegando as 3 datas disponíveis conforme implementado
            //as datas já estão no banco de dados

            string data1, link1, data2, link2, data3, link3;
            
            data1 = datasDisp[0].Data.ToString();
            link1 = datasDisp[0].LinkAgendamento.ToString();

            data2 = datasDisp[1].Data.ToString();
            link2 = datasDisp[1].LinkAgendamento.ToString();

            data3 = datasDisp[2].Data.ToString();
            link3 = datasDisp[2].LinkAgendamento.ToString();

            return String.Format(mensagem, data1, link1, data2, link2, data3, link3);
        }
    }
}
