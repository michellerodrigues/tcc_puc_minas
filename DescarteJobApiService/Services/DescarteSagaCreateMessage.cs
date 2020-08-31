using Agropop.Saga.Util;
using Messages.Descartes.Commands;

namespace DescarteService.Services
{
    public class DescarteSagaCreateMessage : BaseTransformMessage<DescartePendenteCommand>
    {
        public Saga.Messages.Base.BaseMessage TransformCommandToMessage(string lote)
        {
            var command = new DescartePendenteCommand()
            {
                LoteDeDescarte = lote
            };

            return base.TransformTOBaseMessage(command);
        }
    }
}
