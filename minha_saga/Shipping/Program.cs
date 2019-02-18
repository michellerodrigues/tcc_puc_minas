using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Transport;
using Messages;

namespace Shipping
{
    class Program
    {
        static async Task Main()
        {
            /*first version 
            Console.Title = "MinhaSaga_Shipping";

            var endpointConfiguration = new EndpointConfiguration("Minha_Fila");

           // endpointConfiguration.UseTransport<LearningTransport>().StorageDirectory("D:\\mica\\tcc\\tcc_puc_minas\\minha_saga\\minha_pasta_de_transporte");
            endpointConfiguration.UseTransport<LearningTransport>().StorageDirectory("D:\\mica\\tcc\\tcc_puc_minas\\minha_saga");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
            */
            Console.Title = "Shipping";

            var endpointConfiguration = new EndpointConfiguration("Shipping");

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
            .ConnectionString("host=localhost;user=guest;password=guest")
            .UseDirectRoutingTopology();


            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            #region ShippingPubSubConfig
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(OrderPlaced), "Sales");
            routing.RouteToEndpoint(typeof(OrderBilled), "Billing");
            #endregion

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
