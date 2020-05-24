using System;
using System.Collections.Generic;
using RabbitMQ.Client;

namespace Agropop.Saga
{
    public class SagaPattern
    {
        private static IMessageHandlerContext _context;
        public SagaPattern(IMessageHandlerContext context)
        {           
            _context = context;
        }
        IMessageHandlerContext Context => _context;
    }
}