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
    public string EstoqueServicesURL{ get; set; }

    public string SagaDescarteDB{ get; set; }
}

public class EnvioEmail
{    public string ServidorSMTP { get; set; }
     public string UsuarioEmail { get; set; }
     public string SenhaEmail { get; set; }
     public int PortaServidor { get; set; }
     public bool EnableSSL { get; set; }
}

