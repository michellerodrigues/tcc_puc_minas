using System.Collections.Generic;

namespace Agropop.Saga.Interfaces
{   public interface IQueueOptions
    {
        string Queue
        {
            get;
        }
        bool Durable
        {
            get;
        }

        bool Exclusive
        {
            get;
        }

        
        bool AutoDelete
        {
            get;
        }

        IDictionary<string, object> Arguments
        {
            get;
        }
    }
}