using DescarteService.Data.Models;
using DescarteService.DataContext;
using Hangfire;
using Messages.Services.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace DescarteService.Services
{
    public class EmailService:IEmailService
    {
        private readonly AppDataContext _context;

        public EmailService(AppDataContext context)
        {
            this._context = context;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void  EnviarEmailDescartePendente(string emailRemetente, string body, string nomeAnexo, string assunto)
        {   
            lock(typeof(EmailService))
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
        }


        public void EnviarDescarteProdutoPendente(ComunicarDescartePendenteMessageRequest request, Guid IdLoteAgendamento )
        {
            try
            {
                var body="";
                string assuntoEmail="";
                if (request.NomeArquivo == "DescarteProdutoVencido")
                {
                    body = PrepararMensagemCorpoEmail(request.DatasDisponiveis, Startup.AppSettings.MensagemPadraoDescarteProutoVencido);
                    assuntoEmail="Produtos Vencidos Disponíveis para Retirada";
                }
                else
                {
                    body = PrepararMensagemCorpoEmail(request.DatasDisponiveis, Startup.AppSettings.MensagemPadraoDescarteEmbalagens);
                    assuntoEmail="Produtos Finalizados Disponíveis para Retirada";
                }

                string nomeArq = PrepararAnexoEmail(request.ListaProdutos, DateTime.UtcNow.ToString("yyyyMMddHHmmssfff",
                                            CultureInfo.InvariantCulture), request.NomeResponsavel, request.NomeArquivo);
             
                
                BackgroundJob.Enqueue(() => EnviarEmailDescartePendente(request.EmailRemetente, body, nomeArq, assuntoEmail));

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
              
            var descartePendente = ListaProdutos.GroupBy(p=>new {p.NomeProduto, p.QtdeprodutoDisponivel, p.DataVencimento}).Select(global=>global.First()).ToList();
            
            using (TextWriter tw = new StreamWriter(nomeArq))
            {
                tw.WriteLine(String.Format("{0}{1}{2}{3}{4}", "Produto", delimiter, "QuantidadeEmEstoque", delimiter, "DataVencimento"));
                foreach (var produtoAptoDescarte in descartePendente)
                    tw.WriteLine(String.Format("{0}{1}{2}{3}{4}", produtoAptoDescarte.NomeProduto, delimiter, produtoAptoDescarte.QtdeprodutoDisponivel, delimiter, produtoAptoDescarte.DataVencimento));
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
