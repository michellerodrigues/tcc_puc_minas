using System;
using System.Threading.Tasks;
using NServiceBus;
using Messages;
using NServiceBus.Logging;
using NServiceBus.Transport.RabbitMQ;

namespace minha_saga
{
    class Program
    {
        static async Task Main()
        {
            /* Console.Title = "MinhaSaga_ClientUI";

            var endpointConfiguration = new EndpointConfiguration("Minha_Fila");

            //endpointConfiguration.UseTransport<LearningTransport>().StorageDirectory("D:\\mica\\tcc\\tcc_puc_minas\\minha_saga\\minha_pasta_de_transporte");
            endpointConfiguration.UseTransport<LearningTransport>().StorageDirectory("D:\\mica\\tcc\\tcc_puc_minas\\minha_saga");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            await RunLoop(endpointInstance)
                .ConfigureAwait(false);

         //   Console.WriteLine("Press Enter to exit...");
         //   Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);*/

            Console.Title = "ClientUI";

            var endpointConfiguration = new EndpointConfiguration("ClientUI");

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
            .ConnectionString("host=localhost;user=guest;password=guest")
            .UseDirectRoutingTopology();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            await RunLoop(endpointInstance)
                .ConfigureAwait(false);

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }

         #region RunLoop

        static ILog log = LogManager.GetLogger<Program>();

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the command
                        var command = new PlaceOrder
                        {
                            OrderId = Guid.NewGuid().ToString()
                        };

                        // Send the command to the local endpoint
                        log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                        await endpointInstance.SendLocal(command)
                            .ConfigureAwait(false);

                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }

        #endregion
    }
}
