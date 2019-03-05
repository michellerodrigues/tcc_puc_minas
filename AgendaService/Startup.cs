using System;
using AgendaService.DataContext;
using AgendaService.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using NServiceBus.Persistence;
using Microsoft.Extensions.Logging;
using AgendaService.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using AgendaService.Services.Interfaces;
using NServiceBus.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Messages.Descartes.Commands;
using AgendaService.Saga;
using System.Collections.Generic;

namespace AgendaService
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public static AppSettings AppSettings { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        } 


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            this.Configuration = builder.Build();

            AppSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            //ConfigurarNserviceBus();
            
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRouting();
            services.AddDbContext<AppDataContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default")));
            

            var context = services.BuildServiceProvider().GetService<AppDataContext>();

            ConfigureNserviceBus(services,context);            
        
        }

        public void ConfigureNserviceBus(IServiceCollection services, AppDataContext context)
        {
                        /* 
            var endpointName = "Descarte.Messages";
            var machineName = $"{Dns.GetHostName()}.{IPGlobalProperties.GetIPGlobalProperties().DomainName}";
            var instanceIdentifier = $"{endpointName}@{machineName}";*/
            var endpointConfiguration = new EndpointConfiguration("Descarte.Agendamento");


            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
            //passar isso para o webconfig  
            .ConnectionString("host=localhost;user=guest;password=guest")
            .UseConventionalRoutingTopology();
            
            //
            const string SERVICE_CONTROL_METRICS_ADDRESS = "Descarte.Monitoring";

            var hostId = new Guid();
            endpointConfiguration.UniquelyIdentifyRunningInstance()
                .UsingCustomIdentifier(hostId);

            var metrics = endpointConfiguration.EnableMetrics();

            metrics.SendMetricDataToServiceControl(
                serviceControlMetricsAddress: SERVICE_CONTROL_METRICS_ADDRESS,
                interval: TimeSpan.FromSeconds(10),
                instanceId: "Descarte.Agendamento");

            endpointConfiguration.SendFailedMessagesTo("Descarte.Error");
            endpointConfiguration.AuditProcessedMessagesTo("Descarte.Audit");
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.AuditSagaStateChanges(serviceControlQueue: "DescarteServiceMonitoring");

            var persistence = endpointConfiguration.UsePersistence<NHibernatePersistence>();
            persistence.ConnectionString(@"Server = DESKTOP-C8BIS20\MSSQLSERVER2;Database=SagaDescarteDB;Integrated Security=True;"); //@ na frente
                
            endpointConfiguration.EnableInstallers();

           // var routing = transport.Routing();
          //  routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");
            IEndpointInstance endpoint = null;

           
            endpointConfiguration.UseContainer<ServicesBuilder>(
            customizations: customizations =>
            {
                customizations.ExistingServices(services);
            });
           
            //endpointConfiguration.SendOnly();  

            /* 
            var routing = transport.Routing();
            routing.RouteToEndpoint(
                assembly: typeof(AgendarRetiradaCommand).Assembly,
                destination: "Descarte.Agendamento");*/


            endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();   
                         
            services.AddSingleton<IMessageSession>(endpoint);
            services.AddSingleton<IAgendaApiService>(new AgendaApiService(endpoint,context)); 
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

           
           /* 
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });*/

            var trackPackageRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync(
                    $"Hello! Route values: {string.Join(", ", routeValues)}");
            });

            var routeBuilder = new RouteBuilder(app, trackPackageRouteHandler);

            routeBuilder.MapRoute(
                "Track Package Route",
                "package/{operation:regex(^track|create|detonate$)}/{id:int}");

            routeBuilder.MapGet("hello/{name}", context =>
            {
                var name = context.GetRouteValue("name");
                return context.Response.WriteAsync($"Hi, {name}!");
            });

            var routes = routeBuilder.Build();

            app.UseMvcWithDefaultRoute();

            app.UseRouter(routes);

            DBInicializar.StartDataBase(app);          
        }
    }
}