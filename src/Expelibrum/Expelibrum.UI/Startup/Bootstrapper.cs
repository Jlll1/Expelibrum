using Autofac;
using Expelibrum.Services;

namespace Expelibrum.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<OpenLibraryApiService>().As<IIsbnService>();


            return builder.Build();
        }
    }
}
