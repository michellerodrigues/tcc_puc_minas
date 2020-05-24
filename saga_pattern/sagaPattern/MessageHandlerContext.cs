using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace Agropop.Saga
{
    public class MessageHandlerContext : IMessageHandlerContext
    {       
        public void Publish(object message, IPublishOptions options)
        {            
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            
            options.Channel.BasicPublish(exchange: options.ExchangeOptions.Exchange,
                                 routingKey: options.ExchangeOptions.RoutingKey,
                                 false,
                                 basicProperties: null,
                                 body: body);
        }
 
        public void Consume(IConsumeOptions options)
        {

            var consumer = new EventingBasicConsumer(options.Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
            };
            
            options.Channel.BasicConsume(
                queue : options.QueueOptions.Queue, 
                autoAck: true, 
                consumerTag: "",
                noLocal: false,
                exclusive: options.QueueOptions.Exclusive, 
                arguments: options.QueueOptions.Arguments, 
                consumer: consumer
            );
        }
    }
}