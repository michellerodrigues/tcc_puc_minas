using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SagaDemo.Pedidos.Commands;
using SagaDemo.Pedidos.Helpers;

namespace SagaDemo.Pedidos
{
    class Program
    {
      //  const string SERVICE_CONTROL_METRICS_ADDRESS = "particular.monitoring";

   
        static void Main(string[] args)
        {
            ConfigureAndSend().GetAwaiter().GetResult();
        }

        private static async Task ConfigureAndSend()
        {
            EscapeSequencer.Install(); 
            EscapeSequencer.Bold = true;

            var defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Warn);

            while (true)
            {
                var endpointName = "MyEndpoint";
                var machineName = $"{Dns.GetHostName()}.{IPGlobalProperties.GetIPGlobalProperties().DomainName}";
                var instanceIdentifier = $"{endpointName}";

                var endpointConfiguration = new EndpointConfiguration("ExemploSaga.Pedidos");
                endpointConfiguration.EnableInstallers();
                endpointConfiguration.UseTransport<RabbitMQTransport>().ConnectionString("host=localhost;username=guest;password=guest").UseConventionalRoutingTopology();
                endpointConfiguration.SendFailedMessagesTo("ExemploSaga.Pedidos.Errors");
                endpointConfiguration.AuditProcessedMessagesTo("ExemploSaga.Pedidos.Audit");
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
                              
                var adicionarPedido = new AdicionarPedidoCommand(Guid.NewGuid(), "Mica");

                Console.WriteLine($"ENTER para enviar mensagem para pedido {adicionarPedido.Id} - {adicionarPedido.Cliente}");
                Console.ReadLine();

                await endpointInstance.SendLocal(adicionarPedido);

                Console.WriteLine("Aguardando os handlers... ENTER para finalizar");

                await Task.Delay(300).ConfigureAwait(false);
            }
        }
    }
}
    