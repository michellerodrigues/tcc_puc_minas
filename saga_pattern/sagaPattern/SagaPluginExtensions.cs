
using System;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using Agropop.Saga.Interfaces;
using Microsoft.Extensions.Options;

namespace Agropop.Saga.DependencyInjection
{
    public static class SagaPluginExtensions
    {            
        public static IOptionsMonitor<SagaConfigOptions> sagaConfigOptions {get;set;}
        public static void AddSagaPattern(IServiceCollection services, IConfiguration Configuration)
        {   

           //  services.Configure<SagaConfigOptions>(Configuration.GetSection(
                         //               SagaConfigOptions.SagaConfig)).ValidateDataAnnotations();

             services.AddOptions<SagaConfigOptions>()
            .Bind(Configuration.GetSection(SagaConfigOptions.SagaConfig))
            .ValidateDataAnnotations();     

            services.AddSingleton<IMessageBroker, RabbitMQClient>();    
            services.AddTransient<IMessageBroker, RabbitMQClient>();
            services.AddTransient<IQueueOptions, QueueOptions>();
            services.AddTransient<IExchangeOptions, ExchangeOptions>();
            services.AddTransient<IChannel, Channel>();    
        }        
    }
}