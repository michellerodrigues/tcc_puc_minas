using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NServiceBus;
using NServiceBus.Logging;
using LogLevel = NServiceBus.Logging.LogLevel;
using System.Diagnostics;

namespace TriagemService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            ConfigureAndSend().GetAwaiter().GetResult();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static async Task ConfigureAndSend()
        {
            var defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Warn);

            while (true)
            {
                var endpointName = "MyEndpoint";
                var machineName = $"{Dns.GetHostName()}.{IPGlobalProperties.GetIPGlobalProperties().DomainName}";
                var instanceIdentifier = $"{endpointName}";

                var endpointConfiguration = new EndpointConfiguration("AgroPop.DescarteEmbalagens");
                endpointConfiguration.EnableInstallers();
                endpointConfiguration.UseTransport<RabbitMQTransport>().ConnectionString("host=localhost;username=guest;password=guest").UseConventionalRoutingTopology();
                endpointConfiguration.SendFailedMessagesTo("AgroPop.DescarteEmbalagens.Errors");
                endpointConfiguration.AuditProcessedMessagesTo("AgroPop.DescarteEmbalagens.Audit");
                endpointConfiguration.UsePersistence<NHibernatePersistence>();
                endpointConfiguration.UseSerialization<NewtonsoftSerializer>();

                var metrics = endpointConfiguration.EnableMetrics();

                metrics.RegisterObservers(
              register: context =>
              {
                  foreach (var duration in context.Durations)
                  {
                      duration.Register(
                          observer: (ref DurationEvent @event) =>
                          {
                              Trace.WriteLine($"Duration: '{duration.Name}'. Value: '{@event.Duration}'");
                          });
                  }
                  foreach (var signal in context.Signals)
                  {
                      signal.Register(
                          observer: (ref SignalEvent @event) =>
                          {
                              Trace.WriteLine($"Signal: '{signal.Name}'");
                          });
                  }
              });

                //metrics.SendMetricDataToServiceControl(
                //    serviceControlMetricsAddress: SERVICE_CONTROL_METRICS_ADDRESS,
                //    interval: TimeSpan.FromSeconds(10),
                //    instanceId: instanceIdentifier);

                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                transport.UseConventionalRoutingTopology();
                transport.ConnectionString("host=localhost;username=guest;password=guest");


                endpointConfiguration.EnableInstallers();



                var routing = transport.Routing();

                var endpointInstance = await Endpoint.Start(endpointConfiguration)
                    .ConfigureAwait(false);

                //comentado porque na triagem não lança commands, inicialmente
                // var adicionarPedido = new AdicionarPedidoCommand(Guid.NewGuid(), "Mica");

                // Console.WriteLine($"ENTER para enviar mensagem para pedido {adicionarPedido.Id} - {adicionarPedido.Cliente}");
                // Console.ReadLine();

                // await endpointInstance.SendLocal(adicionarPedido);

                Console.WriteLine("Aguardando os handlers... ENTER para finalizar");

                await Task.Delay(300).ConfigureAwait(false);
            }
        }
    }
}
