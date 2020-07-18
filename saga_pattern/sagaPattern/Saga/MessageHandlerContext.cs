using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Agropop.Saga.Factory;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace Agropop.Saga
{
    public abstract class MessageHandlerContext: IMessageHandlerContext
    {
        private readonly IChannel _channel;

        public MessageHandlerContext(IChannel channel)
        {
            _channel = channel;
        }

        public virtual void Publish(object message)
        {          
            //talvez eu possa colocar o assemblu name e o name type nos cabeçalhos da mensagem  
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            
            _channel.MessageBroker.Model.BasicPublish(exchange: _channel.ExchangeOptions.Exchange,
                                 routingKey: _channel.ExchangeOptions.RoutingKey,
                                 false,
                                 basicProperties: null,
                                 body: body);
        }
 
        public virtual List<String> Consume()
        {
            List<String> messages = new List<String>();

            var consumer = new EventingBasicConsumer(_channel.MessageBroker.Model);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;

                var message = JsonConvert.DeserializeObject<BaseMessage>(Encoding.UTF8.GetString(body.ToArray()));
                
                Handle(message);
               
            };

            _channel.MessageBroker.Model.BasicConsume(
                 queue: _channel.QueueOptions.Queue,
                 autoAck: true,
                 consumerTag: "",
                 noLocal: false,
                 exclusive: _channel.QueueOptions.Exclusive,
                 arguments: _channel.QueueOptions.Arguments,
                 consumer: consumer
             );

            Thread.Sleep(10000);

            return messages;
        }

        public virtual Task Handle(BaseMessage message)
        {          
            var message_string = JsonConvert.SerializeObject(message.content);

            Type type = Type.GetType(message.UserForNewType);

            var instancia = Activator.CreateInstance(type);

            TypeConverter tc = TypeDescriptor.GetConverter(type);            

            JsonSerializerSettings settings = new JsonSerializerSettings();

            var retornoJson = JsonConvert.DeserializeObject(message_string, type, settings);

            return new Task(Handle(DynamicCast(retornoJson, type)));
        }

        private dynamic DynamicCast(object entity, Type to)
        {
            var openCast = this.GetType().GetMethod("Cast", BindingFlags.Static | BindingFlags.NonPublic);
            var closeCast = openCast.MakeGenericMethod(to);
            
            var retorno = closeCast.Invoke(entity, new[] { entity });
            return retorno;
        }
        private static T Cast<T>(object entity) where T : class
        {
            return entity as T;
        }       
    }
}