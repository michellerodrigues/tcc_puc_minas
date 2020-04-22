namespace MessageBroker
{
    public class RabbitMqConnectionFactory
    {
            string UserName = "guest";

            string Password = "guest";

            string HostName = "localhost";

            //Main entry point to the RabbitMQ .NET AMQP client

       public void GetConnectionFactory()
       {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {

                UserName = UserName,
                Password = Password,
                HostName = HostName
            };


            var connection = connectionFactory.CreateConnection();

            var model = connection.CreateModel();

            // Create Queue


            model.QueueDeclare("demoqueue", true, false, false, null);
          //  model.QueueBind("demoqueue", "demoExchange", "directexchange_key",null);

        }

    }

}