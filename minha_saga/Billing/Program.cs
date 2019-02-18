using System;
using System.Threading.Tasks;
using NServiceBus;
using Messages;

namespace billing
{
   class Program
    {
        static async Task Main()
        {
            /*
            Console.Title = "MinhaSaga_Billing";

            var endpointConfiguration = new EndpointConfiguration("Minha_Fila");

           // endpointConfiguration.UseTransport<LearningTransport>().StorageDirectory("D:\\mica\\tcc\\tcc_puc_minas\\minha_saga\\minha_pasta_de_transporte");
            endpointConfiguration.UseTransport<LearningTransport>().StorageDirectory("D:\\mica\\tcc\\tcc_puc_minas\\minha_saga");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);*/

                 Console.Title = "Billing";

            var endpointConfiguration = new EndpointConfiguration("Billing");


            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
            .ConnectionString("host=localhost;user=guest;password=guest")
            .UseDirectRoutingTopology();

            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            #region BillingPubSubConfig
            var routing = transport.Routing();
            routing.r(typeof(OrderPlaced), "Sales");

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


