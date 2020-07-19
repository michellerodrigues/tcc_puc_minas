using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using DescarteServices.Jobs;
using System.Data.SqlClient;
using DescarteService.Services;
using Agropop.Saga.DependencyInjection;

namespace DescarteService
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public static AppSettings AppSettings { get; private set; }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            AppSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddHangfire(config => config.UseSqlServerStorage(GetHangfireConnectionString("JobDB")));

            ConfigureSagaPatern(services,Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new[] { new JobsAuthorizationFilter() }
            });

            var jobOptions = new BackgroundJobServerOptions
            {
                ServerName = String.Format("{0}:produtos", Environment.MachineName),
                WorkerCount = 8,
                Queues = new[] { "default", "vencidos", "finalizados" }
            };

            app.UseHangfireServer(jobOptions);

            AutomaticRetryAttribute filtroClienteRepeticao = new AutomaticRetryAttribute();
            filtroClienteRepeticao.Attempts = 0;

            GlobalJobFilters.Filters.Add(new ProlongExpirationTimeAttribute());
            GlobalJobFilters.Filters.Add(new LogEverythingAttributeJobFilter());

            int retries = AppSettings.RetriesJob;

            int intervaloLeituraJob = AppSettings.IntervaloLeituraJob;

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = retries });

            RecurringJob.AddOrUpdate<DescarteJobService>("VerificarProdutosVencidos", js => js.ObterProdutosVencidos(), Cron.DayInterval(7));

            RecurringJob.AddOrUpdate<DescarteJobService>("VerificarProdutosFinalizados", js => js.ObterProdutosFinalizados(), Cron.DayInterval(7));

            
        }


        private void ConfigureSagaPatern(IServiceCollection services, IConfiguration configuration)
        {
            SagaPluginExtensions.AddSagaPattern(services, configuration);            
        }
        private int GetRetriesJob(string tentativas)
        {
            int retriesInt = 0;

            if (tentativas == null)
            {
                retriesInt = 0;
            }

            retriesInt = Convert.ToInt32(tentativas);

            return retriesInt;
        }

        private int GetIntervaloLeitura(string intervaloChave)
        {
            int intervalo = 2;
            if (String.IsNullOrEmpty(intervaloChave))
            {
                intervalo = 2;
            }
            else
            {
                intervalo = Convert.ToInt32(intervaloChave);

                if (intervalo < 2)
                {
                    intervalo = 2;
                }
            }

            return intervalo;

        }
        private string GetHangfireConnectionString(string dbName)
        {
            string connectionStringFormat = "Server = DESKTOP-MO3ES0N\\SQLEXPRESS;Database={0};Integrated Security=False;User Id=job_user;Password=root@1234";
            string.Format(connectionStringFormat, dbName);

            using (var connection = new SqlConnection(String.Format(connectionStringFormat, "master")))
            {
                connection.Open();

                using (var command = new SqlCommand(string.Format(
                    @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}') 
                                    create database [{0}];
                      ", dbName), connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            return String.Format(connectionStringFormat, dbName);
        }
    }
}
