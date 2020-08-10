using Agropop.Saga.Factory;
using Messages.Descartes.Messages;


namespace DescarteService.Services
{
    public static class DescarteSagaCreateMessage : ITransformleMessage<DescartePendenteCommand>
    {
        private BaseMessge AddBaseMessage(IMessage message)
        {
            BaseMessage baseMessage = new BaseMessage()
            {
                assemblyName = message.GetType().Assembly.FullName,
                fullNameType = message.GetType().FullName,
                content = message,
                handleMethod="AgendarRetiradaCommandHandle",
                UserForNewType = message.GetType().FullName + ","+ message.GetType().Assembly.FullName
            };  

            return  baseMessage;
        }
    }
}
