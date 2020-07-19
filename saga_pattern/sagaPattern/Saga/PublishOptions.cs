

namespace Agropop.Saga
{
    public class PublishOptions : IPublishOptions
    {
        private readonly IChannel _channel;
        public PublishOptions(IChannel channel)
        {
           _channel = channel;
        }
        public IChannel Channel => _channel;
    }
}