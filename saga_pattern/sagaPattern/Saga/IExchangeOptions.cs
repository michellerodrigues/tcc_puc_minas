namespace Agropop.Saga
{   public interface IExchangeOptions
    {
        string Exchange
        {
            get;
        }

        string Type
        {
            get;
        }

        string RoutingKey
        {
            get;
        }

        bool Durable
        {
            get;
        }

        string AlternateExchange
        {
            get;
        }
    }
}