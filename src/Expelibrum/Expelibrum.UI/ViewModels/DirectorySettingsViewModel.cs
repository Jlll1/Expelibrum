using Expelibrum.UI.ViewModels.Dialogs;
using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public class DirectorySettingsViewModel : ViewModelBase, IDirectorySettingsViewModel
    {
        #region fields

        private IFolderBrowserDialog _folderBrowser;

        private string _originDirectoryPath;
        private string _targetDirectoryPath;
        private bool _includeSubdirectories = true;

        #endregion

        #region properties

        public string OriginDirectoryPath
        {
            get => _originDirectoryPath;
            set
            {
                _originDirectoryPath = value;
                OnPropertyChanged();
            }
        }
        public string TargetDirectoryPath
        {
            get => _targetDirectoryPath;
            set
            {
                _targetDirectoryPath = value;
                OnPropertyChanged();
            }
        }

        public bool IncludeSubdirectories
        {
            get => _includeSubdirectories;
            set
            {
                _includeSubdirectories = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region commands

        public ICommand ChangeOriginDirectoryPathCommand { get; }
        public ICommand ChangeTargetDirectoryPathCommand { get; }

        #region commandmethods

        private void OnChangeOriginDirectoryPath(object param)
        {
            OriginDirectoryPath = _folderBrowser.GetDirectoryPathDialog();
        }

        private void OnChangeTargetDirectoryPath(object param)
        {
            TargetDirectoryPath = _folderBrowser.GetDirectoryPathDialog();
        }

        #endregion

        #endregion

        #region constructors
        public DirectorySettingsViewModel(IFolderBrowserDialog folderBrowser)
        {
            _folderBrowser = folderBrowser;

            ChangeOriginDirectoryPathCommand = new RelayCommand(OnChangeOriginDirectoryPath);
            ChangeTargetDirectoryPathCommand = new RelayCommand(OnChangeTargetDirectoryPath);
        }

        #endregion

        #region methods

        #endregion
    }
}
