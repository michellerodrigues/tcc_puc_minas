using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DescarteService.DataContext;
using DescarteService.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using DescarteServices.Jobs;
using Messages.Descartes.Messages;
using DescarteService.Services;
using DescarteService.Data.Interfaces;
using DescarteService.Data.Models;
using DescarteService.Data.Repository;

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
            services.AddDbContext<AppDataContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default")));
        
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddHangfire(config => config.UseSqlServerStorage(Configuration.GetConnectionString("Jobs")));

            services.AddScoped<IDescarteApiService,DescarteApiService>();  
            services.AddScoped<IEmailService,EmailService>();
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepository<AgendamentoDescarteSolicitado>), typeof(Repository<AgendamentoDescarteSolicitado>));
            services.AddScoped(typeof(IRepository<LoteDescarte>), typeof(Repository<LoteDescarte>));
            services.AddScoped<IAgendamentoDescarteRepository,AgendamentoDescarteRepository>(); 
            services.AddScoped<ILoteDescarteRepository,LoteDescarteRepository>(); 
            
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            DBInicializar.StartDataBase(app);

            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new[] { new JobsAuthorizationFilter() }
            });

            var jobOptions = new BackgroundJobServerOptions
            {
                ServerName = String.Format("{0}:produtos", Environment.MachineName),
                WorkerCount = 8,
                Queues = new[] { "default","vencidos","finalizados" }
            };

            app.UseHangfireServer(jobOptions);

            AutomaticRetryAttribute filtroClienteRepeticao = new AutomaticRetryAttribute();
            filtroClienteRepeticao.Attempts = 0;

            GlobalJobFilters.Filters.Add(new ProlongExpirationTimeAttribute());
            GlobalJobFilters.Filters.Add(new LogEverythingAttributeJobFilter());
            
            int retries = AppSettings.RetriesJob;

            int intervaloLeituraJob = AppSettings.IntervaloLeituraJob;

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = retries}); 

            RecurringJob.AddOrUpdate<DescarteApiService>("VerificarProdutosVencidos", js => js.ObterProdutosVencidos(), Cron.DayInterval(7));    

            RecurringJob.AddOrUpdate<DescarteApiService>("VerificarProdutosFinalizados", js => js.ObterProdutosFinalizados(),  Cron.DayInterval(7));   



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


    }
}
