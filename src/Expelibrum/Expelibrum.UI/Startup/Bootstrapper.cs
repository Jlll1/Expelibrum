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
            builder.RegisterType<PDFUtils>().As<IPDFUtils>();

            builder.RegisterType<FolderBrowserDialog>().As<IFolderBrowserDialog>();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<ProcessViewModel>().As<IProcessViewModel>();
            builder.RegisterType<DirectorySettingsViewModel>().As<IDirectorySettingsViewModel>();
            builder.RegisterType<NameTaggingViewModel>().As<INameTaggingViewModel>();

            return builder.Build();
        }
    }
}
