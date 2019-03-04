using Autofac;

namespace AgendaService
{
    internal class AutofacServiceProvider
    {
        private IContainer container;

        public AutofacServiceProvider(IContainer container)
        {
            this.container = container;
        }
    }
}