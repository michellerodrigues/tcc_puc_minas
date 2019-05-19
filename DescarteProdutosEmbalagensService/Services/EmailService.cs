using DescarteService.Services.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace DescarteService.Services
{
    public class EmailService
    {
        public void NotificarStatusJob(string para, string nomeJob, string link)
        {

            SmtpClient client = new SmtpClient(Startup.AppSettings.EnvioEmail.ServidorSMTP);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Startup.AppSettings.EnvioEmail.UsuarioEmail, Startup.AppSettings.EnvioEmail.SenhaEmail);
            client.DeliveryMethod = SmtpDeliveryMethod.Network; // modo de envio
            client.EnableSsl = true; // GMail requer SSL
            client.Port = Startup.AppSettings.EnvioEmail.PortaServidor;

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(Startup.AppSettings.EnvioEmail.UsuarioEmail);
            mail.To.Add(para);
            mail.Subject = String.Format("Falha ao Executar Job: {0}", nomeJob);
            mail.Body = String.Format(Startup.AppSettings.MensagemPadraoErroJob, link);
        }

        public void EnviarDescarteProdutoPendente(ComunicarDescartePendenteMessageRequest request)
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
                mail.To.Add(request.EmailRemetente);
                mail.Subject = String.Format("AgroPop informa: Descarte Pendentes");

                //client.Send(mail);

                if (request.NomeArquivo == "DescarteProdutoVencido")
                {
                    mail.Body = PrepararMensagemCorpoEmail(request.DatasDisponiveis, Startup.AppSettings.MensagemPadraoDescarteProutoVencido);
                }
                else
                {
                    mail.Body = PrepararMensagemCorpoEmail(request.DatasDisponiveis, Startup.AppSettings.MensagemPadraoDescarteEmbalagens);
                }

                string nomeArq = PrepararAnexoEmail(request.ListaProdutos, DateTime.UtcNow.ToString("yyyyMMddHHmmssfff",
                                            CultureInfo.InvariantCulture), request.NomeResponsavel, request.NomeArquivo);
                                            
                mail.Attachments.Add(new Attachment(@nomeArq));
                mail.IsBodyHtml = true;
                client.Send(mail);

            }
            catch (Exception ex)
            {
                //resolver este problema aqui
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
