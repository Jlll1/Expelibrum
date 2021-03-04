using Autofac;
using Expelibrum.Services;
using Expelibrum.Services.Events;
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
            builder.RegisterType<PdfIsbnFinder>().As<IPDFUtils>();
            builder.RegisterType<TagService>().As<ITagService>();
            builder.RegisterType<FileMoverService>().As<IFileMoverService>();
            builder.RegisterType<BookCache>().As<IBookCache>().SingleInstance();

            builder.RegisterType<FolderBrowserDialog>().As<IFolderBrowserDialog>();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<ProcessViewModel>().As<IProcessViewModel>();
            builder.RegisterType<DirectorySettingsViewModel>().As<IDirectorySettingsViewModel>();
            builder.RegisterType<DirectoryTaggingViewModel>().As<IDirectoryTaggingViewModel>();
            builder.RegisterType<NameTaggingViewModel>().As<INameTaggingViewModel>();

            return builder.Build();
        }
    }
}
