namespace Expelibrum.UI.ViewModels
{
    public class MainViewModel
    {
        public IDirectoryViewModel DirectoryViewModel { get; }

        public MainViewModel(IDirectoryViewModel directoryViewModel)
        {
            DirectoryViewModel = directoryViewModel;
        }
    }
}
