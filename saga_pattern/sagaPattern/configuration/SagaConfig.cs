public class SagaConfig : ISagaConfig
{
     public Connection Connection { get; set; }
     public ExchangeOptions ExchangeOptions { get; set; }
     public QueueOptions QueueOptions { get; set; }
}

public class Connection
{   
     public string RabbitMQSUrl { get; set; }
}

public class ExchangeOptions
{   
     public string Exchange { get; set; }
     public string Type { get; set; }
     public string RoutingKey { get; set; }
     public bool Durable { get; set; }     
     public string AlternateExchange { get; set; }
}

public class QueueOptions
{   
     public string Queue { get; set; }
     public bool Durable { get; set; }
     public bool Exclusive { get; set; }
     public bool AutoDelete { get; set; }     
}