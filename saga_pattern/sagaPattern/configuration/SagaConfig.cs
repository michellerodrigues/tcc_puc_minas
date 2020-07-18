public class SagaConfig : ISagaConfig
{
     public ConnectionConfig Connection { get; set; }
     public ExchangeOptionsConfig ExchangeOptions { get; set; }
     public QueueOptionsConfig QueueOptions { get; set; }
}

public class ConnectionConfig
{   
     public string RabbitMQSUrl { get; set; }
}

public class ExchangeOptionsConfig
{   
     public string Exchange { get; set; }
     public string Type { get; set; }
     public string RoutingKey { get; set; }
     public bool Durable { get; set; }     
     public string AlternateExchange { get; set; }
}

public class QueueOptionsConfig
{   
     public string Queue { get; set; }
     public bool Durable { get; set; }
     public bool Exclusive { get; set; }
     public bool AutoDelete { get; set; }     
}