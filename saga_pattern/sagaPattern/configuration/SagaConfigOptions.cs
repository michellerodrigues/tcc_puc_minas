public class SagaConfigOptions
{
     public const string SagaConfig = "AppSettings:SagaConfig";
     public ConnectionOpt Connection { get; set; }
     public ExchangeOpt Exchange { get; set; }
     public QueueOpt Queue{ get; set; }   
}

public class ConnectionOpt
{   
     public const string Connection = "Connection";
     public string RabbitMQSUrl { get; set; }
}

public class ExchangeOpt
{   
     public const string Exchange = "Exchange";
     public string Name { get; set; }
     public string Type { get; set; }
     public string RoutingKey { get; set; }
     public bool Durable { get; set; }     
     public string AlternateExchange { get; set; }
}

public class QueueOpt
{   
     public const string Queue = "Queue";
     public string Name { get; set; }
     public bool Durable { get; set; }
     public bool Exclusive { get; set; }
     public bool AutoDelete { get; set; }     
}