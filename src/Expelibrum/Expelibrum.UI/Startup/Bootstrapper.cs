using Autofac;
using Expelibrum.Services;
using Expelibrum.UI.ViewModels;

namespace Expelibrum.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<OpenLibraryApiService>().As<IIsbnService>();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();


            return builder.Build();
        }
    }
}
