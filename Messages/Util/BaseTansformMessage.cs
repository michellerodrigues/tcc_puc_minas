using System;
using Agropop.Saga.Interfaces;
using Saga.Messages.Base;

namespace Agropop.Saga.Util
{
    public abstract class BaseTransformMessage<IMessage> : ITransformMessage<IMessage>
    {
        public BaseMessage TransformTOBaseMessage(IMessage message)
        {
            BaseMessage baseMessage = new BaseMessage()
            {
                assemblyName = message.GetType().Assembly.FullName,
                fullNameType = message.GetType().FullName,
                content = message,
                handleMethod=string.Format(message.GetType().Name,"Handle"),
                UserForNewType = message.GetType().FullName + ","+ message.GetType().Assembly.FullName
            };  

            return baseMessage;
        }
    }
}