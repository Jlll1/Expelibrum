using Autofac;
using Expelibrum.Services;
using Expelibrum.UI.ViewModels;
using Expelibrum.UI.Views.Dialogs;

namespace Expelibrum.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<OpenLibraryApiService>().As<IIsbnService>();

            builder.RegisterType<FolderBrowserDialog>().As<IFolderBrowserDialog>();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();


            return builder.Build();
        }
    }
}
