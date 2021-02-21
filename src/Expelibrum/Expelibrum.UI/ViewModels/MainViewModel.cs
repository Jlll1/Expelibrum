namespace Expelibrum.UI.ViewModels
{
    public class MainViewModel
    {
        public IProcessViewModel ProcessViewModel { get; }

        public MainViewModel(IProcessViewModel processViewModel)
        {
            ProcessViewModel = processViewModel;
        }
    }
}
