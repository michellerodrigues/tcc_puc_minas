using DescarteService;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using System;
using System.Configuration;

namespace DescarteServices.Jobs
{
    public class ProlongExpirationTimeAttribute : JobFilterAttribute, IApplyStateFilter
    {
        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = TimeSpan.FromDays(GetDaysDurationJob());
        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = TimeSpan.FromDays(GetDaysDurationJob());
        }

        private int GetDaysDurationJob()
        {
            int quantidadeDiasExpiracaoJob = Startup.AppSettings.DaysDurationQueueJob;

            int quantidadeDias = 0;

            if (quantidadeDiasExpiracaoJob == 0)
            {
                quantidadeDias = 90;
            }

            quantidadeDias = Convert.ToInt32(quantidadeDiasExpiracaoJob);

            return quantidadeDias;
        }
    }
}