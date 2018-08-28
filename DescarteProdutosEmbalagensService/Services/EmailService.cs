using DescarteService;
using DescarteService.Services.Messages;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

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
            mail.Subject = String.Format("Falha ao Executar Job: {0}",nomeJob);
            mail.Body = String.Format(Startup.AppSettings.MensagemPadraoErroJob,link);            
        }

        public void EnviarDescarteProdutoPendente(ComunicarDescartePendenteMessageRequest request)
        {   
            SmtpClient client = new SmtpClient(Startup.AppSettings.EnvioEmail.ServidorSMTP);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Startup.AppSettings.EnvioEmail.UsuarioEmail, Startup.AppSettings.EnvioEmail.SenhaEmail);
            
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(Startup.AppSettings.EnvioEmail.UsuarioEmail);
            mail.To.Add(request.EmailRemetente);
            mail.Subject = String.Format("AgroPop informa: Descarte Pendentes");

            if(request.NomeArquivo=="DescarteProdutoVencido")
            {
                mail.Body = PrepararMensagemCorpoEmail(request.DatasDisponiveis, Startup.AppSettings.MensagemPadraoDescarteProutoVencido);
            }
            else
            {
                mail.Body = PrepararMensagemCorpoEmail(request.DatasDisponiveis, Startup.AppSettings.MensagemPadraoDescarteEmbalagens);
            }
           
            string nomeArq = PrepararAnexoEmail(request.ListaProdutos, DateTime.Now.ToShortDateString(), request.NomeResponsavel,request.NomeArquivo);
            
            mail.Attachments.Add(new Attachment(@nomeArq));

            client.Send(mail);
        }  


        private string PrepararAnexoEmail(List<DescartePendente> ListaProdutos, string data, string FornecedorNome, string nomeArquivo )
        {
            string nomeArq = String.Format("{0}{1}_{2}_{3}.{4}","c:\\temp\\",data,FornecedorNome,nomeArquivo,"txt");
            using(TextWriter tw = new StreamWriter(nomeArq))
            {
                tw.WriteLine("Produto\\tQuantidadeEmEstoque");
                foreach (var produtoFinalizado in ListaProdutos)
                    tw.WriteLine(String.Format("{0}\\t{1}",produtoFinalizado.NomeProduto,produtoFinalizado.QtdeprodutoDisponivel));
            }  
            return nomeArq;
        }  

        private string PrepararMensagemCorpoEmail(List<DatasDisponiveisMessage> datasDisp, string mensagem)
        {
            string data1,link1, data2, link2, data3, link3;

            data1 = datasDisp[0].Data.ToString();
            link1 = datasDisp[0].LinkAgendamento.ToString();

            data2 = datasDisp[1].Data.ToString();
            link2 = datasDisp[1].LinkAgendamento.ToString();

            data3 = datasDisp[2].Data.ToString();
            link3 = datasDisp[2].LinkAgendamento.ToString();

            return String.Format(mensagem,data1,link1,data2,link2,data3,link3);
        }  

    }
}
