using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriagemService.DataContext;
using TriagemService.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Persistence;
using System.Net;
using System.Net.NetworkInformation;
using Messages.Descartes.Commands;

namespace TriagemService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDataContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default")));

        }

        public void ConfigureNserviceBus(IServiceCollection services)
        {
            
            var endpointName = "Descarte.Triagem";
            var conexaoQueue = "host=localhost;user=guest;password=guest";
            var sendFailedMessagesTo = "Particular.ServiceControl.Error";
            var auditProcessedMessagesTo = "Particular.ServiceControl.Audit";
            var serviceControlQueueName = "Particular.ServiceControl";
            const string SERVICE_CONTROL_METRICS_ADDRESS = "Particular.Monitoring";


            var machineName = $"{Dns.GetHostName()}.{IPGlobalProperties.GetIPGlobalProperties().DomainName}";
            var instanceIdentifier = $"{endpointName}@{machineName}";
            
            var endpointConfiguration = new EndpointConfiguration(endpointName);

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
            .ConnectionString(conexaoQueue)
            .UseConventionalRoutingTopology();
            
            endpointConfiguration.SendFailedMessagesTo(sendFailedMessagesTo);
            endpointConfiguration.AuditProcessedMessagesTo(auditProcessedMessagesTo);

            var hostId = Guid.NewGuid();
            endpointConfiguration.UniquelyIdentifyRunningInstance()
                .UsingCustomIdentifier(hostId);
            

            
            
            //metric não serve pra sendonly
            var metrics = endpointConfiguration.EnableMetrics();

            metrics.SendMetricDataToServiceControl(
                serviceControlMetricsAddress: SERVICE_CONTROL_METRICS_ADDRESS,
                interval: TimeSpan.FromSeconds(10),
                instanceId: endpointName);


            endpointConfiguration.ReportCustomChecksTo(
                serviceControlQueue: serviceControlQueueName);

            endpointConfiguration.SendHeartbeatTo(
                serviceControlQueue: serviceControlQueueName,
                frequency: TimeSpan.FromSeconds(15));


            endpointConfiguration.AuditSagaStateChanges(
                serviceControlQueue: serviceControlQueueName);

            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();

            var persistence = endpointConfiguration.UsePersistence<NHibernatePersistence>();
            persistence.ConnectionString(@"Server = DESKTOP-C8BIS20\MSSQLSERVER2;Database=SagaDescarteDB;Integrated Security=True;"); //@ na frente
            
            //endpointConfiguration.SendOnly();  


            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(RealizarTriagemCommand), endpointName);

            IEndpointInstance endpoint = null;

            endpointConfiguration.UseContainer<ServicesBuilder>(
            customizations: customizations =>
            {
                customizations.ExistingServices(services);
            });
           
            endpointConfiguration.EnableInstallers();
            endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();   

            services.AddSingleton<IMessageSession>(endpoint);           
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
