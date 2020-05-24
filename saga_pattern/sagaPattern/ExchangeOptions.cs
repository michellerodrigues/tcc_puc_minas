namespace Agropop.Saga
{
    public class ExchangeOptions : IExchangeOptions
    {
        public readonly string _exchange;
        public readonly string _type;
        public readonly string _routingKey;
        public ExchangeOptions(string exchange, string type, string routingKey)
        {
            _exchange = exchange;
            _type = type;
            _routingKey  =routingKey;
        }

        public string Exchange { get {return _exchange;}}
        public string Type { get {return _type;}}
        public string RoutingKey {get {return _routingKey;}}

    }
}