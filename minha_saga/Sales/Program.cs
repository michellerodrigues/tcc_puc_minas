using System;
using System.Threading.Tasks;
using NServiceBus;


namespace Sales
{
    class Program
    {
        static async Task Main()
        {
            /* first version
            Console.Title = "MinhaSaga_Sales";

            var endpointConfiguration = new EndpointConfiguration("Minha_Fila");

           // endpointConfiguration.UseTransport<LearningTransport>().StorageDirectory("D:\\mica\\tcc\\tcc_puc_minas\\minha_saga\\minha_pasta_de_transporte");
            endpointConfiguration.UseTransport<LearningTransport>().StorageDirectory("D:\\mica\\tcc\\tcc_puc_minas\\minha_saga");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false); */

             Console.Title = "Sales";

            var endpointConfiguration = new EndpointConfiguration("Sales");

            #region MsmqConfig
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
            .ConnectionString("host=localhost;user=guest;password=guest")
            .UseDirectRoutingTopology();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();
            #endregion

            #region NoDelayedRetries
            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Delayed(delayed => delayed.NumberOfRetries(0));
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
