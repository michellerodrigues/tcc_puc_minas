public class AppSettings
{
    public string JobsConectionString { get; set; }
    public int RetriesJob { get; set; }
    public int IntervaloLeituraJob { get; set; }
    public int DaysDurationQueueJob { get; set; }
    public EnvioEmail EnvioEmail { get; set; }
    public string ReplyTo { get; set; }
    public string ReplyJobCC { get; set; }
    public string MensagemPadraoErroJob{ get; set; }
    public string HostJobAplicacao{ get; set; }
    public string MensagemPadraoDescarteProutoVencido{ get; set; }    
    public string MensagemPadraoDescarteEmbalagens{ get; set; }

    public SagaConfig SagaConfig { get; set; }


}

public class EnvioEmail
{    public string ServidorSMTP { get; set; }
     public string UsuarioEmail { get; set; }
     public string SenhaEmail { get; set; }
     public int PortaServidor { get; set; }
     public bool EnableSSL { get; set; }
}

public class SagaConfig
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