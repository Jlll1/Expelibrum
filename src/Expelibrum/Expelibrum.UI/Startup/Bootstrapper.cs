using Autofac;
using Expelibrum.Services;
using Expelibrum.UI.ViewModels;
using Expelibrum.UI.ViewModels.Dialogs;

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
            builder.RegisterType<DirectoryViewModel>().As<IDirectoryViewModel>();


            return builder.Build();
        }
    }
}
