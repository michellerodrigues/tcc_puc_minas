using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaService.DataContext;
using AgendaService.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using NServiceBus;
using NServiceBus.Persistence;

namespace AgendaService
{
    public class Startup
    {
         public IConfiguration Configuration { get; set; }
         public static AppSettings AppSettings { get; private set; }
/*         public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        } */


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            this.Configuration = builder.Build();

            AppSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            ConfigurarNserviceBus();
            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDataContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default")));
        }

        public void ConfigurarNserviceBus()
        {                     

            EndpointConfiguration endpointConfiguration = new EndpointConfiguration("AgendaService");

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
            //passar isso para o webconfig
            .ConnectionString("host=localhost;user=guest;password=guest")
            .UseDirectRoutingTopology();

            endpointConfiguration.SendFailedMessagesTo("Descarte_Error");
            endpointConfiguration.AuditProcessedMessagesTo("Descarte.Audit");
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();

            var persistence = endpointConfiguration.UsePersistence<NHibernatePersistence>();
            persistence.ConnectionString(AppSettings.SagaDescarteDB); //@ na frente
                
            endpointConfiguration.EnableInstallers();

           // var routing = transport.Routing();
          //  routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

            Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();

           // var agendaConfirmada = new AgendamentoConfirmadoEvent(Agenda);

           //SendLocal(agendaConfirmada);
           // BuildWebHost(args).Run();
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
        }
    }
}
