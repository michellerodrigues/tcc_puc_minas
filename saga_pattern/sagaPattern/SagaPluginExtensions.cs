
using System;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;

namespace Agropop.Saga.DependencyInjection
{
    public static class SagaPluginExtensions
    {               
        public static SagaConfig sagaConfig = new SagaConfig();
        public static void AddSagaPattern(IServiceCollection services, IConfiguration Configuration)
        {
            AddSagaConfig(Configuration, services);
            AddConnectionRabbitMQ(services,sagaConfig);
            services.AddTransient<IMessageBroker, RabbitMQClient>();
            services.AddTransient<IQueueOptions, QueueOptions>();
            services.AddTransient<IExchangeOptions, ExchangeOptions>();
            services.AddTransient<IChannel, Channel>();
        }

        private static void AddSagaConfig(this IConfiguration Configuration, IServiceCollection services)
        {
            Configuration.GetSection("AppSettings:SagaConfig").Bind(sagaConfig);   
            services.AddSingleton<ISagaConfig>(sagaConfig);        
        }

        private static void AddConnectionRabbitMQ(this IServiceCollection services, SagaConfig sagaConfig)
        {
            IConnectionFactory conn = new ConnectionFactory()
            {
                Uri = new Uri(sagaConfig.Connection.RabbitMQSUrl)
            };

            services.AddSingleton(conn);
        }
    }
}