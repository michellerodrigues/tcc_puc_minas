using DescarteService;
using DescarteService.Services;
using Hangfire;
using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Logging;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Web;

namespace DescarteServices.Jobs
{
    public class LogEverythingAttributeJobFilter : JobFilterAttribute, IClientFilter, IServerFilter, IElectStateFilter, IApplyStateFilter
    {
        //private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();
        public void OnCreating(CreatingContext context)
        {
            //Logger.InfoFormat("Creating a job based on method `'{0}'`...", context.Job.Method.Name);
        }

        public void OnCreated(CreatedContext context)
        {
            //Logger.InfoFormat(
            //    "Job that is based on method `'{0}'` has been created with id `'{1}'`",
            //    context.Job.Method.Name,
            //    context.BackgroundJob?.Id);
        }

        public void OnPerforming(PerformingContext context)
        {
            //Logger.InfoFormat("Starting to perform job `'{0}'`", context.BackgroundJob.Id);
        }

        public void OnPerformed(PerformedContext context)
        {
            if(context.ExceptionHandled)
            {
                string nomeJob = "";

                if (context.BackgroundJob != null)
                {
                    nomeJob = context.BackgroundJob.Job.Method.Name;
                }

                var Url = String.Format("{0}/jobs/jobs/details/{1}", Startup.AppSettings.HostJobAplicacao, context.BackgroundJob.Id);

                if (!String.IsNullOrEmpty(Startup.AppSettings.HostJobAplicacao))
                {
                    Url = String.Format("{0}/jobs/jobs/details/{1}", Startup.AppSettings.HostJobAplicacao, context.BackgroundJob.Id);
                }

                NotificarStatusJob(Startup.AppSettings.ReplyJobCC, nomeJob, Url);
            }
        }
        public void OnStateElection(ElectStateContext context)
        {           
            // throw new NotImplementedException();

        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
           // throw new NotImplementedException();
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            //throw new NotImplementedException();
        }

        private void NotificarStatusJob(string para, string nomeJob, string link)
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

    }
}